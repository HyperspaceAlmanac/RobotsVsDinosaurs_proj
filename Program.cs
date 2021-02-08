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
            Game game = new Game();
            game.RunGame();
            Console.ReadLine();
        }

        static void BattlefieldTest()
        {
            Battlefield bf = new Battlefield();
            bf.AddRoboAndDinos();
            bf.DisplayArmies();
            Console.WriteLine("First dino attacks first robot");
            bf.DealDamage(bf.dinoHerd.dinos[0], bf.roboFleet.robos[0]);
            bf.DisplayArmies();
            Console.WriteLine("First robot attacks first dino");
            bf.DealDamage(bf.roboFleet.robos[0], bf.dinoHerd.dinos[0]);
            bf.DisplayArmies();
        }
    }
}
