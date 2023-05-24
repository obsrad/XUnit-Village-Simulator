using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace the_village_of_testing_petter_darsbo.Tests
{
    
    public class VillageTest
    {

        [Fact]
        public void Add1Worker()
        {
            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedWorker = 1;

            //When
            village.AddWorker(name, occupation);
            village.Day();

            //Then
            Assert.Equal(expectedWorker, village.workers.Count);
        }

        [Fact]
        public void Add2Worker()
        {
            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedWorker = 2;

            //When
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);

            //Then
            Assert.Equal(expectedWorker, village.workers.Count);
        }

        [Fact]
        public void Add3Worker()
        {
            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedWorker = 3;

            //When
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);

            //Then
            Assert.Equal(expectedWorker, village.workers.Count);
        }

        [Fact]
        public void AddWorker_ButNotEnoughHouses()
        {
            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedWorkers = 6;

            //When
            village.AddWorker(name, occupation);    
            village.AddWorker(name, occupation);    
            village.AddWorker(name, occupation); 
            
            village.AddWorker(name, occupation);    
            village.AddWorker(name, occupation);    
            village.AddWorker(name, occupation);

            //7th wont be added
            village.AddWorker(name, occupation);


            //Then
            Assert.Equal(expectedWorkers, village.workers.Count);
        }

        [Fact]
        public void Add1Worker_AndThenCallDay()
        {
            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedTotalWood = 5;

            //When
            village.AddWorker(name, occupation);
            village.Day();

            //Then
            Assert.Equal(expectedTotalWood, village.wood);
        }

        [Fact]
        public void VillageDay_ButNoWorkers()
        {
            //since in my village we start at day 1, calling day() will do nothing but adding +1 to daysGone

            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedDaysGone = 2;

            //When
            village.Day();

            //Then
            Assert.Equal(expectedDaysGone, village.daysGone);
        }

        [Fact]
        public void VillageDay_WithWorkersEating()
        {
            //we start with 10 food, every worker eats 1 food every day

            //Given
            List<Worker> workers = new List<Worker>();
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedFood = 8;

            //When
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.Day();

            //Then
            Assert.Equal(expectedFood, village.food);
        }

        [Fact]
        public void VillageDay_NotEnoughFood()
        {
            //Given
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";
            int expectedWood = 105;

            //every woodcutter will add +5 wood. 11 workers = 55 wood
            //then we call day and all food will be gone
            //they will still collect because they get hungry after 1 day of not eating
            //on the last day call - no wood will be gathered

            //When
            village.AddBuilding("House");
            village.AddBuilding("House");
            village.AddBuilding("House");

            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);

            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);

            //
            village.AddWorker(name, occupation);

            village.Day();
            village.Day();
            village.Day();

            //Then
            Assert.Equal(expectedWood, village.wood);
        }

        [Fact] 

        public void AddAffordableProject()
        {
            //Given
            Village village = new Village(0, 0, 0);
            string name = "Samuel";
            string occupation = "Woodcutter";

            string name2 = "Bob";
            string occupation2 = "Builder";
            int expectedWoodAfterBuilding = 5;

            //When
            //+10 wood
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);
            village.Day();

            village.AddWorker(name2, occupation2);
            //-5 wood
            village.AddProject("House");


            //Then
            Assert.Equal(expectedWoodAfterBuilding, village.wood);
        }

        [Fact]  

        public void AddNotAffordableProject()
        {
            //Given
            Village village = new Village(0, 0, 0);
            string name = "Bob";
            string occupation = "Builder";

            string name2 = "Samuel";
            string occupation2 = "Woodcutter";

            int expectedWoodAfterBuilding = 5;
            int expectedTotalBuildings = 3;

            //When
            village.AddWorker(name, occupation);
            village.AddWorker(name2, occupation2);
            village.Day();

            village.AddProject("Castle");

            //Then
            Assert.Equal(expectedWoodAfterBuilding, village.wood);
            Assert.Equal(expectedTotalBuildings, village.buildings.Count);
        }

        [Fact]

        public void ReceivingMoreResources_FromNewBuildings()
        {
            //Given
            Village village = new Village(0, 0, 0);
            string name = "Bob";
            string occupation = "Builder";

            string name2 = "Samuel";
            string occupation2 = "Woodcutter";

            string name3 = "Jimmy";
            string occupation3 = "Miner";

            string name4 = "Kent";
            string occupation4 = "Farmer";

            //11 food 7 + 15 = 22


            int expectedTotalMetalWithQuarry = 12;
            int expectedTotalWoodWithWoodmill = 12;
            int expectedTotalFoodWithFarm = 22;

            //everyday adds +5 metal, +5 wood and +5 food
            //once the quarry, woodmill and farm is added
            //metal = 5+7 = 12
            //wood = 5+7 = 12
            //food = (10 - 4) + 5 = 11

            //day2 food = (11 - 4) + 15 = 22

            //When
            village.AddWorker(name, occupation);
            village.AddWorker(name2, occupation2);
            village.AddWorker(name3, occupation3);
            village.AddWorker(name4, occupation4);
            village.Day();
            village.AddBuilding("Quarry");
            village.AddBuilding("Woodmill");
            village.AddBuilding("Farm");
            village.Day();
            

            //Then
            Assert.Equal(expectedTotalMetalWithQuarry, village.metal);
            Assert.Equal(expectedTotalWoodWithWoodmill, village.wood);
            Assert.Equal(expectedTotalFoodWithFarm, village.food);
        }

        [Fact]

        public void BuildHouse_WaitCorrectAmountOfDays_ForCompletion()
        {
            //Given;
            Village village = new Village(0, 0, 0);
            string name = "Bob";
            string occupation = "Builder";

            string name2 = "Samuel";
            string occupation2 = "Woodcutter";

            int expectedTotalBuildings = 4;
            int expectedTotalProjects = 0;

            //When
            village.AddWorker(name2, occupation2);
            village.Day();
            village.Day();

            village.AddProject("House");
            village.Day();

            //two builders should finish it in two days
            village.AddWorker(name, occupation);
            village.AddWorker(name, occupation);

            village.Day();
            village.Day();



            //Then
            Assert.Equal(expectedTotalBuildings, village.buildings.Count);
            Assert.Equal(expectedTotalProjects, village.projects.Count);
        }

        [Fact]

        public void BuildingACastle()
        {
            //Given
            Village village = new Village(0, 0, 0);
            string name = "Bob";
            string occupation = "Builder";

            string name2 = "Samuel";
            string occupation2 = "Woodcutter";

            string name3 = "Jimmy";
            string occupation3 = "Miner";

            string name4 = "Kent";
            string occupation4 = "Farmer";

            int expectedDaysStartToCastle = 70;

            //When
            village.AddWorker(name, occupation);
            village.AddWorker(name2, occupation2);
            village.AddWorker(name3, occupation3);
            village.AddWorker(name4, occupation4);

            for (int i = 0; i < 10; i++)
            {
                village.Day();
            }

            village.AddProject("Quarry");
            village.AddProject("Woodmill");
            village.AddProject("Farm");

            for (int i = 0; i < 8; i++)
            {
                village.Day();
            }

            village.AddProject("Castle");

            for (int i = 0; i < 51; i++)
            {
                village.Day();
            }


            //Then
            Assert.Equal(expectedDaysStartToCastle, village.daysGone);
        }

    }
}
