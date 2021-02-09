using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Fleet
    {
        public List<Robot> robots;
        private List<Weapon> weapons;
        public Fleet()
        {
            robots = new List<Robot>();
            weapons = new List<Weapon>();
            weapons.Add(new Weapon("Plasma Rifle", 200));
            weapons.Add(new Weapon("Lazer sword", 400));
            weapons.Add(new Weapon("Lazer Canon", 600));
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

        // Select equipment for robot
        public void Equip(bool autoSelect = false)
        {
            if (autoSelect)
            {
                EquipNPC();       
            }
            else
            {
                EquipPlayer();
            }
        }
        private void EquipNPC()
        {
            int weaponIndex;
            Console.WriteLine("The NPC Robot Fleet Commander is equipping the army");
            for (int i = 0; i < robots.Count; i++)
            {
                weaponIndex = Game.rand.Next(weapons.Count);
                Console.WriteLine($"{robots[i].name} has been equiped with {weapons[weaponIndex].attackType} that has attack power of {weapons[weaponIndex].attackPower}");
                robots[i].weapon = weapons[weaponIndex];
            }
        }
        private void EquipPlayer() 
        {
            string str;
            bool done = false;
            int robotIndex = 0;
            int weaponIndex = 0;
            while (!done)
            {
                Console.WriteLine("Please Select the robot (1-3) to equip");
                PrintFleet();
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                    case "2":
                    case "3":
                        robotIndex = Convert.ToInt32(str) - 1;
                        Console.WriteLine($"{robots[robotIndex].name} has been selected");
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid robot, please try again");
                        break;
                }
            }
            done = false;
            while (!done)
            {
                Console.WriteLine("Please Select the weapon (1-3) to equip");
                PrintWeapons();
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                    case "2":
                    case "3":
                        weaponIndex = Convert.ToInt32(str) - 1;
                        Console.WriteLine($"{weapons[weaponIndex].attackType} with attack power of {weapons[weaponIndex].attackPower} has been selected");
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid robot, please try again");
                        break;
                }
            }
            robots[robotIndex].weapon = weapons[weaponIndex];
            PrintFleet();
        }

        private void PrintWeapons() {
            Console.WriteLine("Weapons: ");
            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {weapons[i].attackType}, Attack Power: {weapons[i].attackPower}");
            }
        }
        public bool NoHealth()
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
        public bool NoPower()
        {
            foreach (Robot robot in robots)
            {
                if (robot.health > 0 && robot.powerLevel > 9)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CannotContinue()
        {
            if (NoHealth())
            {
                return true;
            }
            if (NoPower())
            {
                return true;
            }
            // One dino with health and energy
            return false;
        }

        // Need to separate combat ready combatants from ones that can be attacked
        // Units with health and energy
        public int CanAttack()
        {
            List<int> healthyUnits = new List<int>();
            for (int i = 0; i < robots.Count; i++)
            {
                if (robots[i].health > 0 && robots[i].powerLevel > 9)
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
                int value = Game.rand.Next(healthyUnits.Count);
                return healthyUnits[value];
            }
        }
        // units with Health
        public int CanBeAttacked()
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
                int value = Game.rand.Next(healthyUnits.Count);
                return healthyUnits[value];
            }
        }

    }
}
