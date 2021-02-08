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
    }
}
