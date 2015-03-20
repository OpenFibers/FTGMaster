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
        private MacroProfile _currentProfile = null;
        private Dictionary<String, int> _keyPressedTimeDictionary = null;

        public FTGMasterWindow()//构造函数
        {
            InitializeComponent();

            //安装钩子，回调在KeyEventCallback
            _keyboardHook = HookFactory.CreateKeyboardHookExt();
            _keyboardHook.KeyDown += new KeyboardEventHandlerExt(KeyDownEventCallback);
            _keyboardHook.KeyUp += new KeyboardEventHandlerExt(KeyUpEventCallback);
            _keyboardHook.InstallHook();

            //读取当前的profile文件
            _currentProfile = MacroProfile.ProfileFromFileRelativePath("default.txt");

            //初始化
            _keyPressedTimeDictionary = new Dictionary<String, int>();
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


            int currentTime = 0;
            if (type == SingleMacroActionType.Press)
            {
                String keyString = kea.Key.ToString();
                DirectXKeyCode code = DirectXKeyParser.DirectXKeyScanCodeFromString(keyString);
                if (code != DirectXKeyCode.None)
                {
                    _keyPressedTimeDictionary.Add(keyString, currentTime);
                }
            }
            string s = Keys.A.ToString();
            string msg = string.Format("\nKeyDown event: {0}.", sender.ToString());

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
