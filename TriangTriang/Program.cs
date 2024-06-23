using System;

namespace TriangTriang
{
    /// <summary>
    /// Responsible for executing the program
    /// </summary>
    class Program
    {
        // Initializes constant variables
        private const int Rows = 5;
        private const int Columns = 3;

        static void Main(string[] args)
        {
            // Initializes View and Controller
            IView view = new View();
            Board board = new Board(Rows,Columns);

            Controller controller= new Controller(view, board);
            // Initializes the game
            controller.GameLoop(Rows, Columns);
        }
    }
}