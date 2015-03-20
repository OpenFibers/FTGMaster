using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    class SingleMacro
    {
        private String _nameString;
        private SingleMacroAction _triggerAction;//触发macro所需要的键盘事件
        private SingleMacroPreferOption _triggerPreferOption;//当某按键在另一个按键后面按下才会触发this SingleMacro的选项
        private SingleMacroTriggerAfter _triggerAfterOptions;//延时触发选项，用于自动目押功能
        private bool _shouldBlock;
        private List<SingleMacroAction> _actions;

        private SingleMacro(
            String name,
            SingleMacroAction triggerAction,
            SingleMacroPreferOption macroPreferOption,
            SingleMacroTriggerAfter triggerAfterOption,
            bool shouldBlock,
            List<SingleMacroAction> actions
            )
        {
            _nameString = name;
            _triggerAction = triggerAction;
            _triggerPreferOption = macroPreferOption;
            _triggerAfterOptions = triggerAfterOption;
            _shouldBlock = shouldBlock;
            _actions = actions;
        }

        //将Action代码解析成SingleMacroAction
        public static SingleMacro SingleMacroWithString(string macroString)
        {
            int leftBracketIndex = macroString.IndexOf("(");
            if (leftBracketIndex == -1)
            {
                return null;
            }
            int rightBracketIndex = macroString.IndexOf(")");
            if (rightBracketIndex == -1)
            {
                return null;
            }
            String nameString = null;
            SingleMacroAction triggerAction = null;
            SingleMacroPreferOption triggerPreferOption = null;
            SingleMacroTriggerAfter triggerAfterOption = null;
            bool shouldBlock = false;
            List<SingleMacroAction> actions = new List<SingleMacroAction>();

            //填入macro名字
            nameString = macroString.Substring(0, leftBracketIndex); 
            if (nameString.Length == 0)//name string不能为空
            {
                return null;
            }

            //解析 triggerAction、preferOption、afterOption、block
            String argString = macroString.Substring(leftBracketIndex + 1, rightBracketIndex - (leftBracketIndex + 1));
            argString = argString.ToLower();
            String[] args = argString.Split(',');
            foreach(String arg in args)
            {
                if (arg == null || arg.Length == 0)
                {
                    continue;
                }

                String trimmedArg = arg.Trim();
                if (trimmedArg.IndexOf("lift") == 0)//以lift开始
                {
                    String key = trimmedArg.Substring("lift".Length).Trim();
                    triggerAction = new SingleMacroAction(SingleMacroActionType.Lift, key);
                }
                else if (trimmedArg.IndexOf("press") == 0)//以press开始(press覆盖lift)
                {
                    String key = trimmedArg.Substring("press".Length).Trim();
                    triggerAction = new SingleMacroAction(SingleMacroActionType.Press, key);
                }
                else if (trimmedArg.IndexOf("prefer") == 0)//以prefer开始
                {
                    String trimmedOption = trimmedArg.Substring("prefer".Length).Trim();
                    String[] preferSettings = trimmedOption.Split('>');
                    if (preferSettings.Length == 2 &&
                        preferSettings[0].Length != 0 &&
                        preferSettings[1].Length != 0)
                    {
                        triggerPreferOption = new SingleMacroPreferOption(preferSettings[0], preferSettings[1]);
                    }
                }
                else if (trimmedArg.IndexOf("after") == 0)//以after开始
                {
                    String trimmedOptions = trimmedArg.Substring("after".Length).Trim();
                    String[] options = trimmedOptions.Split('|');
                    SingleMacroTriggerAfter parsingTriggerAfterOption = new SingleMacroTriggerAfter();
                    foreach (String option in options)
                    {
                        //检查option是否为空
                        if (option == null) { continue; }
                        String trimmedOption = option.Trim();
                        if (trimmedOption.Length == 0) { continue; }

                        //以空格切分，左为之前按下的按键，右为此按键按下后延时多久执行action
                        //实际执行时以最后按下的为准
                        String[] keyAndDelays = trimmedOption.Split(' ');
                        if (keyAndDelays.Length == 2 &&
                            keyAndDelays[0].Length != 0 &&
                            keyAndDelays[1].Length != 0)
                        {
                            String key = keyAndDelays[0];
                            String delayString = keyAndDelays[1];
                            int delayMilliseconds = int.Parse(delayString);
                            if (delayMilliseconds > 0)
                            {
                                parsingTriggerAfterOption.AddTriggerAfterOption(key, delayMilliseconds);
                            }
                        }
                    }
                    if (parsingTriggerAfterOption.TriggerAfterOptions().Length != 0)
                    {
                        triggerAfterOption = parsingTriggerAfterOption;
                    }
                }
                else if (trimmedArg.IndexOf("block") == 0)//以blocked开始
                {
                    shouldBlock = true;
                }
            }

            //解析actions
            String actionString = macroString.Substring(rightBracketIndex + 1, macroString.Length - (rightBracketIndex + 1));
            actionString = actionString.Trim().ToLower();
            String[] singleActionStrings = actionString.Split(';');
            foreach (String singleActionString in singleActionStrings)
            {
                SingleMacroAction action = SingleMacro.SingleMacroActionFromString(singleActionString);
                if (action != null)
                {
                    actions.Add(action);
                }
            }

            if (nameString.Length == 0 ||
                triggerAction == null ||
                actions.Count == 0)
            {
                return null;
            }

            SingleMacro macro = new SingleMacro(
                nameString,
                triggerAction, 
                triggerPreferOption,
                triggerAfterOption, 
                shouldBlock, 
                actions
                );

            return macro;
        }

        //每一条command，如press a; wait 15; lift a;解析成action
        private static SingleMacroAction SingleMacroActionFromString(String singleMacroActionString)
        {
            SingleMacroAction action = null;
            String trimmedActionString = singleMacroActionString.Trim().ToLower();
            if (trimmedActionString.IndexOf("lift") == 0)//以lift开始
            {
                String key = trimmedActionString.Substring("lift".Length).Trim();
                action = new SingleMacroAction(SingleMacroActionType.Lift, key);
            }
            else if (trimmedActionString.IndexOf("press") == 0)//以press开始(press覆盖lift)
            {
                String key = trimmedActionString.Substring("press".Length).Trim();
                action = new SingleMacroAction(SingleMacroActionType.Press, key);
            }
            else if (trimmedActionString.IndexOf("wait") == 0)//以wait开始
            {
                String delayString = trimmedActionString.Substring("wait".Length).Trim();
                int delayMilliseconds = int.Parse(delayString);
                if (delayMilliseconds == 0)
                {
                    return null;
                }
                action = new SingleMacroAction(delayMilliseconds);
            }
            return action;
        }

        //readonly方法

        public String NameString()
        {
            return _nameString;
        }

        public SingleMacroAction TriggerAction()
        {
            return _triggerAction;
        }

        public SingleMacroPreferOption TriggerPreferOption()
        {
            return _triggerPreferOption;
        }

        public SingleMacroTriggerAfter TriggerAfterObject()
        {
            return _triggerAfterOptions;
        }

        public SingleMacroTriggerAfterOption[] TriggerAfterOptions()
        {
            SingleMacroTriggerAfterOption[] options = _triggerAfterOptions.TriggerAfterOptions();
            return options;
        }

        public bool ShouldBlock()
        {
            return _shouldBlock;
        }

        public SingleMacroAction[] Actions()
        {
            SingleMacroAction[] actions = _actions.ToArray();
            return actions;
        }

        //封装的用于判断macro是否应该被执行的static方法
        public static bool ShouldTriggerAction(
            String currentKeyString,//当前事件的key的string
            SingleMacroActionType currentKeyActionType,//当前是按键按下还是抬起事件
            double currentTime,//当前时间
            SingleMacro actionToCheck,//需要检查是否执行的action
            Dictionary<String, double> pressedKeyTimeDictionary,//之前记录的按下按键的时间
            Dictionary<String, double> liftedKeyTimeDictionary,//之前记录的弹起按键的时间
            out SingleMacroTriggerAfterOption selectedTriggerAfterOption//生效的after选项（自动目押）
            )
        {
            if (actionToCheck.TriggerAction().Key() != currentKeyString)
            {
                selectedTriggerAfterOption = null;
                return false;
            }

            if (actionToCheck.TriggerAction().Type() != currentKeyActionType)
            {
                selectedTriggerAfterOption = null;
                return false;
            }

            //如果有prefer设置，检查是否满足prefer设置
            if (actionToCheck.TriggerPreferOption() != null)
            {
                String beforeKeyString = actionToCheck.TriggerPreferOption().BeforeKey();
                String afterKeyString = actionToCheck.TriggerPreferOption().LateKey();
                double beforeKeyTime = 0;
                double afterKeyTime = 0;
                if (pressedKeyTimeDictionary.ContainsKey(beforeKeyString))
                {
                    beforeKeyTime = pressedKeyTimeDictionary[beforeKeyString];
                }
                if (pressedKeyTimeDictionary.ContainsKey(afterKeyString))
                {
                    afterKeyTime = pressedKeyTimeDictionary[afterKeyString];
                }
                if (afterKeyTime <= beforeKeyTime)//包括afterKey没按下，或afterKey在beforeKey之前按下的情况
                {
                    selectedTriggerAfterOption = null;
                    return false;//设置了prefer选项，此次事件不满足preferOption， 不执行
                }
            }

            //检查after设置（自动目押）
            SingleMacroTriggerAfterOption nearestOption = null;
            SingleMacroTriggerAfterOption[] options = actionToCheck.TriggerAfterOptions();
            foreach (SingleMacroTriggerAfterOption option in options)
            {
                String optionKey = option.Key();
                //TODO:选出nearestOption
            }

            selectedTriggerAfterOption = null;
            return true;
        }
    }
}
