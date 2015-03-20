using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    class SingleMacroTriggerAfter
    {
        private List<SingleMacroTriggerAfterOption> _options;
        public SingleMacroTriggerAfter()
        {
            _options = new List<SingleMacroTriggerAfterOption>();
        }

        public void AddTriggerAfterOption(SingleMacroTriggerAfterOption option)
        {
            _options.Add(option);
        }

        public void AddTriggerAfterOption(String key, int delayMilliseconds)
        {
            SingleMacroTriggerAfterOption option = new SingleMacroTriggerAfterOption(key, delayMilliseconds);
            this.AddTriggerAfterOption(option);
        }

        public SingleMacroTriggerAfterOption[] TriggerAfterOptions()
        {
            SingleMacroTriggerAfterOption[] options = _options.ToArray();
            return options;
        }
    }

    class SingleMacroTriggerAfterOption
    {
        private String _key;
        private int _delayMilliseconds;
        public SingleMacroTriggerAfterOption(String key, int delayMilliseconds)
        {
            _key = key;
            _delayMilliseconds = delayMilliseconds;
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
