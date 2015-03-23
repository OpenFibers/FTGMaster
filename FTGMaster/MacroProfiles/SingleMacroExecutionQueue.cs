using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTGMaster.Helpers;

namespace FTGMaster.MacroProfiles
{
    class SingleMacroExecutionQueue : IDisposable
    {
        #region IDisposable Code
        //IDisposable code
        private bool disposed = false;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Stop();
                    _timer.Dispose();
                }
            }
            disposed = true;
        }

        ~SingleMacroExecutionQueue()
        {
            this.Dispose(false);
        }
        #endregion

        //SingleMacroExecutionQueue执行完成的回调
        public delegate void SingleMacroExecutionCompleteCallback(
            SingleMacroExecutionQueue queue,
            SingleMacro macro,
            bool success
            );

        private SingleMacro _macro;
        private SingleMacroAction[] _macroActions;
        private int _delayMilliseconds;
        private int _currentActionIndex;
        private SingleMacroExecutionCompleteCallback _callback;
        private bool _started;
        private MMTimer _timer;

        public SingleMacroExecutionQueue(SingleMacro macro, int delayMilliseconds)
        {
            _macro = macro;
            _macroActions = _macro.Actions();//由于Actions()每次调用会浅拷贝生成array，这里缓存起来
            _delayMilliseconds = delayMilliseconds;
            _currentActionIndex = 0;
            _started = false;
            _timer = new MMTimer();
            _timer.Timer += TimerCallback;
        }

        public void Start(SingleMacroExecutionCompleteCallback callback)
        {
            if (_started)
            {
                return;
            }
            _started = true;
            _callback = callback;

            if (_delayMilliseconds != 0)
            {
                _timer.Start((uint)_delayMilliseconds, false);
            }
            else
            {
                this.ExecuteNextAction();
            }
        }

        public void Stop()
        {
            _started = false;
            _timer.Stop();
        }


        private void ExecuteNextAction()
        {
            if (_macroActions.Length > _currentActionIndex)
            {
                SingleMacroAction action = _macroActions[_currentActionIndex];
                _currentActionIndex++;
                switch (action.Type())
                {
                    case SingleMacroActionType.Press:
                        {
                            String keyString = action.Key();
                            DirectXKeyCode keyCode = DirectXKeyParser.DirectXKeyScanCodeFromString(keyString);
                            SendInputHelper.DirectInputKeyDown((int)keyCode);
                            this.ExecuteNextAction();
                        }
                        break;
                    case SingleMacroActionType.Lift:
                        {
                            String keyString = action.Key();
                            DirectXKeyCode keyCode = DirectXKeyParser.DirectXKeyScanCodeFromString(keyString);
                            SendInputHelper.DirectInputKeyUp((int)keyCode);
                            this.ExecuteNextAction();
                        }
                        break;
                    case SingleMacroActionType.Wait:
                        _timer.Start((uint)action.DelayMilliseconds(), false);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.CompletionCallback();
            }

        }

        private void CompletionCallback()
        {
            _started = false;
            _callback(this, _macro, true);
        }

        void TimerCallback(object sender, EventArgs e)
        {
            this.ExecuteNextAction();
        }
    }
}
