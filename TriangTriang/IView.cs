namespace TriangTriang
{
    /// <summary>
    /// Interface that establishes a contract with the class view
    /// </summary>
    public interface IView
    {
        // Instructions
        bool AksForInstructions();
        void Instructions();

        // Ask player for input choice
        int AskPlayerForPieceChoice();

        // Menus
        void UserInterface(PieceType currentPlayer);
        void PrintBoard(Piece[,] pieces, int columns, int rows);

        // User input
        string MoveInput();

        // Messages
        void InvalidMovement();
        void GameOver(PieceType player);
    }
}