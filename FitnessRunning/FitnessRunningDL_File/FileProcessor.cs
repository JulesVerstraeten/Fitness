using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessRunningBL.Interface;

namespace FitnessRunningDL_File
{
    public class FileProcessor : IFileProcessor
    {
        public List<string[]> ReadRunningStats(string fileName)
        {
            try
            {
                //string sql = "insert into runningsession values(1,'2021-09-29 16:53:00',6,53,16.230377358490568,1,318,18.7);";
                string pattern = @"values\s*\((.*?)\)";
                List<string[]> stats = new List<string[]>();

                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var match = Regex.Match(line, pattern);
                        string[] values = new string[8];
                        if (match.Success)
                        {
                            // Remove parentheses
                            string valueString = match.Groups[1].Value;
                            values = SplitValues(valueString);
                        }
                        stats.Add(values);
                    }
                }
                //foreach (string[] values in stats)
                //{
                //    Console.WriteLine($"ID : {values[0]}");
                //    Console.WriteLine($"Date : {values[1]}");
                //    Console.WriteLine($"Client Number : {values[2]}");
                //    Console.WriteLine($"Training Time : {values[3]}");
                //    Console.WriteLine($"Average Speed : {values[4]}");
                //    Console.WriteLine($"Sequence Number : {values[5]}");
                //    Console.WriteLine($"Time Interval : {values[6]}");
                //    Console.WriteLine($"Speed Interval : {values[7]}");
                //    Console.WriteLine();
                //    Console.WriteLine("-------------------");
                //    Console.WriteLine();
                //}

                return stats;
            } catch(Exception ex) { throw new Exception($"FileProcessor-Stats", ex); }
        }

        static string[] SplitValues(string valueString)
        {
            var valuesList = new List<string>();
            bool insideQuotes = false;
            int lastSplit = 0;

            for (int i = 0; i < valueString.Length; i++)
            {
                if (valueString[i] == '\'') 
                {
                    insideQuotes = !insideQuotes;
                }
                else if (valueString[i] == ',' && !insideQuotes) 
                {
                    valuesList.Add(valueString.Substring(lastSplit, i - lastSplit).Trim());
                    lastSplit = i + 1;
                    insideQuotes = false;
                }
            }

            valuesList.Add(valueString.Substring(lastSplit).Trim());
            return valuesList.ToArray();
        }
    }
}
