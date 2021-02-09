using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    // A move that a dinosaur can do
    class Move
    {
        public string name;
        public int damage;

        public Move(string name = "Tail Swipe", int damage = 100)
        {
            this.name = name;
            this.damage = damage;
        }

    }
}
