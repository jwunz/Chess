using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    abstract class Piece
    {
        public bool IsWhite;
        public char PieceRep;

        public Location location;

        public abstract bool ValidateMove(Location destination, ref Board board, bool toCap = false);
    }
}
