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
        int health;
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
    }
}
