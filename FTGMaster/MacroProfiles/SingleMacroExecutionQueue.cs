using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGMaster.MacroProfiles
{
    class SingleMacroExecutionQueue
    {
        //SingleMacroExecutionQueue执行完成的回调
        public delegate void SingleMacroExecutionCompleteCallback(
            SingleMacroExecutionQueue queue,
            SingleMacro macro,
            bool success
            );

        public SingleMacroExecutionQueue(SingleMacro macro, int delayMilliseconds)
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
