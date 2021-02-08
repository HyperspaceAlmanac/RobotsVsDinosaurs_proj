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
        PlayerOneTurn,
        PlayerTwoTurn,
        GameOver,
        Exit
    }
    class Game
    {
        bool vsNPC;
        bool playerOneDino;
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
                case GameState.WhoGoesFirst:
                    Console.WriteLine("Here");
                    break;
                case GameState.PlayerOneTurn:
                    break;
                case GameState.PlayerTwoTurn:
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
                vsNPC = false;
                Console.WriteLine("You have selected player vs player");
                return GameState.WhoGoesFirst;
            }
            else if (input == "2")
            {
                Console.WriteLine("You have selected player vs NPC");
                vsNPC = true;
                return GameState.WhoGoesFirst;
            }
            Console.WriteLine("Invalid input, please try again");
            return GameState.ChooseGameMode;
        }
    }
}
