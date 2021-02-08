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
            foreach (Dinosaur d in dinos)
            {
                d.Display();
            }
        }
    }
}
