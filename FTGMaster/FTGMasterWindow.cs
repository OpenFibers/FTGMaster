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
            kea.Cancel = true;

            //读取当前按下时间
            double currentTime = _timeHelper.GetCurrentMilliseconds();

            //如果是下压事件，记录到dictionary中，用于后面的条件判断
            if (type == SingleMacroActionType.Press)
            {
                String keyString = kea.Key.ToString();
                DirectXKeyCode code = DirectXKeyParser.DirectXKeyScanCodeFromString(keyString);
                if (code != DirectXKeyCode.None)
                {
                    _keyPressedTimeDictionary[keyString] = currentTime;
                }
            }


            string msg = string.Format("\nKeyDown event: {0}.", currentTime.ToString());

            //
            // Prevent the 'A' key from reaching any applications whatsoever
            // except this callback and other system hook applications.
            //
            if (kea.Key == Keys.A)
            {
                msg += " [BLOCKED]";

                this.aaa();
            }
            Console.Write(msg);
        }

        private void aaa()
        {
            Console.WriteLine("\\");
            int scanCode = (int)DirectXKeyParser.DirectXKeyScanCodeFromString("Escape");
            SendInputHelper.DirectInputKeyDown(38);
        }
    }
}
