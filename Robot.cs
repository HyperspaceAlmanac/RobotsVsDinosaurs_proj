using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Robot
    {
        string name;
        int health;
        int powerLevel;
        Weapon weapon;

        public Robot(string name, int health, int powerLevel, Weapon weapon)
        {
            this.name = name;
            this.health = health;
            this.powerLevel = powerLevel;
            this.weapon = weapon;
        }

        public void Display()
        {
            Console.WriteLine($"RoboName: {name}, health: {health}, powerLevel: {powerLevel}, weaponType: {weapon.attackType}, weaponPower: {weapon.attackPower}");
        }
    }
}
