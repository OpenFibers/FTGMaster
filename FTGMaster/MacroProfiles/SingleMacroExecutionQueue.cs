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

        private SingleMacro _macro;
        private int _delayMilliseconds;
        private int _currentActionIndex;
        private SingleMacroExecutionCompleteCallback _callback;
        private bool _started;

        public SingleMacroExecutionQueue(SingleMacro macro, int delayMilliseconds)
        {
            _macro = macro;
            _delayMilliseconds = delayMilliseconds;
            _currentActionIndex = 0;
            _started = false;
        }

        ~SingleMacroExecutionQueue()
        {
            this.Stop();
        }

        public void Start(SingleMacroExecutionCompleteCallback callback)
        {
            if (_started)
            {
                return;
            }
            _started = true;
            _callback = callback;

            this.CompletionCallback();
        }

        public void Stop()
        {

        }

        private void CompletionCallback()
        {
            _callback(this, _macro, true);
        }
    }
}
