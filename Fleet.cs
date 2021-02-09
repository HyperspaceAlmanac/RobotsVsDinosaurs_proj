﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Fleet
    {
        public List<Robot> robots;
        private Random rand;
        private List<Weapon> weapons;
        public Fleet()
        {
            if (Game.DEBUGRNG)
            {
                rand = new Random(Game.RNGSEED3);
            }
            else
            {
                rand = new Random();
            }
            robots = new List<Robot>();
            weapons = new List<Weapon>();
            weapons.Add(new Weapon("Plasma Rifle", 150));
            weapons.Add(new Weapon("Lazer sword", 200));
            weapons.Add(new Weapon("Lazer Canon", 300));
        }

        public void PrintFleet()
        {
            for (int i = 0; i < robots.Count; i++)
            {
                Console.Write($"{i + 1}: ");
                robots[i].Display();
                Console.WriteLine();
            }
        }

        public bool CannotContinue()
        {
            foreach (Robot robot in robots)
            {
                if (robot.health > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public int ReturnHealthyCombatant()
        {
            List<int> healthyUnits = new List<int>();
            for (int i = 0; i < robots.Count; i++)
            {
                if (robots[i].health > 0)
                {
                    healthyUnits.Add(i);
                }
            }
            if (healthyUnits.Count == 0)
            {
                return -1;
            }
            else if (healthyUnits.Count == 1)
            {
                return healthyUnits[0];
            }
            else
            {
                // It returns value 0 to max, not including max
                int value = rand.Next(healthyUnits.Count);
                return healthyUnits[value];
            }

        }
    }
}
