using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using FTGMaster.MacroManagerNamespace;


//TODO：补齐脚本注释

namespace FTGMaster
{
    public partial class FTGMasterWindow : Form
    {
        private MacroManager _macroManager = null;
        public FTGMasterWindow()//构造函数
        {
            InitializeComponent();
            _macroManager = new MacroManager();
            _macroManager.LoadProfileWithRelativePath("demo_script.txt");
            _macroManager.InstallHook(MacroManagerKeyEventUpdatedCallback);
        }

        private void FTGMasterWindowForm_Load(object sender, EventArgs e)//Form load
        {
        }

        private void MacroManagerKeyEventUpdatedCallback(MacroManager manager, String keyEventString)
        {
            Debug.WriteLine(keyEventString);
        }
    }
}
