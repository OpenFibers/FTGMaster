using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kennedy.ManagedHooks;
using FTGMaster.MacroProfiles;
using FTGMaster.Helpers;

//TODO：补齐脚本注释
//TODO：按键记录，用于判断后面的触发条件
//TODO：按键检测
//TODO：执行队列

namespace FTGMaster
{
    public partial class FTGMasterWindow : Form
    {
        private IKeyboardHookExt _keyboardHook = null;
        private HighPrecisionTimeHelper _timeHelper = null;
        private MacroProfile _currentProfile = null;
        private Dictionary<String, double> _keyPressedTimeDictionary = null;
        private Dictionary<String, double> _keyLiftedTimeDictionary = null;

        public FTGMasterWindow()//构造函数
        {
            InitializeComponent();

            //安装钩子，回调在KeyEventCallback
            _keyboardHook = HookFactory.CreateKeyboardHookExt();
            if (_keyboardHook == null)
            {
                throw new Exception("创建系统键盘钩子失败，请检查杀毒软件设置。");
            }
            _keyboardHook.KeyDown += new KeyboardEventHandlerExt(KeyDownEventCallback);
            _keyboardHook.KeyUp += new KeyboardEventHandlerExt(KeyUpEventCallback);
            _keyboardHook.InstallHook();

            //初始化time helper
            _timeHelper = HighPrecisionTimeHelper.GenerateHighPrecisionTimeHelper();
            if (_timeHelper == null)
            {
                throw new Exception("CPU或系统不支持QueryPerformanceCounter，换台电脑吧。");
            }

            //读取当前的profile文件
            _currentProfile = MacroProfile.ProfileFromFileRelativePath("default.txt");

            //初始化
            _keyPressedTimeDictionary = new Dictionary<String, double>();
            _keyLiftedTimeDictionary = new Dictionary<String, double>();
        }

        ~FTGMasterWindow()
        {
            _keyboardHook.UninstallHook();
        }

        private void FTGMasterWindowForm_Load(object sender, EventArgs e)//Form load
        {
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
                if (type == SingleMacroActionType.Press)
                {
                    _keyPressedTimeDictionary[keyString] = currentTime;
                }
                else if (type == SingleMacroActionType.Lift)
                {
                    _keyLiftedTimeDictionary[keyString] = currentTime;
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
                    if(macro.ShouldBlock())
                    {
                        kea.Cancel = true;
                    }

                    //excute macro
                    this.ExecuteSingleMacro(macro, delayToTriggerAfterNow);
                    break;
                }
            }
        }

        private void ExecuteSingleMacro(SingleMacro macro, int delay)
        {
            string msg = string.Format("KeyDown event: {0}.", macro.NameString());
            Console.WriteLine(msg);
        }
    }
}
