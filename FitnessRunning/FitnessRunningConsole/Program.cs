using FitnessRunningBL;
using FitnessRunningBL.Interface;
using FitnessRunningDL_File;

namespace FitnessRunningConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\jules\Documents\HoGent\SEM2\Prog_Gevorderd_1\Opdrachten\Labo_1\insertRunning\insertRunningTest3.txt";
            IFileProcessor fp = new FileProcessor();
            RunningStatsManager manager = new RunningStatsManager(fp);
            manager.ReadStats(fileName);

            while (true) 
            {
                Console.Clear();
                Console.WriteLine("1: Alles laten zien");
                Console.WriteLine("2: Zoek op klant id");
                Console.WriteLine("3: Zoek op datum");
                Console.Write("--> ");
                string keuze = Console.ReadLine();  

                switch (keuze)
                {
                    case "1":
                        manager.AllesLatenZien();
                        break;
                    case "2":
                        manager.ZoekOpClientID();
                        break;
                    case "3":
                        manager.ZoekOpDatum();
                        break;

                }

                Console.ReadKey();

            }
        }
    }
}
