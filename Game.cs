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
        // save rngSeed here
        public readonly static int RNGSEED1 = 100;
        public readonly static int RNGSEED2 = 200;
        public readonly static int RNGSEED3 = 300;
        // Set to false for completely random values
        public readonly static bool DEBUGRNG = true;

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
            Console.WriteLine("===========================");
            Console.WriteLine("Welcome to Robots vs Dinosaurs!");
            Console.WriteLine("===========================");
            Console.WriteLine("Please enter 1 for single player vs NPC,or 2 for player vs player");
            Console.WriteLine("You can type \"exit\" at any time to exit the game");
            string input = Console.ReadLine();
            if (exitGame(input))
            {
                return GameState.Exit;
            }
            if (input == "1")
            {
                Console.WriteLine("You have selected player vs NPC");
                vsNPC = true;
                return GameState.PickSide;
            }
            else if (input == "2")
            {
                Console.WriteLine("You have selected player vs player");
                return GameState.WhoGoesFirst;
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
            Console.WriteLine("Please enter 1 to play as commander of the dinosaur herd, or 2 to play as the commander of the robot fleet");
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
                bf.DisplayWinner(vsNPC, humanPlayerDino);
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
            Console.WriteLine("NPC has taken its turn. Press Enter to continue");
            Console.ReadLine();
            return GameState.TakeTurn;
        }

        // Boolean passed in of is it dinosaur herd's turn to attack
        GameState PlayerTurn(bool dino) {
            bool result = bf.PlayerMove(dino);
            // Check if game is over
            if (bf.GameEnded())
            {
                bf.DisplayWinner(vsNPC, humanPlayerDino);
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
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();

            // Game is not over, continue
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

                // Too much effort to debug the elegant boolean logic structure.... just going to layout all the cases
                if (humanPlayerDino)
                {
                    if (playerOneTurn)
                    {
                        // dinosaurs went first, dinosaur is player 1, the person is playing dinosaur => player + dino
                        if (dinoFirst)
                        {
                            return PlayerTurn(true);
                        }
                        // robot is going first thus dino is player 2, and it is currently robot player turn = > NPC + robot
                        else
                        {
                            return NpcTurn(false);
                        }
                    } else {
                        // Dinosuar went first, but it is player 2's turn.  => NPC + robot
                        if (dinoFirst)
                        {
                            return NpcTurn(false);
                        }
                        // Dinosaur went second, and it is player 2's turn => player + dino
                        else
                        {
                            return PlayerTurn(true);
                        }
                    }
                }
                else
                {
                    if (playerOneTurn)
                    {
                        // Player 1's turn, dinosaur is going first, person is not playing dino => NPC + dino
                        if (dinoFirst)
                        {
                            return NpcTurn(true);
                        }
                        else
                        // Player 1's turn, robot is first = > player + robot
                        {
                            return PlayerTurn(false);
                        }
                    }
                    else
                    {
                        // Player 2's turn, dinosaur went first => player + robot
                        if (dinoFirst)
                        {
                            return PlayerTurn(false);
                        }
                        // Player 2's turn, dinosaur went second => NPC + dino
                        else
                        {
                            return NpcTurn(true);
                        }
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
