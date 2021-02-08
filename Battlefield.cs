using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Battlefield
    {
        public Fleet roboFleet;
        public Herd dinoHerd;

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
                roboFleet.robos.Add(new Robot("Infantry" + roboId, rand.Next(1500, 2000), rand.Next(50, 100), new Weapon("Blaster", rand.Next(100, 200))));
                dinoHerd.dinos.Add(new Dinosaur(dinoTypes[rand.Next(0, dinoTypes.Length -1)], rand.Next(500, 1000), rand.Next(50, 100), rand.Next(100, 200)));
            }
        }

        public void DisplayArmies()
        {
            Console.WriteLine("Robot Fleet:");
            roboFleet.PrintFleet();
            Console.WriteLine("Dinosaur Herd:");
            dinoHerd.PrintHerd();
            Console.WriteLine();
        }

        public bool DealDamage(Dinosaur dino, Robot robo)
        {
            if (dino.health > 0 && robo.health > 0)
            {
                dino.Attack(robo);
                return true;
            }
            else
            {
                if (dino.health == 0)
                {
                    Console.WriteLine("This dino has already been incapacitated");
                }
                if (robo.health == 0)
                {
                    Console.WriteLine("This robot has already been incapacitated");
                }
                return false;
            }
        }
        public bool DealDamage(Robot robo, Dinosaur dino)
        {
            if (robo.health > 0 && dino.health > 0) {
                robo.Attack(dino);
                return true;
            } else {
                if (dino.health == 0)
                {
                    Console.WriteLine("This dino has already been incapacitated");
                }
                if (robo.health == 0)
                {
                    Console.WriteLine("This robot has already been incapacitated");
                }
                return false;
            }
        }

        public bool GameEnded()
        {
            bool dinoArmyWipedOut = true;
            foreach (Dinosaur dino in dinoHerd.dinos)
            {
                if (dino.health > 0)
                {
                    dinoArmyWipedOut = false;
                }
            }
            bool robotArmyWipedOut = true;
            foreach (Dinosaur dino in dinoHerd.dinos)
            {
                if (dino.health > 0)
                {
                    robotArmyWipedOut = false;
                }
            }
            return robotArmyWipedOut || dinoArmyWipedOut;
        }

        public bool playerMove(bool dino)
        {
            string attacker = "Robot";
            string defender = "Dinosaur";
            if (dino) {
                attacker = "Dinosaur";
                defender = "Robot";
            }
            Console.WriteLine($"{attacker} fleet commander turn. ");
            DisplayArmies();
            Console.WriteLine($"Please choose a {attacker} to attack with");
            bool inputError = false;
            // Code will return early if these are not set, but need to add default value to compile
            int combatantOne = 0;
            int combatantTwo = 0;

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                case "2":
                case "3":
                    combatantOne = Convert.ToInt32(input) - 1;
                    break;
                default:
                    inputError = true;
                    break;
            }
            if (inputError)
            {
                Console.WriteLine($"Invalid {attacker} selection");
                return false;
            }
            Console.WriteLine($"Please choose a {defender} to attack");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                case "2":
                case "3":
                    combatantTwo = Convert.ToInt32(input) - 1;
                    break;
                default:
                    inputError = true;
                    break;
            }
            if (inputError)
            {
                Console.WriteLine($"Invalid {defender} selection");
                return false;
            }
            if (dino)
            {
                return DealDamage(dinoHerd.dinos[combatantOne], roboFleet.robos[combatantTwo]);
            }
            else
            {
                return DealDamage(roboFleet.robos[combatantTwo], dinoHerd.dinos[combatantOne]);
            }
        }

        public bool npcRobot()
        {
            return true;
        }

        public bool npcDinosaur()
        {
            return true;
        }

        public void DisplayWinner()
        {
            foreach (Dinosaur dino in dinoHerd.dinos)
            {
                if (dino.health > 0)
                {
                    Console.WriteLine("The dinosaur herd is victorious!");
                    return;
                }
            }
            // Could probably check robot health here just to be sure
            Console.WriteLine("The robot fleet is victorious!");
        }
    }
}
