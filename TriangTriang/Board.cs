using System;

namespace TriangTriang
{
    /// <summary>
    /// Class responsible for the all of the board logic and functions
    /// </summary>
    public class Board
    {
        //Defines the number of Rows and Columns, to keep consistency and avoid "magic numbers"
        private int Rows;
        private int Columns;

        //Initializes the pieces array
        private Piece[,] pieces;

        public Board(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            pieces = new Piece[Rows, Columns];
            InitializeBoard();
        }
        
        /// <summary>
        /// Initializes the starting pieces in the board
        /// </summary>
        private void InitializeBoard()
        {
            for (int i = 0; i < Columns; i++)
            {
                // Creates 6 White pieces from 0-0 to 1-2
                pieces[0, i] = new Piece(PieceType.White);
                pieces[1, i] = new Piece(PieceType.White);
                // Creates 6 Black pieces from 3-0 to 4-2
                pieces[3, i] = new Piece(PieceType.Black);
                pieces[4, i] = new Piece(PieceType.Black);
                // The middle row is left empty, and 2-1 is the only spot where a piece can be placed
                // 2-0 and 2-2 are not valid spaces in the board
            }
        }

        // Fetch piece array
        public Piece[,] GetPieces()
        {
            return pieces;
        }

        /// <summary>
        /// Moves piece based on the coordinates written by the player
        /// </summary>
        /// <param name="piece_X"></param>
        /// <param name="piece_Y"></param>
        /// <param name="target_X"></param>
        /// <param name="target_Y"></param>
        /// <returns></returns>
        public bool MovePiece(int piece_X, int piece_Y, int target_X, int target_Y)
        {
            if (IsValidMove(piece_X, piece_Y, target_X, target_Y))
            {
                // Moves piece to the chosen target
                pieces[target_X, target_Y] = pieces[piece_X, piece_Y];
                // Empties the piece's previous placement
                pieces[piece_X, piece_Y] = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the piece exists in the coordinate chosen by the player and
        /// if the target coordinate is a valid one
        /// </summary>
        /// <param name="piece_X"></param>
        /// <param name="piece_Y"></param>
        /// <param name="target_X"></param>
        /// <param name="target_Y"></param>
        /// <returns></returns>
       private bool IsValidMove(int piece_X, int piece_Y, int target_X, int target_Y)
        {
            // Verify if the piece exists and if the target is valid.
            if (!IsPositionValid(piece_X, piece_Y) || !IsPositionValid(target_X, target_Y))
            {
                return false;
            }

            // Defines the chosen piece's coordinate
            Piece piece = pieces[piece_X, piece_Y];
            // Defines the target coordinate
            Piece target = pieces[target_X, target_Y];

            // Checks if there is a piece at the specified coordinate and if the 
            // target coordinate is already occupied with another piece
            if (piece == null || target != null)
            {
                return false;
            }

            // Calculates the the variation between the beginning coordinates 
            // (piece_X and Piece_Y) and the ending coordinates (target_X and target_Y)
            // Generated with ChatGPT
            int deltaX = target_X - piece_X;
            int deltaY = target_Y - piece_Y;

            // Checks if the player is trying to move towards an adjacent house
            // Generated with ChatGPT
            if ((deltaX == 0 || deltaX == 1 || deltaX == -1) && (deltaY == 0 || deltaY == 1 || deltaY == -1))
            {
                return true;
            }

            // Checks if the player is trying to move 2 houses
            // Generated with ChatGPT
            if ((deltaX == 2 || deltaX == -2) && (deltaY == 0 || deltaY == 2 || deltaY == -2))
            {
                // Creates a coordinate between the initial position and the target position
                // Generated with ChatGPT
                int opponent_X = piece_X + deltaX / 2;
                int opponent_Y = piece_Y + deltaY / 2;

                // Assign this coordinate to an opponent piece
                Piece opponent_piece = pieces[opponent_X, opponent_Y];

                // Checks if the piece actually exists on the board
                if (opponent_piece != null && opponent_piece.Type != piece.Type)
                {
                    // Eliminates the opponent piece that was in the way
                    pieces[opponent_X, opponent_Y] = null;
                    return true;
                }
                // If there is not an opponent piece in the way between 2 houses, the player cannot move to the target
            }
            return false;
        }
        
        /// <summary>
        /// Checks if the target chosen by the player is valid and empty
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsPositionValid(int x, int y)
        {
            // Checks if position is inside the board grid
            if (x < 0 || x >= Rows || y < 0 || y >= Columns)
            {
                return false;
            }

            // Checks if position is 2-0 or 2-2 (which are not valid spaces)
            if (x == 2 && y != 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the game is over
        /// The game is over if either one of the players loses all pieces or
        /// has no possible moves left
        /// </summary>
        /// <returns></returns>
        public bool IsGameOver(PieceType currentPlayer)
        {
            // If any of the players have pieces left in the board
            if(!HasPieces(PieceType.White) || !HasPieces(PieceType.Black))
            {
                return true;
            }
            // If the current player has no available moves left
            if(!HasValidMovesForPlayer(currentPlayer))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This function identifies the winner based on the pieces left on the 
        /// board, or if the current player has any available moves left
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <returns></returns>
        public PieceType GetWinner(PieceType currentPlayer)
        {
            // Check if White has no pieces left
            if (!HasPieces(PieceType.White))
            {
                return PieceType.Black;
            }

            // Check if Black has no pieces left
            if (!HasPieces(PieceType.Black))
            {
                return PieceType.White;
            }

            // Check if White has no valid moves
            if (!HasValidMovesForPlayer(PieceType.White))
            {
                return PieceType.Black;
            }

            // Check if Black has no valid moves
            if (!HasValidMovesForPlayer(PieceType.Black))
            {
                return PieceType.White;
            }

            // If none of the above conditions were met, return null
            return PieceType.None;
        }

        /// <summary>
        /// Checks if there are any pieces from both types left on the board.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool HasPieces(PieceType type)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (pieces[i, j]?.Type == type)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        /// <summary>
        /// Checks if any of the pieces of specific type has any move available
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasValidMovesForPlayer(PieceType currentPlayer)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    // Checks if the piece has any possible direction to move
                    if (HasValidMove(i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the player has a valid move in any direction
        /// </summary>
        /// <param name="piece_X"></param>
        /// <param name="piece_Y"></param>
        /// <returns></returns>
        private bool HasValidMove(int piece_X, int piece_Y)
        {
            // Defines the possible directions the player can move in x and y
            int[] directions = {-1, 1};

            //Checks if player can move in any adjacent houses
            foreach (int deltaX in directions)
            {
                foreach (int deltaY in directions)
                {
                    if (IsValidMove(piece_X, piece_Y, piece_X + deltaX, piece_Y + deltaY))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
