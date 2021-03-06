﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kennedy.ManagedHooks;
using FTGMaster.MacroProfiles;
using FTGMaster.Helpers;
using System.Diagnostics;

/* 使用举例
 * private MacroManager _macroManager = null;
 * private void InitMacroManager()
 * {
 *     _macroManager = new MacroManager();//初始化
 *     _macroManager.LoadProfileWithRelativePath("demo_script.txt");//载入profile
 *     _macroManager.InstallHook(MacroManagerKeyEventUpdatedCallback);//安装钩子
 *     _macroManager.UnInstallHook();//卸载钩子
 *     _macroManager.IsHooked();//检查是否安装了钩子
 * }
 * 
 * private void MacroManagerKeyEventUpdatedCallback(MacroManager manager, String keyEventString)//回调函数
 * {
 *     Debug.WriteLine(keyEventString);
 * }
 */

namespace FTGMaster.MacroManagerNamespace
{
    class MacroManager
    {
        public delegate void MacroManagerKeyEventUpdatedCallback(MacroManager manager, String keyEventString);

        private IKeyboardHookExt _keyboardHook = null;
        private HighPrecisionTimeHelper _timeHelper = null;
        private MacroProfile _currentProfile = null;
        private Dictionary<String, double> _keyPressedTimeDictionary = null;
        private Dictionary<String, double> _keyLiftedTimeDictionary = null;
        private List<SingleMacroExecutionQueue> _macroExecutionQueues = null;
        private MacroManagerKeyEventUpdatedCallback _eventUpdateCallback;

        private double _lastKeyEventTime = 0;
        private String _lastEventTypeString = "";

        public MacroManager()
        {
            //安装钩子，回调在KeyEventCallback
            _keyboardHook = HookFactory.CreateKeyboardHookExt();
            if (_keyboardHook == null)
            {
                throw new Exception("创建系统键盘钩子失败，请检查杀毒软件设置。");
            }
            _keyboardHook.KeyDown += new KeyboardEventHandlerExt(KeyDownEventCallback);
            _keyboardHook.KeyUp += new KeyboardEventHandlerExt(KeyUpEventCallback);

            //初始化time helper
            _timeHelper = HighPrecisionTimeHelper.GenerateHighPrecisionTimeHelper();
            if (_timeHelper == null)
            {
                throw new Exception("CPU或系统不支持QueryPerformanceCounter，换台电脑吧。");
            }

            //初始化
            _keyPressedTimeDictionary = new Dictionary<String, double>();
            _keyLiftedTimeDictionary = new Dictionary<String, double>();
            _macroExecutionQueues = new List<SingleMacroExecutionQueue>();
        }

        ~MacroManager()
        {
            try
            {
                _keyboardHook.UninstallHook();
            }
            catch
            {
                //do nothing here
            }
        }

        public bool InstallHook(MacroManagerKeyEventUpdatedCallback callback)
        {
            if (_currentProfile == null)
            {
                return false;
            }
            _eventUpdateCallback = callback;
            _keyboardHook.InstallHook();
            return true;
        }

        public void UnInstallHook()
        {
            _keyboardHook.UninstallHook();
        }

        public bool IsHooked()
        {
            return _keyboardHook.IsHooked;
        }

        public bool LoadProfileWithRelativePath(String relativePath)
        {
            _currentProfile = null;
            _currentProfile = MacroProfile.ProfileFromFileRelativePath("demo_script.txt");
            bool successed = _currentProfile != null;
            return successed;
        }

        private void KeyDownEventCallback(object sender, KeyboardHookEventArgs kea)//按键回调
        {
            this.KeyEventCallback(sender, kea, SingleMacroActionType.Press);
        }

        private void KeyUpEventCallback(object sender, KeyboardHookEventArgs kea)//按键回调
        {
            this.KeyEventCallback(sender, kea, SingleMacroActionType.Lift);
        }

        private void KeyEventCallback(object sender, KeyboardHookEventArgs kea, SingleMacroActionType type)//按键回调
        {
            //读取当前按下时间
            double currentTime = _timeHelper.GetCurrentMilliseconds();

            //记录按键下压和抬起的时间到dictionary中，用于后面的条件判断
            String keyString = kea.Key.ToString().ToLower();
            DirectXKeyCode code = DirectXKeyParser.DirectXKeyScanCodeFromString(keyString);
            if (code != DirectXKeyCode.None)
            {

                String[] keyAliases = DirectXKeyParser.AliasesForKeyString(keyString);
                if (keyAliases != null && keyAliases.Length != 0)
                {
                    foreach (String eachAlias in keyAliases)
                    {
                        this.StoreKeyEventTime(eachAlias, type, currentTime);
                    }
                }
                else
                {
                    this.StoreKeyEventTime(keyString, type, currentTime);
                }
            }

            //键盘事件回调
            if (_eventUpdateCallback != null)
            {
                String eventTypeString = (type == SingleMacroActionType.Press ? "press " : "lift ") + keyString + ";\n";
                if (eventTypeString != _lastEventTypeString)//事件变更才需要发送通知,为了挡掉重复按下事件
                {
                    double timeElapsed = currentTime - _lastKeyEventTime;
                    String timeElapsedString = "wait " + ((int)Math.Round(timeElapsed)).ToString() + ";\n";
                    _eventUpdateCallback(this, timeElapsedString + eventTypeString);

                    //更新last记录
                    _lastEventTypeString = eventTypeString;
                    _lastKeyEventTime = currentTime;
                }
            }

            //检查是否有合适执行的macro
            foreach (SingleMacro macro in _currentProfile.AllMacros())
            {
                SingleMacroTriggerAfterOption selectedTriggerAfterOption;//生效的after选项（自动目押）
                int delayToTriggerAfterNow;//从当前时间延后多少毫秒触发。由selectedTriggerAfterOption和当前时间计算而来
                bool shouldExcute = SingleMacro.ShouldTriggerAction(
                    keyString,
                    type,
                    currentTime,
                    macro,
                    _keyPressedTimeDictionary,
                    _keyLiftedTimeDictionary,
                    out selectedTriggerAfterOption,
                    out delayToTriggerAfterNow);
                if (shouldExcute)//找到合适执行的macro就执行，停止查找
                {
                    //判断是否应该block原有按键
                    if (macro.ShouldBlock())
                    {
                        kea.Cancel = true;
                    }

                    //excute macro
                    this.ExecuteSingleMacro(macro, delayToTriggerAfterNow);
                    break;
                }
            }
        }

        //保存按键按下记录
        private void StoreKeyEventTime(String keyString, SingleMacroActionType type, double currentTime)
        {
            if (type == SingleMacroActionType.Press)
            {
                _keyPressedTimeDictionary[keyString] = currentTime;
            }
            else if (type == SingleMacroActionType.Lift)
            {
                _keyLiftedTimeDictionary[keyString] = currentTime;
            }
        }

        //生成执行队列，执行macro
        private void ExecuteSingleMacro(SingleMacro macro, int delayMilliseconds)
        {
            string consoleMessage = string.Format("Macro begin execute macro named: {0}.", macro.NameString());
            Debug.WriteLine(consoleMessage);

            SingleMacroExecutionQueue queue = new SingleMacroExecutionQueue(macro, delayMilliseconds);
            _macroExecutionQueues.Add(queue);
            queue.Start(this.MacroCompleteCallback);
        }

        private void MacroCompleteCallback(
            SingleMacroExecutionQueue queue,
            SingleMacro macro,
            bool success
            )
        {
            bool contains = _macroExecutionQueues.Contains(queue);
            if (contains)
            {
                queue.Dispose();
                _macroExecutionQueues.Remove(queue);
            }
        }
    }
}
