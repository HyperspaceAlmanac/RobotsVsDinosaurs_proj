using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Dinosaur
    {
        public string dinoType;
        public int health;
        int energy;
        // Will probably replace later with Attack class with type
        int attackPower;

        public Dinosaur(string dinoType, int health, int energy, int attackPower)
        {
            this.dinoType = dinoType;
            this.health = health;
            this.energy = energy;
            this.attackPower = attackPower;
        }

        public void Attack(Robot robot, Move move)
        {
            if (robot.health > move.damage)
            {
                Console.WriteLine("============");
                Console.WriteLine($"{dinoType} used {move.name} to deal {move.damage} damage to {robot.name}!");
                robot.health -= move.damage;
                Console.WriteLine($"{robot.name} is now at {robot.health} health");
                Console.WriteLine("============");
            }
            else
            {
                Console.WriteLine("============");
                Console.WriteLine($"{dinoType} used {move.name} to deal {robot.health} damage to {robot.name}!");
                Console.WriteLine($"{robot.name} has been incapacitated");
                Console.WriteLine("============");
                robot.health = 0;
            }
        }

        public void Display()
        {
            Console.Write($"Type: {dinoType}, health: {health}, energy: {energy}, attackPower: {attackPower}");
        }
    }
}
