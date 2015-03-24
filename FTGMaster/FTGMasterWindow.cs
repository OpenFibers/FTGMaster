using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTGMaster.MacroManagerNamespace;


//TODO：补齐脚本注释
//TODO: 增加manager
//TODO:脚本录制

namespace FTGMaster
{
    public partial class FTGMasterWindow : Form
    {
        private MacroManager _macroManager = null;
        public FTGMasterWindow()//构造函数
        {
            InitializeComponent();
            _macroManager = new MacroManager();
        }

        private void FTGMasterWindowForm_Load(object sender, EventArgs e)//Form load
        {
        }
    }
}
