using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Fleet
    {
        public List<Robot> robos;
        public Fleet()
        {
            robos = new List<Robot>();
        }

        public void PrintFleet()
        {
            for (int i = 0; i < robos.Count; i++)
            {
                Console.Write($"{i + 1}: ");
                robos[i].Display();
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
            return -1;

        }
    }
}
