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
            foreach (Robot r in robos)
            {
                r.Display();
            }
        }
    }
}
