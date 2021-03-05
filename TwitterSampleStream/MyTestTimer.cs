using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace TwitterSampleStreamAPI
{
    public static class MyTestTimer
    {
        private static Timer myTimer = new Timer(int.MaxValue);
        public static List<DateTime> TimeList { get; set; } = new List<DateTime>();

        public static string GetTime() {
            return @$"{TimeList.LastOrDefault().ToString()} || {TimeList.Count}";
        }
        public static async Task InitiateTimer()
        {
            myTimer.Interval = 1000;
            myTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            myTimer.Start();
        }

        public static void OnTimedEvent(object source, ElapsedEventArgs args)
        {
            MyTestTimer.TimeList.Add(DateTime.Now);
            if (TimeList.Count >= 60) {
                //save it to db.
                TimeList.Clear();
            }
        }
    }
}
