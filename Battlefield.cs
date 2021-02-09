﻿using System;
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
            Random rand;
            if (Game.DEBUGRNG)
            {
                rand = new Random(Game.RNGSEED1);
            }
            else
            {
                rand = new Random();
            }
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
                robotFleet.robots.Add(new Robot("Infantry" + roboId, rand.Next(600, 801), rand.Next(50, 100), new Weapon("Blaster", rand.Next(200, 301))));
                dinoHerd.dinos.Add(new Dinosaur(dinoTypes[rand.Next(0, dinoTypes.Length -1)], rand.Next(800, 901), rand.Next(50, 100), rand.Next(101, 301)));
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
                    Console.WriteLine("============");
                    Console.WriteLine("This dino has already been incapacitated");
                    Console.WriteLine("============");
                }
                if (robo.health == 0)
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
            if (robo.health > 0 && dino.health > 0) {
                robo.Attack(dino);
                return true;
            } else {
                if (dino.health == 0)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This dino has already been incapacitated");
                    Console.WriteLine("============");
                }
                if (robo.health == 0)
                {
                    Console.WriteLine("============");
                    Console.WriteLine("This robot has already been incapacitated");
                    Console.WriteLine("============");
                }
                return false;
            }
        }

        public bool GameEnded()
        {
            return robotFleet.CannotContinue() || dinoHerd.CannotContinue();
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
            Console.WriteLine($"Please choose a {attacker} to attack with, or enter \"skip\" to skip the turn");
            bool inputError = false;
            // Code will return early if these are not set, but need to add default value to compile
            int combatantOne = 0;
            int combatantTwo = 0;

            string input = Console.ReadLine();
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
            Console.WriteLine($"Please choose a {defender} to attack, or enter \"skip\" to skip the turn");
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
                return DealDamage(dinoHerd.dinos[combatantOne], robotFleet.robots[combatantTwo]);
            }
            else
            {
                return DealDamage(robotFleet.robots[combatantTwo], dinoHerd.dinos[combatantOne]);
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
                combatantOne = dinoHerd.ReturnHealthyCombatant();
                combatantTwo = robotFleet.ReturnHealthyCombatant();
            }
            else
            {
                combatantOne = robotFleet.ReturnHealthyCombatant();
                combatantTwo = dinoHerd.ReturnHealthyCombatant();
            }
            if (combatantOne < 0 || combatantTwo < 0)
            {
                Console.WriteLine("Should not reach here");
                return false;
            }
            if (dino)
            {
                return DealDamage(dinoHerd.dinos[combatantOne], robotFleet.robots[combatantTwo]);
            }
            else
            {
                return DealDamage(robotFleet.robots[combatantOne], dinoHerd.dinos[combatantTwo]);
            }
        }

        public void DisplayWinner(bool npc = false, bool dino=false)
        {
            Console.WriteLine("===============================");
            // there cannot be tie
            if (dinoHerd.CannotContinue())
            {
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
            else
            {
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
            Console.WriteLine("===============================");
        }
    }
}
