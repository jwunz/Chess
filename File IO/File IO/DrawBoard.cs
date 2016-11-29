using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class BoardDrawer
    {
        public static void DrawBoard(Board board)
        {
            Console.WriteLine("   | A | B | C | D | E | F | G | H |");
            for (byte i = 0; i < 8; i++)
            {
                Console.WriteLine("---|---+---+---+---+---+---+---+---|");
                Console.Write($" {i + 1} |");
                for (byte j = 0; j < 8; j++)
                {
                    if (board.Locations[i,j].getPiece() == null)
                    {
                        Console.Write("   |");
                    }
                    else
                    {
                        Console.Write($" {board.Locations[i,j].getPiece().PieceRep} |");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
