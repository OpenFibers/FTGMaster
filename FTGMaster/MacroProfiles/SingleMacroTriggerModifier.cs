using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    class SingleMacroTriggerModifier
    {
        private List<String> _modifierKeyStrings;

        private SingleMacroTriggerModifier()
        {
            _modifierKeyStrings = new List<String>();
        }

        private void AddKeyString(String keyString)
        {
            _modifierKeyStrings.Add(keyString);
        }

        public static SingleMacroTriggerModifier SingleMacroTriggerModifierFromOptionString(String optionString)
        {
            if (optionString == null)
            {
                return null;
            }
            if (optionString.Length == 0)
            {
                return null;
            }

            SingleMacroTriggerModifier modifier = new SingleMacroTriggerModifier();
            String[] singleOptionStrings = optionString.Split('&');
            foreach (String singleOptionString in singleOptionStrings)
            {
                if (singleOptionString != null && 
                    singleOptionString.Length != 0)
                {
                    modifier.AddKeyString(singleOptionString);
                }
            }
            return modifier;
        }
    }
}
