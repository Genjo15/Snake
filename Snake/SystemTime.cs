using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Snake
{
    class SystemTime
    {
        /********************************************* Declaration of variables *********************************************/

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern void GetLocalTime(ref SYSTEM_TIME systemTimeStruct);

        [StructLayout(LayoutKind.Sequential)]
        struct SYSTEM_TIME
        {
            public ushort _Year;
            public ushort _Month;
            public ushort _Weekday;
            public ushort _Day;
            public ushort _Hour;
            public ushort _Minute;
            public ushort _Mecond;
            public ushort _Millisecond;
        };

        private string _Time;
        private SYSTEM_TIME _SystemTime;

        /**************************************************** Constructor  ****************************************************/

        public SystemTime()
        {
            _SystemTime = new SYSTEM_TIME();
        }

        /*********************************************** Method for getting time ***********************************************/

        public String Get_Time()
        {
            GetLocalTime(ref _SystemTime);
            _Time = String.Format("{0:D2}:{1:D2}", _SystemTime._Hour, _SystemTime._Minute);
            return _Time;
        }
    }
}
