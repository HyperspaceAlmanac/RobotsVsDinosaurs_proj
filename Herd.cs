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
        private Random rand;

        public Herd()
        {
            if (Game.DEBUGRNG)
            {
                rand = new Random(Game.RNGSEED2);
            }
            else
            {
                rand = new Random();
            }
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

        // Method to check if game is over. Start with checking health, can add energy check later for bonus
        public bool CannotContinue()
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

        public int ReturnHealthyCombatant()
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
                // Looks Like Random 
                int value = rand.Next(healthyUnits.Count);
                return healthyUnits[value];
            }
        }
    }
}
