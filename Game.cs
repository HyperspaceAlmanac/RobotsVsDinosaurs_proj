using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    enum GameState
    {
        ChooseGameMode,
        WhoGoesFirst,
        PickSide,
        TakeTurn,
        GameOver,
        Exit
    }
    class Game
    {
        bool vsNPC;
        bool humanPlayerDino;
        bool dinoFirst;
        bool playerOneTurn;
        Battlefield bf;

        public void RunGame()
        {
            // GameState to keep track what is curretly going on
            GameState gameState = GameState.ChooseGameMode;
            // while game state is not exit
            while (gameState != GameState.Exit)
            {
                gameState = handleInput(gameState);
            }
            Console.WriteLine("Exiting Game");
        }

        GameState handleInput(GameState state)
        {
            // Call other functions to handle the input
            GameState newState = GameState.Exit;
            switch (state)
            {
                case GameState.ChooseGameMode:
                    newState = ChooseMode();
                    break;
                case GameState.PickSide:
                    newState = PickSide();
                    break;
                case GameState.WhoGoesFirst:
                    newState = WhoIsFirst();
                    break;
                case GameState.TakeTurn:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    Console.WriteLine("Something went wrong");
                    break;
            }
            return newState;
        }
        bool exitGame(string str)
        {
            return str == "exit";
        }
        // Game logic here
        // Based on game state passed in, this function does different thigns

        public GameState ChooseMode()
        {
            // Initialize the values here, instead of constructor
            vsNPC = false;
            humanPlayerDino = false;
            dinoFirst = false;
            playerOneTurn = true;
            Console.WriteLine("Welcome to Robots vs Dinosaurs!");
            Console.WriteLine("Please enter 1 for player vs player, and 2 for player vs NPC");
            Console.WriteLine("You can enter \"exit\" at any time to exit the game");
            string input = Console.ReadLine();
            if (exitGame(input))
            {
                return GameState.Exit;
            }
            if (input == "1")
            {
                Console.WriteLine("You have selected player vs player");
                return GameState.WhoGoesFirst;
            }
            else if (input == "2")
            {
                Console.WriteLine("You have selected player vs NPC");
                vsNPC = true;
                return GameState.PickSide;
            }
            Console.WriteLine("Invalid input, please try again");
            return GameState.ChooseGameMode;
        }

        public GameState WhoIsFirst()
        {
            Console.WriteLine("Please enter 1 for dinosaurs to take the first turn, or 2 for robots to take the first turn");
            string input = Console.ReadLine();
            if (exitGame(input))
            {
                return GameState.Exit;
            }
            if (input == "1" || input == "2")
            {
                if (input == "1")
                {
                    dinoFirst = true;
                }
                else if (input == "2")
                {
                    dinoFirst = false;
                }
                Console.WriteLine("Initializing Game");
                bf = new Battlefield();
                bf.AddRoboAndDinos();
                bf.DisplayArmies();
                return GameState.TakeTurn;
            }
            else
            {
                Console.WriteLine("Invalid input, please try again");
                return GameState.WhoGoesFirst;
            }
        }
        public GameState PickSide() {
            Console.WriteLine("Please enter 1 to play as commander of the dinosaur herd, or 2 to play as the commande of the robot fleet");
            string input = Console.ReadLine();
            if (exitGame(input))
            {
                return GameState.Exit;
            }
            if (input == "1")
            {
                humanPlayerDino = true;
                return GameState.WhoGoesFirst;
            }
            else if (input == "2")
            {
                humanPlayerDino = false;
                return GameState.WhoGoesFirst;
            }
            return GameState.PickSide;
        }
    }
}
