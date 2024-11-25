using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessRunningBL.Model.Interval
{
    internal class IntervalStats
    {
        private int seqNr;
        private int timeInterval;
        private double speedInterval;

        public int SeqNr
        {
            get { return seqNr; }
            set
            {
                if (value > -1)
                {
                    seqNr = value;
                }
                else { throw new Exception($"seqNr is {value} and it must be a positive integer"); }
            }
        }
        public int TimeInterval
        {
            get { return timeInterval; }
            set
            {
                if (value > 5 && value < 10800)
                {
                    timeInterval = value;
                }
                else { throw new Exception($"timeInterval is {value}, it must be greater than 5 and smaller than 10800"); }
            }
        }
        public double SpeedInterval
        {
            get { return speedInterval; }
            set
            {
                if (value >= 5 && value <= 22)
                {
                    speedInterval = value;
                }
                else { throw new Exception($"speedInterval is {value}, it must be greater than 5 and smaller than 22"); }
            }
        }

        public IntervalStats(int seqNr, int timeInterval, double speedInterval)
        {
            this.SeqNr = seqNr;
            this.SpeedInterval = speedInterval;
            this.TimeInterval = timeInterval;
        }
        public void PrintIntervalStats()
        {
            Console.WriteLine($"SeqNr: {seqNr} | Snelheid: {speedInterval} | Tijd: {timeInterval}");
        }
    }
}
