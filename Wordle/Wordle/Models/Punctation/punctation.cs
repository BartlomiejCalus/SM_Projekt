using System.Timers;
using System;

namespace Wordle.Models.Punctation
{
    public class punctation
    {
        private DateTime StartTime;
        private DateTime EndTime;
        private int durationTime;
        public punctation() { }

        public void startTime()
        {
            StartTime =  DateTime.Now; 
        }

        public void endTime()
        {
            TimeSpan timeSpan = DateTime.Now - StartTime;
            durationTime = (int)timeSpan.TotalSeconds;
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
