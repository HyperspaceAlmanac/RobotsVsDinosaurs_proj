using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Battlefield
    {
        public Fleet robotFleet;
        public Herd dinoHerd;

        public Battlefield()
        {
            robotFleet = new Fleet();
            dinoHerd = new Herd();
        }

        public void AddRoboAndDinos()
        {
            // Randomly generate some stats
            // Add in rngSeed to do same values for now
            HashSet<int> roboNameHash = new HashSet<int>();
            // Adding three basic dinos and robos

            string[] dinoTypes = {"T-Rex", "Pterodactyl", "Stegosaurus", "Triceratops", "Brontosaurus"};
            string[] roboTypes = { "Infantry", "Construction", "Artillery", "Vanguard", "Spec Ops" };
            for (int i = 0; i < 3; i++)
            {
                int roboId = Game.rand.Next(0, 1000);
                while (roboNameHash.Contains(roboId))
                {
                    roboId = Game.rand.Next(0, 1000);
                }
                roboNameHash.Add(roboId);
                // customize values here
                robotFleet.robots.Add(new Robot(roboTypes[Game.rand.Next(roboTypes.Length)] + roboId, Game.rand.Next(600, 801), 50, new Weapon()));
                dinoHerd.dinos.Add(new Dinosaur(dinoTypes[Game.rand.Next(dinoTypes.Length)], Game.rand.Next(1200, 1601), 50, Game.rand.Next(101, 301)));
            }
        }

        public void DisplayArmies(bool dinoFirst = true)
        {
            if (dinoFirst)
            {
                Console.WriteLine("Dinosaur Herd:");
                dinoHerd.PrintHerd();
                Console.WriteLine("Robot Fleet:");
                robotFleet.PrintFleet();
            } 
            else
            {
                Console.WriteLine("Robot Fleet:");
                robotFleet.PrintFleet();
                Console.WriteLine("Dinosaur Herd:");
                dinoHerd.PrintHerd();
            }
            Console.WriteLine();
        }

        public bool DealDamage(Dinosaur dino, Robot robo, Move move)
        {
            if (dino.health > 0 && robo.health > 0 && dino.energy > 9)
            {
                dino.Attack(robo, move);
                return true;
            }
            else
            {
                if (dino.health <= 0)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This dino has already been incapacitated");
                    Console.WriteLine("============");
                } else if (dino.energy < 10)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This dino does not have enough energy");
                    Console.WriteLine("============");
                }
                if (robo.health <= 0)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This robot has already been incapacitated");
                    Console.WriteLine("============");
                }
                return false;
            }
        }
        public bool DealDamage(Robot robo, Dinosaur dino)
        {
            if (robo.health > 0 && dino.health > 0 && robo.powerLevel > 9) {
                robo.Attack(dino);
                return true;
            } else {
                if (robo.health <= 0)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This robot has already been incapacitated");
                    Console.WriteLine("============");
                }
                else if (robo.powerLevel < 10)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This robot does not have enough power level");
                    Console.WriteLine("============");
                }
                if (dino.health <= 0)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This dino has already been incapacitated");
                    Console.WriteLine("============");
                }

                return false;
            }
        }

        public bool PlayerMove(bool dino)
        {
            string attacker = "Robot";
            string defender = "Dinosaur";
            if (dino) {
                attacker = "Dinosaur";
                defender = "Robot";
            }
            Console.WriteLine("===========================");
            Console.WriteLine($"{attacker} " + (dino ? "Herd" : "Fleet") + " commander turn. ");
            Console.WriteLine("===========================");
            DisplayArmies(dino);
            if (dino)
            {
                if (dinoHerd.NoEnergy())
                {
                    Console.WriteLine("Dinosaur herd has no active unit with enough energy left");
                    return true;
                }
            }
            else
            {
                if (robotFleet.NoPower())
                {
                    Console.WriteLine("Robot fleet has no active unit with enough power left");
                    return true;
                }
            }
            Console.WriteLine($"Please choose a {attacker}(1-3) to attack with, or enter \"skip\" to skip the turn");
            if (!dino)
            {
                Console.WriteLine("Please enter \"equip\" to change weapons");
            }
            bool inputError = false;
            // Code will return early if these are not set, but need to add default value to compile
            int combatantOne = 0;
            int combatantTwo = 0;

            // Check if there is enough energy for move

            string input = Console.ReadLine();
            switch (input)
            {
                case "equip":
                    if (!dino)
                    {
                        Console.WriteLine("Equipping robots in the fleet");
                        robotFleet.Equip();
                        return false;
                    }
                    inputError = true;
                    break;
                case "skip":
                    Console.WriteLine("===================");
                    Console.WriteLine($"{attacker} " + (dino ? "Herd" : "Fleet") + " turn skipped");
                    Console.WriteLine("===================");
                    return true;
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
                Console.WriteLine("============");
                Console.WriteLine($"Invalid attacking {attacker} selection, Please try again");
                Console.WriteLine("============");
                return false;
            }
            Console.WriteLine($"Please choose a {defender}(1-3) to attack, or enter \"skip\" to skip the turn");
            input = Console.ReadLine();
            switch (input)
            {
                case "skip":
                    Console.WriteLine("===================");
                    Console.WriteLine($"{attacker} " + (dino ? "Herd" : "Fleet") + " turn skipped");
                    Console.WriteLine("===================");
                    return true;
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
                Console.WriteLine("============");
                Console.WriteLine($"Invalid defending {defender} selection, Please try again");
                Console.WriteLine("============");
                return false;
            }
            if (dino)
            {
                Dinosaur dinosaur = dinoHerd.dinos[combatantOne];
                return DealDamage(dinosaur, robotFleet.robots[combatantTwo], dinosaur.GetMove());
            }
            else
            {
                return DealDamage(robotFleet.robots[combatantOne], dinoHerd.dinos[combatantTwo]);
            }
        }

        public bool NpcTurn(bool dino)
        {
            string attacker = "Robot";
            string defender = "Dinosaur";
            if (dino)
            {
                attacker = "Dinosaur";
                defender = "Robot";
            }
            Console.WriteLine("===========================");
            Console.WriteLine($"NPC {attacker} " + (dino ? "Herd" : "Fleet") + " commander turn. ");
            Console.WriteLine("===========================");
            DisplayArmies(dino);

            int combatantOne = -1;
            int combatantTwo = -1;
            if (dino)
            {
                if (dinoHerd.NoEnergy())
                {
                    Console.WriteLine("Could not perform action, none of the active dinosaurs have enough energy left");
                    return true;
                }
                combatantOne = dinoHerd.CanAttack();
                combatantTwo = robotFleet.CanBeAttacked();
            }
            else
            {
                if (robotFleet.NoPower())
                {
                    Console.WriteLine("Could not perform action, none of the active robots have enough power left");
                    return true;
                }
                combatantOne = robotFleet.CanAttack();
                combatantTwo = dinoHerd.CanBeAttacked();
            }
            if (combatantOne < 0 || combatantTwo < 0)
            {
                Console.WriteLine("Should not reach here");
                return false;
            }
            if (dino)
            {
                // Randomly select a move from the list
                Dinosaur dinosaur = dinoHerd.dinos[combatantOne];
                return DealDamage(dinosaur, robotFleet.robots[combatantTwo], dinosaur.GetMove(true));
            }
            else
            {
                return DealDamage(robotFleet.robots[combatantOne], dinoHerd.dinos[combatantTwo]);
            }
        }

        public bool GameEnded()
        {
            // Need to check no health first for a win
            if (dinoHerd.NoHealth() || robotFleet.NoHealth())
            {
                return true;
            }
            // Need to check if both side has run out of energy for a draw
            if (dinoHerd.NoEnergy() && robotFleet.NoPower())
            {
                return true;
            }
            // There is available moves
            else
            {
                return false;
            }
              
        }

        public void DisplayWinner(bool npc = false, bool dino=false)
        {
            Console.WriteLine("===============================");
            if (dinoHerd.NoHealth())
            {
                Console.WriteLine("The dinosaur herd has run out of health");
                Console.WriteLine("The robot fleet is victorious!");
                if (npc)
                {
                    if (dino)
                    {
                        Console.WriteLine("You lose!");
                    }
                    else
                    {
                        Console.WriteLine("You Win!");
                    }
                }
            }
            else if (robotFleet.NoHealth())
            {
                Console.WriteLine("The robot fleet has run out of health");
                Console.WriteLine("The dinosaur herd is victorious!");
                if (npc)
                {
                    if (dino)
                    {
                        Console.WriteLine("You Win!");
                    }
                    else
                    {
                        Console.WriteLine("You Lose!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Both sides have run out of energy. It is a stalmate");
            }
            Console.WriteLine("===============================");

        }
    }
}
