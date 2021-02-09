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
                int oldHealth = dino.health;
                dino.health -= weapon.attackPower;
                Console.WriteLine($"{dino.dinoType} went from {oldHealth} down to {dino.health} health");
                Console.WriteLine("============");
            }
            else
            {
                Console.WriteLine("============");
                Console.WriteLine($"{name} used {weapon.attackType} to deal {dino.health} damage to {dino.dinoType}!");
                dino.health = 0;
                Console.WriteLine($"{dino.dinoType} has been incapacitated");
                Console.WriteLine("============");
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
