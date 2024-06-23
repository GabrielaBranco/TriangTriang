namespace TriangTriang
{
    /// <summary>
    /// This classes is the model responsible for a piece in the game
    /// </summary>
    public class Piece
    {
        // Gets the piece type, it can't be set outside this class
        public PieceType Type { get; private set; }

        /// <summary>
        /// Initiates an instance of a piece
        /// </summary>
        /// <param name="type"></param>
        public Piece(PieceType type)
        {
            Type = type;
        }
    }
}