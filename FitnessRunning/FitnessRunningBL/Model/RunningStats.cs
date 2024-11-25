using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessRunningBL.Model.Interval;

namespace FitnessRunningBL.Model
{
    internal class RunningStats
    {
        private int sessionNr;
        private DateTime date;
        private int clientNr;
        private int trainingTime;
        private double averageSpeed;
        public List<IntervalStats> IntervalStats = new List<IntervalStats>();

        public int SessionNr
        {
            get { return sessionNr; }
            set
            {
                if (value > 0)
                {
                    sessionNr = value;
                } else
                {
                    throw new Exception($"sessieNummer is {value}, and it must be greater than 0");
                }
            }
        }
        public int ClientNr
        {
            get { return clientNr; }
            set
            {
                if (value > 0)
                {
                    clientNr = value;
                }
                else
                {
                    throw new Exception($"clientNr is {value}, and it must be greater than 0");
                }
            }
        }
        public int TrainingTime
        {
            get { return trainingTime; }
            set
            {
                if (value >= 5 && value <= 180)
                {
                    trainingTime = value;
                } else
                {
                    throw new Exception($"trainingTime is {value}, and it must be at least 5 and max 180");
                }
                
            }
        }
        public double AverageSpeed
        {
            get { return averageSpeed; }
            set { 
                if (value >= 5 && value <= 180)
                {
                    averageSpeed = value;
                } else { throw new Exception($"averageSpeed is {value}, and it must be greater than 5 and smaller than 180"); }
            }
        }
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        public RunningStats(int sessionNr, DateTime date, int clientNr, int trainingTime, double averageSpeed, IntervalStats intervalStats)
        {
            this.SessionNr = sessionNr;
            this.Date = date;
            this.ClientNr = clientNr;
            this.TrainingTime = trainingTime;
            this.AverageSpeed = averageSpeed;
            this.IntervalStats.Add(intervalStats);
        }

        public void PrintRunningStats()
        {
            Console.WriteLine($"Sessie: {sessionNr} | {Date} | Klant: {clientNr} | Duur: {trainingTime} | Speed: {averageSpeed}");
        }

        public void PrintIntervalStats()
        {
            foreach ( IntervalStats intervalStats in IntervalStats ) 
            {
                intervalStats.PrintIntervalStats();
            }
        }
    }
}


