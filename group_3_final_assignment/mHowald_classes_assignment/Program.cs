using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    /// Author: Mace Howald, Nasratullah Asadi, Roman Sorokin
    /// Date: 06/11/2024
    /// 
    /// Program Description:
    /// This program simulates an appliance management system for a store. It allows users to:
    /// 1. Check out an appliance by item number.
    /// 2. Search for appliances by brand.
    /// 3. Display appliances by type with specific criteria.
    /// 4. Display a random list of appliances.
    /// 5. Save the current appliance data to a file and exit.
    /// 
    /// Inputs:
    /// - User inputs for selecting menu options.
    /// - User inputs for specific categories (e.g., item number, brand name, appliance type, etc.).
    /// 
    /// Processing:
    /// - Load appliance data from a text file.
    /// - Perform various methods based on user inputs.
    /// - Update appliance data (e.g., reducing quantity upon checkout).
    /// - Display results based on the user's inputs.
    /// 
    /// Outputs:
    /// - Console outputs to display information and results based on user inputs.
    /// - Updated appliance data saved back to the text file upon exit.
    public class Program
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

            
            // Separating each line by appliance category and adding it to the appliances list using its approprirate class
            foreach (var line in fileLines)
            {
                if (line != "")
                {
                    string[] fileValues = line.Split(';');

                    string idType = fileValues[0].Substring(0, 1);

                    if (idType == "1")
                    {
                        //Fridge

                        applianceList.Add(new refrigerator(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), int.Parse(fileValues[6]), int.Parse(fileValues[8]), int.Parse(fileValues[7])));
                    }
                    else if (idType == "2")
                    {
                        //Vacuum
                        int volage = int.Parse(fileValues[7]);
                        applianceList.Add(new vacuum(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), fileValues[6], int.Parse(fileValues[7])));
                    }
                    else if (idType == "3")
                    {
                        //Microwave

                        applianceList.Add(new microwave(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), double.Parse(fileValues[6]), fileValues[7]));
                    }
                    else if (idType == "4" || idType == "5")
                    {
                        //Dishwasher

                        applianceList.Add(new dishwasher(long.Parse(fileValues[0]), fileValues[1], int.Parse(fileValues[2]), double.Parse(fileValues[3]), fileValues[4], double.Parse(fileValues[5]), fileValues[6], (fileValues[7])));
                    }


                }
            }

            // Creating a while true statement to keep the code running until the user enters 5
            bool running = true;
            while (running)
            {
                // Prints the approprirate menu
                Console.WriteLine("Welcome to Modern Appliances!\nHow may we assist you?");
                Console.WriteLine("1 - Check out appliance");
                Console.WriteLine("2 - Find appliances by brand");
                Console.WriteLine("3 - Display appliances by type");
                Console.WriteLine("4 - Produce random appliance list");
                Console.WriteLine("5 - Save & exit");
                Console.WriteLine("Enter option:");
                string choice = Console.ReadLine();

                // Switch statement to run the appropriate method based on the user's input
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
                        running = false; // Ends the continuous loop and exits the program.
                        break;
                    default:
                        Console.WriteLine("Invalid choice."); // Statement written in case the user enters an invalid choice
                        break;
                }
            }
            Console.ReadLine();
        }


        static void PurchaseAppliance()
        {
            Console.WriteLine("Enter the item number of an appliance");
            string itemSearch = Console.ReadLine();

            if (!long.TryParse(itemSearch, out long itemNumber)) // Checks if it can convert the user's search into the long format as the variable itemNumber. If it cannot, it prints an appropriate message. This ensures the user does not enter a non-numeric entry.
            {
                Console.WriteLine("No appliances found with that item number.");
                return;
            }

            var appliance = applianceList.FirstOrDefault(a => a.ItemNumber == itemNumber); // Search for the appliance in the list by item number 

            if (appliance == null)
            {
                Console.WriteLine("No appliances found with that item number."); // If no appliance is matched, it prints an appropriate message.
                Console.WriteLine();
                return;
            }

            if (appliance.Quantity > 0)
            {
                appliance.Quantity--; // Checks out the proper appliance by reducing its quantity by 1
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out."); // If the item's quantity is at 0, the item would not be available to be checked out and an appropriate message is written.
                Console.WriteLine();
            }
        }

        static void SearchByBrand()
        {
            Console.WriteLine("Enter brand to search for:");
            string itemSearch = Console.ReadLine().Trim();

            var results = applianceList.Where(a => a.Brand.ToLower() == itemSearch.ToLower()); // Checks if the brand entered by the user matches any brands in the appliances list

            if (!results.Any())
            {
                Console.WriteLine("No appliances found for that brand."); // Prints an appropriate message if no matches are found
            }
            else
            {
                Console.WriteLine("Matching Appliances:"); // Prints all matching appliances based on the brand.
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
            Console.WriteLine("Enter type of appliance:");
            int typeChoice = int.Parse(Console.ReadLine()); // Converts the user's entry into an integer

            switch (typeChoice)
            {
                case 1:
                    Console.WriteLine("Enter number of doors: 2 (double door), 3 (three doors), or 4 (four doors)");
                    int doorChoice = int.Parse(Console.ReadLine());
                    if (doorChoice >= 2 && doorChoice <= 4)
                    {
                        var fridgeResults = applianceList.Where(a => a.GetType().Name == "refrigerator" && ((refrigerator)a).Doors == doorChoice); // Checks if the door choice entered matches the number of doors on the refrigerators.
                        Console.WriteLine("Matching refrigerators:");
                        DisplayResults(fridgeResults); // Displays the matching fridges
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid door amount."); // Prints an appropriate message if the door amount is not between 2-4.
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high)"); 
                    int voltChoice = int.Parse(Console.ReadLine());
                    if (voltChoice == 18 || voltChoice == 24)
                    {
                        var vacuumResults = applianceList.Where(a => a.GetType().Name == "vacuum" && ((vacuum)a).Voltage == voltChoice); // Checks if the battery voltage entered matches the vacuums in the list.
                        Console.WriteLine("Matching vacuums:");
                        DisplayResults(vacuumResults); // Displays the matching vacuums
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid voltage."); // Prints an appropriate message if the voltage entered is not 18 or 24.
                    }
                    break;
                case 3:
                    Console.WriteLine("Room where the microwave will be installed: K (kitchen) or W (work site):");
                    string installChoiceInput = Console.ReadLine().ToUpper(); // Converts the user's choice to uppercase.

                    if (installChoiceInput == "K")
                    {
                        var microwaveResults = applianceList.Where(a => a.GetType().Name == "microwave" && ((microwave)a).RoomType == installChoiceInput); // Checks if the microwave room type matches the microwaves in the list.
                        Console.WriteLine("Matching microwaves:");
                        DisplayResults(microwaveResults); 
                    }
                    else if (installChoiceInput == "W")
                    {
                        var microwaveResults = applianceList.Where(a => a.GetType().Name == "microwave" && ((microwave)a).RoomType == installChoiceInput); // Checks if the microwave room type matches the microwaves in the list.
                        Console.WriteLine("Matching microwaves:");
                        DisplayResults(microwaveResults); // Displays the matching microwaves
                    }
                    else
                    {
                        Console.WriteLine("Invalid room type. Please enter K (kitchen) or W (work site)."); // Prints an appropriate message if the microwave room type entered is not K or W.
                        return;
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu (Quiet) or M (Moderate)");
                    string soundChoiceInput = Console.ReadLine().ToUpper();

                    if (soundChoiceInput == "QT") // Displays dishwashers that are the Quietest setting
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else if (soundChoiceInput == "QR") // Displays dishwashers that are the Quieter setting
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else if (soundChoiceInput == "QU") // Displays dishwashers that are the Quiet setting
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else if (soundChoiceInput == "M") // Displays dishwashers that are in the Moderate setting
                    {
                        var dishwasherResults = applianceList.Where(a => a.GetType().Name == "dishwasher" && ((dishwasher)a).SoundRating == soundChoiceInput);
                        Console.WriteLine("Matching dishwashers:");
                        DisplayResults(dishwasherResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid sound rating. Please enter Qt (Quietest), Qr (Quieter), Qu (Quiet) or M (Moderate)."); // Writes appropriate messsage if the option does not match the 4 sound ratings.
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option."); // Default message printed for the switch case
                    break;
            }
        }

        static void DisplayResults(IEnumerable<appliance> results) // Function for displaying the results of DisplayAppliancesByType for easier code readability where it converts the results into an enumerable data type
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
            if (!int.TryParse(Console.ReadLine(), out int number)) // Reads the user's entry and if it is non-numeric, print the appropriate message
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            Random rand = new Random(); // Uses the random class to sort random appliances
            var randomAppliances = applianceList.OrderBy(a => rand.Next()).Take(number).ToList(); // Gives each appliance a random integer and sorts them in the random order. Then it takes the number entered by the users and generates that number of applianes into a list.

            Console.WriteLine("Random appliances:");
            foreach (var appliance in randomAppliances) // Displays each random appliance
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
