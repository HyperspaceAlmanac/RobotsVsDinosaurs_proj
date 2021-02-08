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

        public void RunGame()
        {
            // GameState to keep track what is curretly going on
            GameState gameState = GameState.ChooseGameMode;
            // while game state is not exit
            while (gameState != GameState.Exit)
            {
                gameState = handleInput(gameState, Console.ReadLine());
            }
            Console.WriteLine("Exiting Game");
        }

        GameState handleInput(GameState state, string input)
        {
            // Call other functions to handle the input
            GameState newState = GameState.Exit;
            switch (state)
            {
                case GameState.ChooseGameMode:
                    break;
                case GameState.WhoGoesFirst:
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

    }
}
