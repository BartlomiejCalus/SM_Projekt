using System.Timers;
using System;

namespace Wordle.Models.Punctation
{
    public class punctation
    {
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
        public TimeSpan durationSpan { get; private set; }
        private int durationTime;
        public punctation() { }

        public void startTime()
        {
            StartTime =  DateTime.Now; 
        }

        public void endTime()
        {
            EndTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromTicks(EndTime.Ticks) - TimeSpan.FromTicks(StartTime.Ticks);
            durationTime = timeSpan.Seconds;
            durationSpan = timeSpan;
        }

        public int Stats(int row) { 

            if(row <= 0 || row > 5) return 0;
            int positionPoints = 1000;

            for (int i = 1; i < row; i++)
            {
                positionPoints -= 200;
            }
                
            int result = (durationTime * 45) + positionPoints;

            return result;
        }
    }
}
