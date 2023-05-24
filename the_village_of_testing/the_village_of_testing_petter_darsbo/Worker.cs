using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_village_of_testing_petter_darsbo
{
    public class Worker
    {
        //declaring variables
        public string name;
        public string occupation;
        public bool hungry = false;
        public bool alive = true;
        public int daysHungry;

        //define delegates
        public delegate void DoWorkDelegate();

        //creating objects
        public DoWorkDelegate doWorkDelegate;

        //constructor
        //take DoWorkDelegate object as a parameter and assign it doWorkDelegate
        public Worker(string name, string occupation, DoWorkDelegate doWorkDelegate)
        {
            this.name = name;
            this.occupation = occupation;
            this.doWorkDelegate = doWorkDelegate;
        }


        //methods
        public void DoWork()
        {

            //check if worker is hungry

            if (daysHungry >= 4)
            {
                Console.WriteLine(name + " has died.");
                alive = false;
            }
            else if (hungry)
            {
                Console.WriteLine($"{this.name} is too hungry to work.");
                ++daysHungry;
            }
            else
            {
                doWorkDelegate();
            }
        }

        public void Feed()
        {
            Console.WriteLine($"{this.name} eats some food and is no longer hungry.");
        }
    }
}
