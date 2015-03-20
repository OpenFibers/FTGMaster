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

        public FTGMasterWindow()//构造函数
        {
            InitializeComponent();

            //安装钩子，回调在KeyDownEventCallback
            _keyboardHook = HookFactory.CreateKeyboardHookExt();
            _keyboardHook.KeyDown += new KeyboardEventHandlerExt(KeyDownEventCallback);
            _keyboardHook.InstallHook();

            //读取当前的profile文件
            _currentProfile = MacroProfile.ProfileFromFileRelativePath("default.txt");
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
            kea.Cancel = true;
            string s = Keys.A.ToString();
            string msg = string.Format("\nKeyDown event: {0}.", kea.ToString());

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
            int scanCode = (int)DirectXHelper.DirectXKeyParser.DirectXKeyScanCodeFromString("Escape");
            SendInputHelper.SendInputHelper.DirectInputKeyDown(38);
        }
    }
}
