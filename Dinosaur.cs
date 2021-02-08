using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Dinosaur
    {
        string dinoType;
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

        public void Attack(Robot robot)
        {
            if (robot.health > this.attackPower)
            {
                robot.health -= this.attackPower;
            }
            else
            {
                robot.health = 0;
            }
        }

        public void Display()
        {
            Console.WriteLine($"Type: {dinoType}, health: {health}, energy: {energy}, attackPower: {attackPower}");
        }
    }
}
