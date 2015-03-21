using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    class SingleMacroExcutionQueue
    {
        //SingleMacroExecutionQueue执行完成的回调
        public delegate void SingleMacroExecutionCompleteCallback(
            SingleMacroExcutionQueue queue,
            SingleMacro macro,
            bool success
            );

        public SingleMacroExcutionQueue(SingleMacro macro, int delayMilliseconds)
        {

        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
