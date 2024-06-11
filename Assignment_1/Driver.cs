using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class Driver
    {
        static List<Appliance> appliances = new List<Appliance>();

        static void Main(string[] args)
        {
            LoadAppliances("..\\..\\Res\\appliances.txt");

            while (true)
            {
                Console.WriteLine("Welcome to Modern Appliances!");
                Console.WriteLine("How may we assist you?");
                Console.WriteLine("1 – Check out appliance");
                Console.WriteLine("2 – Find appliances by brand");
                Console.WriteLine("3 – Display appliances by type");
                Console.WriteLine("4 – Produce random appliance list");
                Console.WriteLine("5 – Save & exit");
                Console.Write("\nEnter option: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input, please enter a number between 1 and 5.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        CheckOutAppliance();
                        break;
                    case 2:
                        FindAppliancesByBrand();
                        break;
                    case 3:
                        DisplayAppliancesByType();
                        break;
                    case 4:
                        DisplayRandomAppliances();
                        break;
                    case 5:
                        SaveFile();
                        return;
                    default:
                        Console.WriteLine("Invalid option, please enter a number between 1 and 5.");
                        break;
                }
            }
        }

        static void LoadAppliances(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Data file not found.");
                return;
            }

            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                long itemNumber = long.Parse(parts[0]);
                string itemNumberStr = itemNumber.ToString();
                string brand = parts[1];
                int quantity = int.Parse(parts[2]);
                double wattage = int.Parse(parts[3]);
                string color = parts[4];
                double price = double.Parse(parts[5]);

                switch (itemNumberStr[0])
                {
                    case '1':
                        var doors = int.Parse(parts[6]);
                        var height = int.Parse(parts[7]);
                        var width = int.Parse(parts[8]);
                        appliances.Add(new Refrigerator(itemNumber, brand, quantity, wattage, color, price, doors, height, width));
                        break;
                    case '2':
                        var grade = parts[6];
                        var batteryVoltage = int.Parse(parts[7]);
                        appliances.Add(new Vaccum(itemNumber, brand, quantity, wattage, color, price, grade, batteryVoltage));
                        break;
                    case '3':
                        var capacity = double.Parse(parts[6]);
                        var roomType = parts[7];
                        appliances.Add(new Microwave(itemNumber, brand, quantity, wattage, color, price, capacity, roomType));
                        break;
                    case '4':
                    case '5':
                        var feature = parts[6];
                        var soundRating = parts[7];
                        appliances.Add(new Dishwasher(itemNumber, brand, quantity, wattage, color, price, feature, soundRating));
                        break;
                }
            }
        }

        static void CheckOutAppliance()
        {
            Console.Write("Enter the item number of an appliance: ");
            if (long.TryParse(Console.ReadLine(), out long itemNumber))
            {
                var appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);

                if (appliance != null)
                {
                    if (appliance.Quantity > 0)
                    {
                        appliance.Quantity--;
                        Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                    }
                    else
                    {
                        Console.WriteLine("The appliance is not available to be checked out.");
                    }
                }
                else
                {
                    Console.WriteLine("No appliances found with that item number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid item number format.");
            }
            Console.WriteLine();
        }

        
        static void FindAppliancesByBrand()
        {
            Console.Write("Enter the brand to search for: ");
            string brandInput = Console.ReadLine().ToLower();

            List<Appliance> matchingAppliances = new List<Appliance>();

            foreach (var appliance in appliances)
            {
                if (appliance.Brand.ToLower() == brandInput)
                {
                    matchingAppliances.Add(appliance);
                }
            }

            if (matchingAppliances.Count > 0)
            {
                Console.WriteLine("\nMatching Appliances:\n");
                foreach (var appliance in matchingAppliances)
                {
                    Console.WriteLine("\n" + appliance + "\n");
                }
            }
            else
            {
                Console.WriteLine("No matching appliances found.");
            }
        }

        static void DisplayAppliancesByType()
        {
            Console.WriteLine("Appliance Types:");
            Console.WriteLine("1 – Refrigerators");
            Console.WriteLine("2 – Vacuums");
            Console.WriteLine("3 – Microwaves");
            Console.WriteLine("4 – Dishwashers");
            Console.Write("\nEnter type of appliance:\n ");
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid input, please enter a number between 1 and 4.");
                return;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("\nEnter number of doors: 2 (double door), 3 (three doors), or 4 (four doors):");
                    if (int.TryParse(Console.ReadLine(), out int doorChoice) && (doorChoice >= 2 && doorChoice <= 4))
                    {
                        var fridgeResults = appliances.OfType<Refrigerator>().Where(r => r.Doors == doorChoice);
                        Console.WriteLine("\nMatching refrigerators:\n");
                        DisplayResults(fridgeResults);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option.");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high):");
                    if (int.TryParse(Console.ReadLine(), out int voltChoice) && (voltChoice == 18 || voltChoice == 24))
                    {
                        var vacuumResults = appliances.OfType<Vaccum>().Where(v => v.BatteryVoltage == voltChoice);
                        Console.WriteLine("\nMatching vacuums:\n");
                        DisplayResults(vacuumResults);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid voltage.");
                    }
                    break;
                case 3:
                    Console.WriteLine("\nRoom where the microwave will be installed: K (kitchen) or W (work site):");
                    string roomTypeChoice = Console.ReadLine().ToUpper();

                    if (roomTypeChoice == "K" || roomTypeChoice == "W")
                    {
                        var microwaveResults = appliances.OfType<Microwave>().Where(m => m.RoomType == roomTypeChoice);
                        Console.WriteLine("\nMatching microwaves:\n");
                        DisplayResults(microwaveResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid room type. Please enter K (kitchen) or W (work site).");
                    }
                    break;
                case 4:
                    Console.WriteLine("\nEnter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu (Quiet) or M (Moderate):");
                    string soundRatingChoice = Console.ReadLine().ToUpper();

                    if (new[] { "QT", "QR", "QU", "M" }.Contains(soundRatingChoice))
                    {
                        var dishwasherResults = appliances.OfType<Dishwasher>().Where(d => d.SoundRating == soundRatingChoice);
                        Console.WriteLine("\nMatching dishwashers:\n");
                        DisplayResults(dishwasherResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid sound rating. Please enter Qt (Quietest), Qr (Quieter), Qu (Quiet) or M (Moderate).");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }

        static void DisplayResults(IEnumerable<Appliance> results)
        {
            if (results == null || !results.Any())
            {
                Console.WriteLine("No matching appliances found.");
                return;
            }

            foreach (var appliance in results)
            {
                Console.WriteLine(appliance.ToString());
                Console.WriteLine();
            }
        }

        static void DisplayRandomAppliances()
        {
            Console.WriteLine("Enter number of appliances:");

            if (int.TryParse(Console.ReadLine(), out int number) && number > 0)
            {
                Random random = new Random();
                var randomAppliances = appliances.OrderBy(_ => random.Next()).Take(number);

                Console.WriteLine("\nRandom appliances:\n");
                foreach (var appliance in randomAppliances)
                {
                    Console.WriteLine(appliance);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Invalid number. Please enter a positive integer.");
            }
        }


        static void SaveFile()
        {
            string path = "..\\..\\res\\appliances.txt";
            var lines = new List<string>();

            foreach (var appliance in appliances)
            {
                if (appliance is Refrigerator r)
                {
                    lines.Add($"{r.ItemNumber};{r.Brand};{r.Quantity};{r.Wattage};{r.Color};{r.Price};{r.Doors};{r.Height};{r.Width}");
                }
                else if (appliance is Vaccum v)
                {
                    lines.Add($"{v.ItemNumber};{v.Brand};{v.Quantity};{v.Wattage};{v.Color};{v.Price};{v.Grade};{v.BatteryVoltage}");
                }
                else if (appliance is Microwave m)
                {
                    lines.Add($"{m.ItemNumber};{m.Brand};{m.Quantity};{m.Wattage};{m.Color};{m.Price};{m.Capacity};{m.RoomType}");
                }
                else if (appliance is Dishwasher d)
                {
                    lines.Add($"{d.ItemNumber};{d.Brand};{d.Quantity};{d.Wattage};{d.Color};{d.Price};{d.Feature};{d.SoundRating}");
                }
            }

            File.WriteAllLines(path, lines);
        }







    
}
}
