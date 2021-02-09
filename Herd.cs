using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Herd
    {
        public List<Dinosaur> dinos;
        

        public Herd()
        {
            dinos = new List<Dinosaur>();
        }

        public void PrintHerd()
        {
            for (int i = 0; i < dinos.Count; i++)
            {
                Console.Write($"{i + 1}: ");
                dinos[i].Display();
                Console.WriteLine();
            }
        }
        public bool NoHealth()
        {
            foreach (Dinosaur dino in dinos)
            {
                if (dino.health > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool NoEnergy()
        {
            foreach (Dinosaur dino in dinos)
            {
                if (dino.health > 0 && dino.energy > 9)
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
            if (NoEnergy())
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
            for (int i = 0; i < dinos.Count; i++)
            {
                if (dinos[i].health > 0 && dinos[i].energy > 9)
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
            for (int i = 0; i < dinos.Count; i++)
            {
                if (dinos[i].health > 0)
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
