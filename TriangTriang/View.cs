using System;

namespace TriangTriang
{
    /// <summary>
    /// Responsible for creating the communication between user and interface
    /// Keeps all the console.writeLine and readLine
    /// </summary>
    public class View : IView
    {
        public View(){}

        // Ask user
        public bool AksForInstructions()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Do you wish to see the instructions? (y/n)\n");
            Console.Write("Your choice > ");

            Console.ForegroundColor = ConsoleColor.White;
            string response = Console.ReadLine().Trim().ToLower();
            return response == "y";
        }

        public int AskPlayerForPieceChoice()
        {
            Console.WriteLine("Player 1, which piece type do you choose:\n" + 
            "1. White\n" +
            "2. Black\n");
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Your choice > ");
            Console.ForegroundColor = ConsoleColor.White;

            int.TryParse(Console.ReadLine(), out int input);
            return input;
        }

        // Displays game instructions
        public void Instructions()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Rules:");
            Console.WriteLine("The pieces may be moved in the following ways:");
            Console.WriteLine("    - In any direction where there is an adjacent empty spot on the board.");
            Console.WriteLine("    - By jumping over an adjacent opponent's piece, thereby eliminating that piece and landing on an empty spot on the board. However, only one piece can be captured at a time.");
            Console.WriteLine("The game ends when one player has captured or immobilized all of the opponent's pieces.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        // User interface
        public void UserInterface(PieceType currentPlayer)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nCurrent Player: {currentPlayer}");
            Console.WriteLine("Enter move (format: piece_X piece_Y target_X target_Y):");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ex.: 1 0 2 1\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Draws and updates the board based on the pieces array
        /// </summary>
        /// <param name="pieces"></param>
        public void PrintBoard(Piece[,] pieces, int columns, int rows)
        {
            Console.WriteLine();

            for (int i = 0; i < rows; i++) // Checks all 5 rows of the array
            {
                for (int j = 0; j < columns; j++) // Checks all 3 columns of the array
                {
                    Piece piece = pieces[i, j];

                    if (piece == null) // Checks if there is no pieces in this position at the moment
                    {
                        if (i == 2 && j != 1) // Invalid positions (not part of the board)
                        {
                            Console.Write("  ");
                        }
                        else
                        {
                            string target = "\u2699";
                            Console.Write(target + " "); // Places a circle on valid empty positions
                        }
                    }
                    else
                    {
                        // Checks the type of the piece and print it on the board accordingly
                        string pieceChar;
                        if (piece.Type == PieceType.White)
                        {
                            pieceChar = "\u26AB"; // Full circle
                        }
                        else
                        {
                            pieceChar = "\u26AA"; // Empty circle
                        }
                        Console.Write(pieceChar);
                    }
                }
                Console.WriteLine();
            }
        }

        // Reads user input
        public string MoveInput()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Your choice > ");

            Console.ForegroundColor = ConsoleColor.White;
            return Console.ReadLine();
        }

        // Shows game over message
        public void GameOver(PieceType player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{player} player won!");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Over!");
        }

        // Show invalid movement message
        public void InvalidMovement()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine("\n>>> Invalid choice. Try again! <<<\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}