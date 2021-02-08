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
                    newState = TakeTurn();
                    break;
                case GameState.GameOver:
                    newState = GameOver();
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
        GameState NpcTurn(bool dino)
        {
            // ToDo
            bool result = bf.NpcTurn(dino);
            if (bf.GameEnded())
            {
                bf.DisplayWinner();
                return GameState.GameOver;
            }
            else
            {
                // If input was good, next player's turn
                if (result)
                {
                    playerOneTurn = !playerOneTurn;
                }
                else
                {
                    // Should never reach here, even if energy is drained per attack and every combatant runs out, GameEnded function should catch it
                    Console.WriteLine("Something went wrong here");
                }
            }
            return GameState.TakeTurn;
        }

        public GameState GameOver()
        {
            Console.WriteLine("Play again? Please enter \"yes\" to play again, \"no\" or \"exit\" to exit");
            string input = Console.ReadLine();
            if (input == "yes")
            {
                return GameState.ChooseGameMode;
            }
            else if (input == "no" || input == "exit")
            {
                return GameState.Exit;
            }
            else
            {
                return GameState.GameOver;
            }
        }

        // Boolean passed in of is it dinosaur herd's turn to attack
        GameState PlayerTurn(bool dino) {
            bool result = bf.PlayerMove(dino);
            // Check if game is over
            if (bf.GameEnded())
            {
                bf.DisplayWinner();
                return GameState.GameOver;
            }
            else
            {
                // If input was good, next player's turn
                if (result)
                {
                    playerOneTurn = !playerOneTurn;
                }
            }
            
            // Game is not over, continue
            return GameState.TakeTurn;
        }


        // Where the action happens
        public GameState TakeTurn()
        {
            // Consider all the cases of whose turn is it
            if (vsNPC) {
                // Consider conditions for NPC turn
                // HumanPlayerDino = true, playerOneTurn = true, dinoFirst = true ===> (player, dino)
                // HumanPlayerDino = true, playerOneTurn = true, dinoFirst = false ===> (npc, robot)
                // HumanPlayerDino = true, playerOneTurn = false, dinoFirst = true ===> (npc, robot)
                // HumanPlayerDino = true, playerOneTurn = false, dinoFirst = false ===> (player, dino)

                // HumanPlayerDino = false, playerOneTurn = true, dinoFirst = true ===> (npc, dino)
                // HumanPlayerDino = false, playerOneTurn = true, dinoFirst = false ===> (player, robot)
                // HumanPlayerDino = false, playerOneTurn = false, dinoFirst = true ===> (player, robot)
                // HumanPlayerDino = false, playerOneTurn = false, dinoFirst = false ===> (npc, dino)
                if ((playerOneTurn && !dinoFirst) || (!playerOneTurn && dinoFirst))
                {
                    if (humanPlayerDino)
                    {
                        return NpcTurn(false);
                    }
                    else
                    {
                        return PlayerTurn(true);
                    }
                }
                else
                {
                    if (humanPlayerDino)
                    {
                        return PlayerTurn(false);
                    }
                    else
                    {
                        return NpcTurn(true);
                    }
                }
                
                //return PlayerTurn((playerOneTurn && dinoFirst) || (!playerOneTurn && !dinoFirst));
            }
            else
            {
                // p1 turn + dinoFirst = true, p1 turn + dinoFirst = false
                // not p1 turn + dinoFirst = false, not p1 turn + not dino = true
                return PlayerTurn((playerOneTurn && dinoFirst) || (!playerOneTurn && !dinoFirst));
            }
        }
    }
}
