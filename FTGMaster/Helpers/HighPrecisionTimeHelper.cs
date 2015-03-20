using System;
using System.Runtime.InteropServices;

namespace FTGMaster.Helpers
{
    class HighPrecisionTimeHelper
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
          out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
          out long lpFrequency);

        private double _freq = 0;
        private long _startCPUClock = 0;

        private HighPrecisionTimeHelper(double freq)
        {
            _freq = freq;
            QueryPerformanceCounter(out _startCPUClock);
        }

        public double GetCurrentMilliseconds()
        {
            long currentCPUClock;
            QueryPerformanceCounter(out currentCPUClock);

            double result = (currentCPUClock - _startCPUClock) / _freq;//单位是秒
            result *= 1000;//转换为毫秒
            return result;
        }

        public static HighPrecisionTimeHelper GenerateHighPrecisionTimeHelper()
        {
            long freq;
            if (QueryPerformanceFrequency(out freq) == false)
            {
                return null;
            }
            HighPrecisionTimeHelper helper = new HighPrecisionTimeHelper((double)freq);
            return helper;
        }
    }
}
