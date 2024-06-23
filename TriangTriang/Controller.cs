namespace TriangTriang
{
    /// <summary>
    /// This class is responsible for the loop of the game and establishes the connection
    /// between classes like view and board
    /// </summary>
    public class Controller
    {                
        // Initializes view and board
        private IView view;
        private Board board;

        // Initializes the variable current player
        private PieceType currentPlayer;

        public Controller(IView view, Board board)
        {
            this.view = view;
            this.board = board;
            currentPlayer = PlayerPieceChoice();
        }

        /// <summary>
        /// Defines the currentPlayer type based on the user input
        /// </summary>
        /// <returns></returns>
        private PieceType PlayerPieceChoice()
        {
            while (true)
            {
                int input = view.AskPlayerForPieceChoice();
                if(input == 1)
                {
                    return PieceType.White;
                }
                else if(input == 2)
                {
                    return PieceType.Black;
                }
                else
                {
                    view.InvalidMovement();
                }
            }
        }

        /// <summary>
        /// Handles the player input and updates the game based on that
        /// </summary>
        /// <param name="view"></param>
        public void GameLoop(int rows, int columns)
        {
            if(view.AksForInstructions())
            {
                view.Instructions();
            }

            while (!board.IsGameOver(currentPlayer))
            {
                view.PrintBoard(board.GetPieces(), columns, rows);

                bool validMove = false;
                while (!validMove)
                {
                    view.UserInterface(currentPlayer);
                    string input = view.MoveInput();
                    string[] inputParts = input.Split(' ');

                    // The splitted input needs to be 4 numbers long to define the coordinates
                    if (inputParts.Length != 4)
                    {
                        view.InvalidMovement();
                    }

                    // Tries to convert string input into an integer
                    else if (int.TryParse(inputParts[0], out int piece_X) &&
                             int.TryParse(inputParts[1], out int piece_Y) &&
                             int.TryParse(inputParts[2], out int target_X) &&
                             int.TryParse(inputParts[3], out int target_Y))
                    {
                        // Tries to move the piece
                        validMove = MakeMove(piece_X, piece_Y, target_X, target_Y);

                        // If validMove is null it means the input was incorrect
                        if (!validMove)
                        {
                            view.InvalidMovement();
                        }
                    }
                }
            }
            view.PrintBoard(board.GetPieces(), columns, rows);
            view.GameOver(board.GetWinner(currentPlayer));
        }
        
        /// <summary>
        /// Calls the Board function to move the piece and passes the user input
        /// as parameters
        /// </summary>
        /// <param name="piece_X"></param>
        /// <param name="piece_Y"></param>
        /// <param name="target_X"></param>
        /// <param name="target_Y"></param>
        /// <returns></returns>
        public bool MakeMove(int piece_X, int piece_Y, int target_X, int target_Y)
        {
            // Defines the chosen piece's coordinate and fetches them
            Piece piece = board.GetPieces()[piece_X, piece_Y];

            // Verifies if the piece the player is trying to move belongs to them
            if(piece?.Type != currentPlayer)
            {
                return false;
            }

            // Checks if the piece can be moved to chosen target
            if (board.MovePiece(piece_X, piece_Y, target_X, target_Y))
            {
                // if current player is White switches to Black and vice versa
                currentPlayer = (currentPlayer == PieceType.White) ? PieceType.Black : PieceType.White;
                return true;
            }
            return false;
        }
    }
}
