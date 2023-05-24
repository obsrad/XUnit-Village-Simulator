using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_village_of_testing_petter_darsbo
{
    public class VillageDemo
    {
        static int ReadMenuChoice(string message)
        {
            Console.Write(message);

            int rmchoice;
            bool intTryParse = int.TryParse(Console.ReadLine(), out rmchoice);
            //we only have 3 menu options (1,2 or 3) as of now, so we dont want users to input an integer outside of those.
            if (intTryParse == true && rmchoice >= 1 && rmchoice <= 3)
            {
                return rmchoice;
            }
            else
            {
                Console.WriteLine("Input is not a valid number or is not one of the menu options.");
                return ReadMenuChoice(message);
            }
        }

        static string ReadUserChoice(string message)
        {
            Console.Write(message);
            var usrchoice = Console.ReadLine() ?? string.Empty;

            if (message.Contains("occupation", StringComparison.OrdinalIgnoreCase))
            {
                while (usrchoice.Any(char.IsDigit))
                {
                    Console.WriteLine("Occupation should not contain any numbers.");
                    Console.Write(message);
                    usrchoice = Console.ReadLine() ?? string.Empty;
                }
            }

            return usrchoice;
        }

        public void RunVillage()
        {
            //Initate village
            Village myVillage = new Village(0, 0, 0);

            //declare variables
            bool runMainMenu = true;

            //start the loop
            while (runMainMenu == true)
            {
                Console.Clear();

				Console.WriteLine("| Day " + myVillage.daysGone + " of building a village.  | \n"
					+ "| Wood: " + myVillage.wood + " | " + "Food: " + myVillage.food + " | " + "Metal: " + myVillage.metal + " |"
					+ "\n" + "\n"
					+ "Worker population: " + myVillage.workers.Count() + "\n");

				Console.WriteLine("Amount of Buildings: " + myVillage.buildings.Count());
                Console.WriteLine("Amount of Projects:  " + myVillage.projects.Count() + "\n");

                Console.WriteLine("Choose a task:");
                Console.WriteLine("");
                Console.WriteLine("1. Progress to the next day.");
                Console.WriteLine("2. Add a worker.");
                Console.WriteLine("3. Add a building project.");
                Console.WriteLine("");
                //wait for user input
                var menuChoice = ReadMenuChoice("Please enter a task by typing its number:");
                runMainMenu = false;

                while (menuChoice == 1)
                {
                    Console.Clear();

					Console.WriteLine("| Day " + myVillage.daysGone + " of building a village.  | \n"
						+ "| Wood: " + myVillage.wood + " | " + "Food: " + myVillage.food + " | " + "Metal: " + myVillage.metal + " |"
						+ "\n" + "\n"
						+ "Worker population: " + myVillage.workers.Count() + "\n");


					//call Day function, causing all workers in the workers list to DoWork and also adding +1 day to daysGone
					myVillage.Day();

                    Console.WriteLine("Press any key to return to main menu.");
                    Console.ReadKey(true);
                    runMainMenu = true;
                    break;
                }

                while (menuChoice == 2)
                {
                    Console.Clear();

					Console.WriteLine("| Day " + myVillage.daysGone + " of building a village.  | \n"
						+ "| Wood: " + myVillage.wood + " | " + "Food: " + myVillage.food + " | " + "Metal: " + myVillage.metal + " |"
						+ "\n" + "\n"
						+ "Worker population: " + myVillage.workers.Count() + "\n");


					Console.WriteLine("There are 4 different occupations to choose from:");
                    Console.WriteLine("'Woodcutter', 'Farmer', 'Miner', or 'Builder'.\n");

                    Console.WriteLine("Please enter a 'name' and 'occupation' for the worker.");
                    var nameChoice = ReadUserChoice("name:");
                    var occupChoice = ReadUserChoice("occupation:");

                    Console.WriteLine("");
                    myVillage.AddWorker(nameChoice, occupChoice);
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to return to main menu.");
                    Console.ReadKey(true);
                    runMainMenu = true;
                    break;
                }

                while (menuChoice == 3)
                {
                    Console.Clear();

					Console.WriteLine("| Day " + myVillage.daysGone + " of building a village.  | \n"
						+ "| Wood: " + myVillage.wood + " | " + "Food: " + myVillage.food + " | " + "Metal: " + myVillage.metal + " |"
						+ "\n" + "\n"
						+ "Worker population: " + myVillage.workers.Count() + "\n");

					Console.WriteLine("There are 5 different buildings to choose from:");
                    Console.WriteLine("'House', 'Woodmill', 'Quarry', 'Farm' or 'Castle'.\n");
                    var buildingChoice = ReadUserChoice("Choose a building by entering its name:");

                    Console.WriteLine("");
                    myVillage.AddProject(buildingChoice);
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to return to main menu.");
                    Console.ReadKey(true);
                    runMainMenu = true;
                    break;
                }
            }
        }


    }
}
