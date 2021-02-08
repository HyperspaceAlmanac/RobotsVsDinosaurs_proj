using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Battlefield
    {
        Fleet roboFleet;
        Herd dinoHerd;

        public Battlefield()
        {
            roboFleet = new Fleet();
            dinoHerd = new Herd();
        }

        public void AddRoboAndDinos()
        {
            // Randomly generate some stats
            // Add in rngSeed to do same values for now
            int rngSeed = 1000;
            Random rand = new Random(rngSeed);
            HashSet<int> roboNameHash = new HashSet<int>();
            // Adding three basic dinos and robos

            string[] dinoTypes = {"T-Rex", "Pterodactyl", "Stegosaurus", "Triceratops", "Brontosaurus"};
            for (int i = 0; i < 3; i++)
            {
                int roboId = rand.Next(0, 1000);
                while (roboNameHash.Contains(roboId))
                {
                    roboId = rand.Next(0, 1000);
                }
                roboNameHash.Add(roboId);
                roboFleet.robos.Add(new Robot("Infantry" + roboId, rand.Next(100, 200), rand.Next(50, 100), new Weapon("Blaster", rand.Next(100, 200))));
                dinoHerd.dinos.Add(new Dinosaur(dinoTypes[rand.Next(0, dinoTypes.Length -1)], rand.Next(500, 1000), rand.Next(50, 100), rand.Next(200, 300)));
            }
        }

        public void DisplayArmies()
        {
            Console.WriteLine("Robot army:");
            roboFleet.PrintFleet();
            Console.WriteLine();
            dinoHerd.PrintHerd();
            Console.WriteLine();
        }
    }
}
