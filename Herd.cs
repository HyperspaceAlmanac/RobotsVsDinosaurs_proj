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

        public int ReturnHealthyCombatant()
        {
            Random rand;
            if (Game.DEBUGRNG)
            {
                rand = new Random(Game.RNGSEED);
            }
            else
            {
                rand = new Random();
            }
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
                return healthyUnits[rand.Next(0, healthyUnits.Count - 1)];
            }
        }
    }
}
