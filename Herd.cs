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
        private Move[] moves = new Move[3];

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
            moves = new Move[3];
            moves[0] = new Move();
            moves[1] = new Move("Tackle", 200);
            moves[2] = new Move("Uppercut", 300);
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

        public Move GetMove(bool autoSelect=false)
        {
            if (autoSelect)
            {
                return moves[rand.Next(moves.Length)];
            }
            else
            {
                return AskForMove();
            }
        }

        public Move AskForMove()
        {
            string input;
            while (true)
            {
                Console.WriteLine("Please enter the move that the dinosaur should do");
                for (int i = 0; i < moves.Length; i++)
                {
                    Console.WriteLine($" {i + 1}: {moves[i].name}, {moves[i].damage} damage");
                }
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    case "2":
                    case "3":
                        return moves[Convert.ToInt32(input) - 1];
                    default:
                        Console.WriteLine("Invalid input, please try again");
                        break;
                }
            }

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
