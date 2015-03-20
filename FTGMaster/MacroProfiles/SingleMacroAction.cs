using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    public enum SingleMacroActionType
    {
        Press = 1,
        Lift = 2,
        Wait = 3
    }

    class SingleMacroAction
    {
        private SingleMacroActionType _type;
        private String _key;
        private int _delayMilliseconds;
        public SingleMacroAction(SingleMacroActionType type, String key)
        {
            _type = type;
            _key = key;
        }

        public SingleMacroAction(int delayMilliSeconds)
        {
            _type = SingleMacroActionType.Wait;
            _delayMilliseconds = delayMilliSeconds;
        }

        public SingleMacroActionType Type()
        {
            return _type;
        }

        public String Key()
        {
            return _key;
        }

        public int DelayMilliseconds()
        {
            return _delayMilliseconds;
        }

    }
}
