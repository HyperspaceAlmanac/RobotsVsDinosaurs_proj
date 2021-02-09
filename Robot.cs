using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Robot
    {
        public string name;
        public int health;
        int powerLevel;
        public Weapon weapon;

        public Robot(string name, int health, int powerLevel, Weapon weapon)
        {
            this.name = name;
            this.health = health;
            this.powerLevel = powerLevel;
            this.weapon = weapon;
        }

        public void Attack(Dinosaur dino)
        {
            if (dino.health > weapon.attackPower)
            {
                Console.WriteLine("============");
                Console.WriteLine($"{name} used {weapon.attackType} to deal {weapon.attackPower} damage to {dino.dinoType}!");
                Console.WriteLine($"{dino.dinoType} is now at {dino.health} health");
                Console.WriteLine("============");
                dino.health -= weapon.attackPower;
            }
            else
            {
                Console.WriteLine("============");
                Console.WriteLine($"{name} used {weapon.attackType} to deal {dino.health} damage to {dino.dinoType}!");
                Console.WriteLine($"{dino.dinoType} has been incapacitated");
                Console.WriteLine("============");
                dino.health = 0;
            }
        }

        public void Equip(Weapon weapon)
        {
            this.weapon = weapon;
        }
        public void Display()
        {
            Console.Write($"RobotName: {name}, health: {health}, powerLevel: {powerLevel}, weaponType: {weapon.attackType}, weaponPower: {weapon.attackPower}");
        }
    }
}
