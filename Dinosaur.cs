using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Dinosaur
    {
        public string dinoType;
        public int health;
        int energy;
        // Will probably replace later with Attack class with type
        int attackPower;
        Move[] moves = new Move[3];

        public Dinosaur(string dinoType, int health, int energy, int attackPower)
        {
            this.dinoType = dinoType;
            this.health = health;
            this.energy = energy;
            this.attackPower = attackPower;
            moves = new Move[3];
            moves[0] = new Move();
            moves[1] = new Move("Tackle", 200);
            moves[2] = new Move("Uppercut", 300);
        }

        public void Attack(Robot robot, Move move)
        {
            if (robot.health > move.damage)
            {
                Console.WriteLine("============");
                Console.WriteLine($"{dinoType} used {move.name} to deal {move.damage} damage to {robot.name}!");
                robot.health -= move.damage;
                Console.WriteLine($"{robot.name} is now at {robot.health} health");
                Console.WriteLine("============");
            }
            else
            {
                Console.WriteLine("============");
                Console.WriteLine($"{dinoType} used {move.name} to deal {robot.health} damage to {robot.name}!");
                robot.health = 0;
                Console.WriteLine($"{robot.name} has been incapacitated");
                Console.WriteLine("============");
            }
        }

        public Move GetMove(bool autoSelect = false)
        {
            if (autoSelect)
            {
                return moves[Game.rand.Next(moves.Length)];
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
                Console.WriteLine("Please enter the move(1-3) that the dinosaur should do");
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

        public void Display()
        {
            StringBuilder sb = new StringBuilder();

            bool first = true;
            foreach (Move m in moves)
            {
                if (first)
                {
                    sb.Append(" " + m.name +" " + m.damage);
                    first = false;
                }
                else
                {
                    sb.Append(", " + m.name + " " + m.damage);
                }
            }
            Console.Write($"Type: {dinoType}, health: {health}, energy: {energy}, available moves:{sb.ToString()}");
        }
    }
}
