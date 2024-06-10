using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Read file

            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

            string fileName = string.Format("{0}res\\appliances.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

            string[] fileLines = File.ReadAllLines(fileName);

            /*
            foreach (string line in fileLines)
            {
                Console.WriteLine(line);
            }
            */

            //Parse file info

            List<appliance> applianceList = new List<appliance>();

            foreach (var line in fileLines)
            {
                if (line != "")
                {
                    string[] fileValues = line.Split(';');

                    string idType = fileValues[0].Substring(0, 1);

                    if (idType == "1")
                    {
                        //Fridge
                        Console.WriteLine("Fridge found!");

                        applianceList.Add(new refrigerator(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), int.Parse(fileValues[6]), int.Parse(fileValues[8]), int.Parse(fileValues[7])));
                    }
                    else if (idType == "2")
                    {
                        //Vacuum
                        Console.WriteLine("Vacuum found!");
                        int volage = int.Parse(fileValues[7]);
                        applianceList.Add(new vacuum(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), fileValues[6], int.Parse(fileValues[7])));
                    }
                    else if (idType == "3")
                    {
                        //Microwave
                        Console.WriteLine("Microwave found!");

                        applianceList.Add(new microwave(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), double.Parse(fileValues[6]), fileValues[7]));
                    }
                    else if (idType == "4" || idType == "5")
                    {
                        //Dishwasher
                        Console.WriteLine("Dishwasher found!");

                        applianceList.Add(new dishwasher(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), fileValues[6], (fileValues[7])));
                    }


                }
            }

            foreach (var i in applianceList)
            {
                Console.WriteLine(i.ToString());
            }







            Console.ReadLine();
        }
    }
}
