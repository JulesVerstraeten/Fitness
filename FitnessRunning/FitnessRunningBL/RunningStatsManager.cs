using FitnessRunningBL.Interface;
using FitnessRunningBL.Model;
using FitnessRunningBL.Model.Interval;

namespace FitnessRunningBL
{
    public class RunningStatsManager
    {
        private IFileProcessor fileProcessor;
        private string filePath = @"C:\Users\jules\Documents\HoGent\SEM2\Prog_Gevorderd_1\Opdrachten\Labo_1\insertRunning\ErrorLog1.txt";

        public RunningStatsManager(IFileProcessor fileProcessor)
        {
            this.fileProcessor = fileProcessor;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            } else
            {
                File.Create(filePath).Close();
            }
        }

        List<RunningStats> runningStatsList = new List<RunningStats>();

        public void ErrorLog(Exception ex)
        {
           
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    //writer.WriteLine("Call stack : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }
        public void ReadStats(string fileName)
        {
            List<string[]> readedRunningStats = fileProcessor.ReadRunningStats(fileName);
            int currentId = -1;

            foreach (string[] values in readedRunningStats)
            {
                int sessionNr = int.Parse(values[0]);
                DateTime date = DateTime.Parse(values[1].Replace("\'", ""));
                int clientId = int.Parse(values[2]);
                int trainingTime = int.Parse(values[3]);
                double averageSpeed = double.Parse(values[4]);
                int seqNr = int.Parse(values[5]);
                int timeInterval = int.Parse(values[6]);
                double speedInterval = double.Parse(values[7]);

                try
                {
                    IntervalStats iS = new IntervalStats(seqNr, timeInterval, speedInterval);
                    RunningStats rS = new RunningStats(sessionNr, date, clientId, trainingTime, averageSpeed, iS);

                    if (currentId == sessionNr - 1)
                    {
                        runningStatsList[currentId].IntervalStats.Add(iS);
                    }
                    else if (currentId != sessionNr - 1)
                    {
                        runningStatsList.Add(rS);
                        currentId += 1;

                    }
                }
                catch (Exception ex) { ErrorLog(ex); }
            }
        }
        public void AllesLatenZien()
        {
            foreach (var i in runningStatsList)
            {
                i.PrintRunningStats();
                i.PrintIntervalStats();
            }
        }
        public void ZoekOpClientID()
        {
            Console.Clear();
            int input;
            bool clientFound = false;

            while (true)
            {
                Console.Write("Klant nr: ");
                string i = Console.ReadLine();
                bool invoerOk = int.TryParse(i, out input);
                if (invoerOk)
                {
                    break;
                } else
                {
                    Console.WriteLine("Moet een cijfer zijn");
                }
            }

            Console.Clear();

            foreach (var s in runningStatsList)
            {
                if (s.ClientNr == input)
                {
                    clientFound = true;
                    s.PrintRunningStats();
                    s.PrintIntervalStats();
                }
            }

            if (!clientFound)
            {
                Console.WriteLine("Geen klant gevonden");
            }
        }
        public void ZoekOpDatum()
        {
            int dag;
            int maand;
            int jaar;

            while (true)
            {
                Console.Clear();

                Console.Write("Dag: ");
                string i = Console.ReadLine();
                bool invoerOk = int.TryParse(i, out dag);
                if (invoerOk && (dag < 0 || dag > 31) )
                {
                    Console.WriteLine("Dag moet gelijk zijn of binnen 1 en 31");
                } else if (invoerOk)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Moet een cijfer zijn");
                }
            }

            while (true)
            {
                Console.Clear();

                Console.Write("Maand: ");
                string i = Console.ReadLine();
                bool invoerOk = int.TryParse(i, out maand);
                if (invoerOk && (maand < 0 || maand > 12))
                {
                    Console.WriteLine("Dag moet gelijk zijn of binnen 1 en 12");
                }
                else if (invoerOk)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Moet een cijfer zijn");
                }
            }

            while (true)
            {
                Console.Clear();

                Console.Write("Jaar: ");
                string i = Console.ReadLine();
                bool invoerOk = int.TryParse(i, out jaar);
                if (invoerOk && (jaar < 2015 || jaar > 2030))
                {
                    Console.WriteLine("Dag moet gelijk zijn of binnen 2015 en 2030");
                }
                else if (invoerOk)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Moet een cijfer zijn");
                }
            }

            Console.Clear();

            foreach (var s in runningStatsList)
            {
                if (s.Date.Day == dag && s.Date.Month == maand && s.Date.Year == jaar)
                {
                    s.PrintRunningStats();
                    s.PrintIntervalStats();
                }
            }
        }
    }
}
