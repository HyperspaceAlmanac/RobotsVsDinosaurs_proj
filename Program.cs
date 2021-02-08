using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Program
    {
        static void Main(string[] args)
        {
            Battlefield bf = new Battlefield();
            bf.AddRoboAndDinos();
            bf.DisplayArmies();
            Console.WriteLine();
        }
    }
}
