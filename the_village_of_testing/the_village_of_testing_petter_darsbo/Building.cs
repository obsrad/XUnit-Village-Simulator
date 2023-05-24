using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_village_of_testing_petter_darsbo
{
    public class Building
    {
        //declaring variables
        public string name;
        public int woodCost;
        public int metalCost;
        public int daysWorkedOn = 1;
        public int daysToComplete;

        public bool complete = false;

        public Building(string name, int woodCost, int metalCost, int daysWorkedOn, int daysToComplete, bool complete)
        {
            this.name = name;
            this.woodCost = woodCost;
            this.metalCost = metalCost;
            this.daysWorkedOn = daysWorkedOn;
            this.daysToComplete = daysToComplete;
            this.complete = complete;

        }

        public Building(string name)
        {
            this.name = name;
        }

        public void PrintProject()
        {
            Console.WriteLine(name + " has been added to the project list.");
        }

        public void PrintBuilding()
        {
            Console.WriteLine(name + " has been added to the building list.");
        }

    }
}
