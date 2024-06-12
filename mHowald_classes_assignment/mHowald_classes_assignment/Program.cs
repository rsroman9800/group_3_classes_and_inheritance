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

        static List<appliance> applianceList = new List<appliance>();
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

            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to Modern Appliances!\nHow May We Assist You?");
                Console.WriteLine("1 - Check out appliance");
                Console.WriteLine("2 - Find appliances by brand");
                Console.WriteLine("3 - Display appliances by type");
                Console.WriteLine("4 - Produce random appliance list");
                Console.WriteLine("5 - Save & exit");
                Console.WriteLine("Enter option:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PurchaseAppliance();
                        break;
                    case "2":
                        SearchByBrand();
                        break;
                    case "3":
                        DisplayAppliancesByType();
                        break;
                    case "4":
                        DisplayRandomAppliances();
                        break;
                    case "5":
                        SaveFile();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }





            Console.ReadLine();
        }


        static void PurchaseAppliance()
        {
            Console.WriteLine("Enter the item number of an appliance");
            string itemSearch = Console.ReadLine();

            if (!long.TryParse(itemSearch, out long itemNumber)) // Checks if it can convert the user's search into the long format as the variable itemNumber. If it cannot, it prints an appropriate message
            {
                Console.WriteLine("No appliances found with that item number.");
                return;
            }

            var appliance = applianceList.FirstOrDefault(a => a.ItemNumber == itemNumber); // Search for the appliance in the list by item number 

            if (appliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
                Console.WriteLine();
                return;
            }

            if (appliance.Quantity > 0)
            {
                appliance.Quantity--;
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
                Console.WriteLine();
            }
        }

        static void SearchByBrand()
        {
            Console.WriteLine("Enter brand to search for");
            string itemSearch = Console.ReadLine().Trim();

            var results = applianceList.Where(a => a.Brand.ToLower() == itemSearch.ToLower());

            if (!results.Any())
            {
                Console.WriteLine("No appliances found for that brand.");
            }
            else
            {
                foreach (var appliance in results)
                {
                    Console.WriteLine(appliance);
                    Console.WriteLine();
                }
            }
        }

        static void DisplayAppliancesByType()
        {
            Console.WriteLine("Appliance Types");
            Console.WriteLine("1 - Refrigerators");
            Console.WriteLine("2 - Vacuums");
            Console.WriteLine("3 - Microwaves");
            Console.WriteLine("4 - Dishwashers");
            Console.WriteLine("Enter the type of appliance:");
            int typeChoice = int.Parse(Console.ReadLine());

            switch (typeChoice)
            {
                case 1:
                    Console.WriteLine("Enter number of doors: 2 (double door), 3 (three doors), or 4 (four doors)");
                    int doorChoice = int.Parse(Console.ReadLine());
                    if (doorChoice >= 2 && doorChoice <= 4)
                    {
                        var fridgeResults = applianceList.Where(a => a.GetType().Name == "refrigerator" && ((refrigerator)a).Doors == doorChoice);
                        Console.WriteLine("Matching refrigerators:");
                        DisplayResults(fridgeResults);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid door amount.");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high)");
                    int voltChoice = int.Parse(Console.ReadLine());
                    if (voltChoice == 18 || voltChoice == 24)
                    {
                        var vacuumResults = applianceList.Where(a => a.GetType().Name == "vacuum" && ((vacuum)a).Voltage == voltChoice);
                        Console.WriteLine("Matching vacuums:");
                        DisplayResults(vacuumResults);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid voltage.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Room where the microwave will be installed: K (kitchen) or W (work site):");
                    string installChoiceInput = Console.ReadLine().ToUpper();

                    if (installChoiceInput == "K")
                    {
                        var microwaveResults = applianceList.Where(a => a.GetType().Name == "microwave" && ((microwave)a).RoomType == installChoiceInput);
                        Console.WriteLine("Matching microwaves:");
                        DisplayResults(microwaveResults);
                    }
                    else if (installChoiceInput == "W")
                    {
                        var microwaveResults = applianceList.Where(a => a.GetType().Name == "microwave" && ((microwave)a).RoomType == installChoiceInput);
                        Console.WriteLine("Matching microwaves:");
                        DisplayResults(microwaveResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid room type. Please enter K (kitchen) or W (work site).");
                        return;
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu (Quiet) or M (Moderate)");
                    string soundChoiceInput = Console.ReadLine().ToUpper();

                    if (soundChoiceInput == "QT")
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else if (soundChoiceInput == "QR")
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else if (soundChoiceInput == "QU")
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else if (soundChoiceInput == "M")
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid sound rating. Please enter Qt (Quietest), Qr (Quieter), Qu (Quiet) or M (Moderate).");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }

        static void DisplayResults(IEnumerable<appliance> results) // Function for displaying the results of DisplayAppliancesByType
        {
            if (!results.Any())
            {
                Console.WriteLine("No matching appliances found.");
            }
            else
            {
                foreach (var appliance in results)
                {
                    Console.WriteLine(appliance);
                    Console.WriteLine();
                }
            }
        }

        static void DisplayRandomAppliances()
        {
            Console.WriteLine("Enter number of appliances:");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            Random rand = new Random();
            var randomAppliances = applianceList.OrderBy(a => rand.Next()).Take(number).ToList(); // Gives each appliance a random integer and sorts them in the random order generated into a list.

            foreach (var appliance in randomAppliances)
            {
                Console.WriteLine(appliance);
                Console.WriteLine();
            }
        }

        static void SaveFile()
        {
            var lines = new List<string>();
            string path = "..\\..\\res\\appliances.txt";

            foreach (var appliance in applianceList)
            {
                switch (appliance)
                {
                    case refrigerator r:
                        lines.Add($"{r.ItemNumber};{r.Brand};{r.Quantity};{r.Wattage};{r.Color};{r.Price};{r.Doors};{r.Height};{r.Width}");
                        break;
                    case vacuum v:
                        lines.Add($"{v.ItemNumber};{v.Brand};{v.Quantity};{v.Wattage};{v.Color};{v.Price};{v.Grade};{v.Voltage}");
                        break;
                    case microwave m:
                        lines.Add($"{m.ItemNumber};{m.Brand};{m.Quantity};{m.Wattage};{m.Color};{m.Price};{m.Capacity};{m.RoomType}");
                        break;
                    case dishwasher d:
                        lines.Add($"{d.ItemNumber};{d.Brand};{d.Quantity};{d.Wattage};{d.Color};{d.Price};{d.Feature};{d.SoundRating}");
                        break;
                }
            }
            File.WriteAllLines(path, lines);
        }
    }
}
