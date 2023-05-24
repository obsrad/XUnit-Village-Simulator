using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace the_village_of_testing_petter_darsbo
{


    public class Village
    {

        //resource variables
        public int food { get; private set; }
        public int wood { get; private set; }
        public int metal { get; private set; }

        //object lists
        public List<Worker> workers = new List<Worker>();
        public List<Building> buildings = new List<Building>();
        public List<Building> projects = new List<Building>();
        public List<Worker> workersToRemove = new List<Worker>();

        //resource parameters
        public int metalPerDay;
        public int woodPerDay;
        public int foodPerDay;
        public int daysGone = 1;
        

        //constructor
        public Village(int food, int wood, int metal)
        {
            this.food = food;
            this.wood = wood;
            this.metal = metal;

            //create 3 houses to the village
            buildings.Add(new Building("House", 0, 0, 0, 0, true));
            buildings.Add(new Building("House", 0, 0, 0, 0, true));
            buildings.Add(new Building("House", 0, 0, 0, 0, true));


            //give 10 food to the village
            this.food += 10;

        }

        //methods
        public void AddWorker(string name, string occupation)
        {

            //check if there are enough houses for a new worker
            int totalHouses = buildings.Where(building => building.name == "House").Count();
            int totalWorkers = workers.Count();

            //should we exclude builders? otherwise might get stuck
            //int totalWorkers = workers.Where(worker => worker.occupation != "Builder").Count();

            //if totalhouses * 2 is less than or equal to totalWorkers
            if (totalHouses * 2 <= totalWorkers)
            {
                Console.WriteLine("Not enough houses to create a new worker.");
                return;
            }

            string assignment = name + " has been assigned as a: " + occupation + ".";

            //stringcomparison.ordinalignorecase will compare the strings and ignore DiFfEreNceS
            if (occupation.Equals("Farmer", StringComparison.OrdinalIgnoreCase))
            {
                Worker worker = new Worker(name, occupation, () => AddFood());
                Console.WriteLine(assignment);
                workers.Add(worker);
            }

            else if (occupation.Equals("Woodcutter", StringComparison.OrdinalIgnoreCase))
            {
                Worker worker = new Worker(name, occupation, () => AddWood());
                Console.WriteLine(assignment);
                workers.Add(worker);
            }

            else if (occupation.Equals("Miner", StringComparison.OrdinalIgnoreCase))
            {
                Worker worker = new Worker(name, occupation, () => AddMetal());
                Console.WriteLine(assignment);
                workers.Add(worker);
            }

            else if (occupation.Equals("Builder", StringComparison.OrdinalIgnoreCase))
            {
                Worker worker = new Worker(name, occupation, () => Build());
                Console.WriteLine(assignment);
                workers.Add(worker);
            }
            else
            {
                Console.WriteLine("Occupation not found.");
                return;
            }

        }

        public bool AddProject(string name)
        {
            //Dictionary to hold all the possibly building projects and their parameters
            //project names as keys, project costs as values
            Dictionary<string, int[]> projectCosts = new Dictionary<string, int[]>
            {
                //i.e. House, 5 Wood, 0 Metal, 3 Days to complete
                {    "House", new int[] { 5, 0, 3 } },
                {    "Woodmill", new int[] { 5, 1, 5 } },
                {    "Quarry", new int[] { 3, 5, 7 } },
                {    "Farm", new int[] { 5, 2, 5 } },
                {    "Castle", new int[] { 50, 50, 50 } }

            };

            int[] costs;
            

            //check if projectCosts contains a key with the given name
            //if its not found, return false
            if (!projectCosts.TryGetValue(name, out costs))
            {
                Console.WriteLine("Building " + name + " not found.");
                return false;
            }

            int woodCost = costs[0];
            int metalCost = costs[1];
            int daysToComplete = costs[2];

            if (wood < woodCost) 
            {
                Console.WriteLine("Not enough wood");
                return false;
            }

            if (metal < metalCost)
            {
                Console.WriteLine("Not enough metal");
                return false;
            }

            Building project = new Building(name);

            //set all properties of the project
            project.woodCost = woodCost;
            project.metalCost = metalCost;
            project.daysToComplete = daysToComplete;

            wood -= woodCost;
            metal -= metalCost;

            projects.Add(project);
            project.PrintProject();

            return true;
        }

        public bool AddBuilding(string name)
        {
            Building building = new Building(name);
            buildings.Add(building);

            building.PrintBuilding();
            return true;
        }

        public void Day()
        {
            
            foreach (Worker workers in workers)
            {
                BuryDead();
                workers.DoWork();
            }

            FeedWorkers();

            Console.WriteLine("");
            Console.WriteLine("One day has passed.");
            Console.WriteLine("");
            Console.WriteLine();

            daysGone++;
        }

        public void AddFood() 
        {
            int foodPerDay = 5;

            foreach (Building building in buildings)
            {
                if (building.name == "Farm")
                {
                    food += 10;
                }

            }

            Console.WriteLine($"Added {foodPerDay} food.");
            food += foodPerDay;
        }

        public void AddMetal()
        {
            int metalPerDay = 5;

            foreach (Building building in buildings)
            {
                if (building.name == "Quarry")
                {
                    metal += 2;
                }

            }

            Console.WriteLine($"Added {metalPerDay} metal.");
            metal += metalPerDay;
        }

        public void AddWood()
        {
            int woodPerDay = 5;

            foreach (Building building in buildings)
            {
                if (building.name == "Woodmill")
                {
                    wood += 2;
                }

            }

            Console.WriteLine($"Added {woodPerDay} wood.");
            wood += woodPerDay;
        }

        public void Build()
        {
            foreach (Building project in projects)
            {

                //if the daysWorkedOn is greater or equal to daysToComplete
                if (project.daysWorkedOn >= project.daysToComplete)
                {
                    //set complete to true
                    project.complete = true;
                    Console.WriteLine("");
                    Console.WriteLine(project.name + " project is completed");

                    //add project to the buildings list
                    AddBuilding(project.name);

                    //remove project from the projects list
                    projects.Remove(project);
                    Console.WriteLine(project.name + " removed from projects list");
                    Console.WriteLine("");
                    break;
                }
                else
                {
                    //take the first project(first index:[0] of the projects list and add +1 to daysWorkedOn
                    projects[0].daysWorkedOn++;
                    Console.WriteLine(projects[0].name + " project is being worked on.");
                }
            }
        }

        public void FeedWorkers()
        {
            //feed workers here
            //remove amount of food from village per worker in workers list

            foreach (Worker workers in workers)
            {
                if (food <= 0)
                {
                    workers.hungry = true;
                }

                else if (workers.alive == true)
                {
                    workers.Feed();
                    food--;
                    workers.daysHungry = 0;
                    workers.hungry = false;
                }
            }

        }

        public void BuryDead()
        {
            //remove all the workers from the list that have alive set to false
            //if worker.alive bool is false, add that *dead* worker to workersToRemove list.
            foreach (Worker worker in workers)
            {
                if (worker.alive == false)
                {
                    workersToRemove.Add(worker);
                }
            }

            //for every worker in workersToRemove list, remove them from normal workers list.
            foreach (Worker worker in workersToRemove)
            {
                workers.Remove(worker);
            }
        }
    }
}
