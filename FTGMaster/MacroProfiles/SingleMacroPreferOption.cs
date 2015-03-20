using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    class SingleMacroPreferOption
    {
        private String _lateKey;
        private String _beforeKey;

        //仅当lateKey晚于beforeKey按下时，会触发action
        public SingleMacroPreferOption(String lateKey, String beforeKey)
        {
            _lateKey = lateKey;
            _beforeKey = beforeKey;
        }

        public String LateKey()
        {
            return _lateKey;
        }

        public String BeforeKey()
        {
            return _beforeKey;
        }
    }
}
