using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board
    {
        public bool WhiteTurn { get; set; } = true;
        public Location[,] Locations = new Location [8,8];
        public ArrayList Kings = new ArrayList(2);

        public Board()
        {
            for(byte i = 0; i < 8; i++)
            {
                for(byte j = 0; j < 8; j++)
                {
                    Locations[i, j] = new Location(i, j);
                    Locations[i, j] = new Location(i, j);
                }
            }
        }

        public void CapturePiece(Location loc1, Location loc2)
        {
            RemovePiece(loc1);
            MovePiece(loc1, loc2);
        }

        public void MovePiece(Location loc1, Location loc2)
        {
            Piece movedPiece = loc1.getPiece();
            RemovePiece(loc1);            
            PlacePiece(movedPiece, loc2);
        }

        public void PlacePiece(Piece piece, Location location)
        {
            location.setPiece(piece);
        }

        public void RemovePiece(Location location)
        {
            location.setPiece(null);
        }

        
    }
}
