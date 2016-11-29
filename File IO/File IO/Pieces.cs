using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(bool isWhite, ref Location loc)
        {
            this.IsWhite = isWhite;

            if (this.IsWhite)
            {
                PieceRep = 'p';
            }
            else
            {
                PieceRep = 'P';
            }

            location = loc;
        }
        public override bool ValidateMove(Location destination, ref Board board, bool toCap = false)
        {
            bool valid = (destination.YCoordinate == this.location.YCoordinate);
            int direction = 1;
            int startingX = 6;

            if (valid)
            {
                if (board.WhiteTurn)
                {
                    direction = -1;
                    startingX = 1;
                }

                if (!toCap)
                {
                    if (this.location.XCoordinate == destination.XCoordinate + direction)
                    {
                        if (destination.getPiece() != null)
                        {
                            valid = false;
                            Console.WriteLine("There's a piece in the way!");
                        }
                    }
                    else if (this.location.XCoordinate == startingX && this.location.XCoordinate == destination.XCoordinate + 2 * direction)
                    {
                        if (destination.getPiece() != null || board.Locations[destination.XCoordinate + direction, destination.YCoordinate].getPiece() != null)
                        {
                            valid = false;
                            Console.WriteLine("There's a piece in the way!");
                        }
                    }
                    else
                    {
                        valid = false;
                    }
                }
                else
                {
                    valid = false;
                }
            }
            else if (toCap)
            {
                valid = ((this.location.XCoordinate + direction == destination.XCoordinate && Math.Abs(this.location.YCoordinate - destination.YCoordinate) == 1) &&
                    destination.getPiece() != null && destination.getPiece().IsWhite != this.IsWhite);
            }

            return valid;
        }
    }

    class Rook : Piece
    {
        public Rook(bool isWhite, ref Location loc)
        {
            this.IsWhite = isWhite;

            if (this.IsWhite)
            {
                PieceRep = 'r';
            }
            else
            {
                PieceRep= 'R';
            }

            location = loc;
        }
        public override bool ValidateMove(Location destination, ref Board board, bool toCap = false)
        {
            bool valid = (destination.XCoordinate == this.location.XCoordinate || destination.YCoordinate == this.location.YCoordinate);

            if(valid)
            {
                byte start;
                byte end;

                if(destination.XCoordinate == this.location.XCoordinate)
                {
                    if (destination.YCoordinate > this.location.YCoordinate)
                    {
                        start = (byte)(this.location.YCoordinate + 1);
                        end = destination.YCoordinate;
                        if (toCap)
                        {
                            end--;
                        }
                    }
                    else
                    {
                        start = destination.YCoordinate;
                        if (toCap)
                        {
                            start++;
                        }
                        end = (byte)(this.location.YCoordinate - 1);
                    }

                    for(byte i = start; i <= end; i++)
                    {
                        if(board.Locations[destination.XCoordinate, i].getPiece() != null)
                        {
                            if (!toCap)
                            {
                                Console.WriteLine("There is a piece in the way of that move!");
                            }
                            valid = false;
                            break;
                        }
                    }
                }
                else
                {
                    if (destination.XCoordinate > this.location.XCoordinate)
                    {
                        start = (byte)(this.location.XCoordinate + 1);
                        end = destination.XCoordinate;
                        if (toCap)
                        {
                            end--;
                        }
                    }
                    else
                    {
                        start = destination.XCoordinate;
                        if (toCap)
                        {
                            start--;
                        }
                        end = (byte)(this.location.XCoordinate - 1);
                    }

                    for(byte i = start; i <= end; i++)
                    {
                        if (board.Locations[i, destination.YCoordinate].getPiece() != null)
                        {
                            if (!toCap)
                            {
                                Console.WriteLine("There is a piece in the way of that move!");
                            }
                            valid = false;
                            break;
                        }
                    }
                }               
            }

            if ((valid || (Math.Abs(location.XCoordinate - destination.XCoordinate) == 1 ^ Math.Abs(location.YCoordinate - destination.YCoordinate) == 1)) && toCap)
            {
                valid = (destination.getPiece().IsWhite != IsWhite);
            }

            return valid;
        }
    }

    class Knight : Piece
    {
        public Knight(bool isWhite, ref Location loc)
        {
            this.IsWhite = isWhite;

            if (isWhite)
            {
                PieceRep = 'n';
            }
            else
            {
                PieceRep = 'N';
            }

            location = loc;
        }
        public override bool ValidateMove(Location destination, ref Board board, bool toCap = false)
        {
            bool valid = ((Math.Abs(destination.XCoordinate - this.location.XCoordinate) == 1) && (Math.Abs(destination.YCoordinate - this.location.YCoordinate) == 2) || 
                ((Math.Abs(destination.XCoordinate - this.location.XCoordinate) == 2) && (Math.Abs(destination.YCoordinate - this.location.YCoordinate) == 1)));
            if(destination.getPiece() != null && !toCap)
            {
                if (!toCap)
                {
                    Console.WriteLine("There is a piece in the way!");
                }
                valid = false;
            }

            if (valid && toCap && destination.getPiece() != null)
            {
                valid = (destination.getPiece().IsWhite != IsWhite);
            }

            return valid;
        }
    }

    class Bishop : Piece
    {
        public Bishop(bool isWhite, ref Location loc)
        {
            this.IsWhite = isWhite;

            if (isWhite)
            {
                PieceRep = 'b';
            }
            else
            {
                PieceRep = 'B';
            }

            location = loc;
        }
        public override bool ValidateMove(Location destination, ref Board board, bool toCap = false)
        {
            bool valid = (Math.Abs(destination.XCoordinate - this.location.XCoordinate) == Math.Abs(destination.YCoordinate - this.location.YCoordinate));

            if (valid)
            {
                byte xStart = (byte)(this.location.XCoordinate + 1);
                byte xEnd = destination.XCoordinate;
                byte yStart = (byte)(this.location.YCoordinate + 1);
                byte yEnd = destination.YCoordinate;
                if (toCap)
                {
                    xEnd--;
                    yEnd--;
                }
                int xDelta = 1;
                int yDelta = 1;

                if (destination.XCoordinate < this.location.XCoordinate)
                {
                    xStart -= 2;
                    xDelta = -1;
                    if (toCap)
                    {
                        xEnd += 2;
                    }
                }
                if (destination.YCoordinate < this.location.YCoordinate)
                {
                    yStart -= 2;
                    yDelta = -1;
                    if (toCap)
                    {
                        yEnd += 2;
                    }
                }

                bool looping = true;
                byte i = xStart;
                byte j = yStart;

                while (looping)
                {
                    if (board.Locations[i, j].getPiece() != null)
                    {
                        if (!toCap)
                        {
                            Console.WriteLine("There's a piece in the way!");
                        }
                        valid = false;
                        looping = false;
                    }
                    else if (i == xEnd || i == yEnd)
                    {
                        looping = false;
                    }
                    else
                    {
                        i = (byte)(i + xDelta);
                        j = (byte)(j + yDelta);
                    }
                }
            }

            if ((valid || (Math.Abs(location.XCoordinate - destination.XCoordinate) == 1 && Math.Abs(location.YCoordinate - destination.YCoordinate) == 1)) && toCap)
            {
                valid = (destination.getPiece().IsWhite != IsWhite);
            }

            return valid;
        }
    }

    class Queen : Piece
    {
        public Queen(bool isWhite, ref Location loc)
        {
            this.IsWhite = isWhite;

            if (isWhite)
            {
                PieceRep = 'q';
            }
            else
            {
                PieceRep = 'Q';
            }

            location = loc;
        }
        public override bool ValidateMove(Location destination, ref Board board, bool toCap)
        {
            bool valid = ((Math.Abs(destination.XCoordinate - this.location.XCoordinate) == Math.Abs(destination.YCoordinate - this.location.YCoordinate)) ||
                (destination.XCoordinate == this.location.XCoordinate || destination.YCoordinate == this.location.YCoordinate));

            if (valid)
            {
                byte xStart;
                byte xEnd = destination.XCoordinate;
                byte yStart;
                byte yEnd = destination.YCoordinate;
                int xDelta;
                int yDelta;

                if (destination.XCoordinate < this.location.XCoordinate)
                {
                    xStart = (byte)(this.location.XCoordinate - 1);
                    xDelta = -1;
                    if (toCap)
                    {
                        xEnd += 1;
                    }
                }
                else if (destination.XCoordinate > this.location.XCoordinate)
                {
                    xStart = (byte)(this.location.XCoordinate + 1);
                    xDelta = 1;
                    if (toCap)
                    {
                        xEnd -= 1;
                    }
                }
                else
                {
                    xStart = this.location.XCoordinate;
                    xDelta = 0;
                }

                if (destination.YCoordinate < this.location.YCoordinate)
                {
                    yStart = (byte)(this.location.YCoordinate - 1);
                    yDelta = -1;
                    if (toCap)
                    {
                        yEnd += 1;
                    }
                }
                else if (destination.YCoordinate > this.location.YCoordinate)
                {
                    yStart = (byte)(this.location.YCoordinate + 1);
                    yDelta = 1;
                    if (toCap)
                    {
                        yEnd -= 1;
                    }
                }
                else
                {
                    yStart = this.location.YCoordinate;
                    yDelta = 0;
                }

                bool looping = true;
                byte i = xStart;
                byte j = yStart;

                while (looping)
                {
                    if (board.Locations[i, j].getPiece() != null)
                    {
                        if (!toCap)
                        {
                            Console.WriteLine("There's a piece in the way!");
                        }
                        valid = false;
                        looping = false;
                    }
                    else if (i == xEnd && j == yEnd)
                    {
                        looping = false;
                    }
                    else
                    {
                        i = (byte)(i + xDelta);
                        j = (byte)(j + yDelta);
                    }
                }
            }

            if ((valid || (Math.Abs(location.XCoordinate - destination.XCoordinate) == 1 || Math.Abs(location.YCoordinate - destination.YCoordinate) == 1)) && toCap)
            {
                valid = (destination.getPiece().IsWhite != IsWhite);
            }

            return valid;
        }
    }

    class King : Piece
    {
        public King(bool isWhite, ref Location loc)
        {
            this.IsWhite = isWhite;

            if (isWhite)
            {
                PieceRep = 'k';
            }
            else
            {
                PieceRep = 'K';
            }

            location = loc;
        }
        public override bool ValidateMove(Location destination, ref Board board, bool toCap = false)
        {
            bool valid = (!(Math.Abs(destination.XCoordinate - this.location.XCoordinate) > 1) &&
                !(Math.Abs(destination.YCoordinate - this.location.YCoordinate) > 1) &&
                true);//!new King(IsWhite, ref destination).InCheck(ref board));

            if (destination.getPiece() != null && !toCap)
            {
                Console.WriteLine("There is a piece in the way!");
                valid = false;
            }
            else if (toCap && destination.getPiece() != null && destination.getPiece().IsWhite != IsWhite)
            {
                valid = true;
            }

            return valid;
        }

        public bool InCheck(ref Board board)
        {
            bool inCheck =
               ((this.location.XCoordinate - 2 >= 0 && this.location.YCoordinate - 1 >= 0 && board.Locations[this.location.XCoordinate - 2, this.location.YCoordinate - 1].getPiece() != null && board.Locations[this.location.XCoordinate - 2, this.location.YCoordinate - 1].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate - 2, this.location.YCoordinate - 1].getPiece().IsWhite) ||
                (this.location.XCoordinate - 1 >= 0 && this.location.YCoordinate - 2 >= 0 && board.Locations[this.location.XCoordinate - 1, this.location.YCoordinate - 2].getPiece() != null && board.Locations[this.location.XCoordinate - 1, this.location.YCoordinate - 2].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate - 1, this.location.YCoordinate - 2].getPiece().IsWhite) || 
                (this.location.XCoordinate + 1 <= 7 && this.location.YCoordinate - 2 >= 0 && board.Locations[this.location.XCoordinate + 1, this.location.YCoordinate - 2].getPiece() != null && board.Locations[this.location.XCoordinate + 1, this.location.YCoordinate - 2].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate + 1, this.location.YCoordinate - 2].getPiece().IsWhite) ||
                (this.location.XCoordinate + 2 <= 7 && this.location.YCoordinate - 1 >= 0 && board.Locations[this.location.XCoordinate + 2, this.location.YCoordinate - 1].getPiece() != null && board.Locations[this.location.XCoordinate + 2, this.location.YCoordinate - 1].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate + 2, this.location.YCoordinate - 1].getPiece().IsWhite) ||
                (this.location.XCoordinate - 2 >= 0 && this.location.YCoordinate + 1 <= 7 && board.Locations[this.location.XCoordinate - 2, this.location.YCoordinate + 1].getPiece() != null && board.Locations[this.location.XCoordinate - 2, this.location.YCoordinate + 1].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate - 2, this.location.YCoordinate + 1].getPiece().IsWhite) ||
                (this.location.XCoordinate - 1 >= 0 && this.location.YCoordinate + 2 <= 7 && board.Locations[this.location.XCoordinate - 1, this.location.YCoordinate + 2].getPiece() != null && board.Locations[this.location.XCoordinate - 1, this.location.YCoordinate + 2].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate - 1, this.location.YCoordinate + 2].getPiece().IsWhite) ||
                (this.location.XCoordinate + 1 <= 7 && this.location.YCoordinate + 2 <= 7 && board.Locations[this.location.XCoordinate + 1, this.location.YCoordinate + 2].getPiece() != null && board.Locations[this.location.XCoordinate + 1, this.location.YCoordinate + 2].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate + 1, this.location.YCoordinate + 2].getPiece().IsWhite) ||
                (this.location.XCoordinate + 2 <= 7 && this.location.YCoordinate + 1 <= 7 && board.Locations[this.location.XCoordinate + 2, this.location.YCoordinate + 1].getPiece() != null && board.Locations[this.location.XCoordinate + 2, this.location.YCoordinate + 1].getPiece().ValidateMove(this.location, ref board, true) && this.IsWhite != board.Locations[this.location.XCoordinate + 2, this.location.YCoordinate + 1].getPiece().IsWhite));

            if (!inCheck)
            {
                for (int i = 1; i < 7; i++)
                {
                    if (this.location.XCoordinate - i >= 0)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate - i, this.location.YCoordinate].getPiece() != null &&
                            board.Locations[this.location.XCoordinate - i, this.location.YCoordinate].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.YCoordinate - i >= 0)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate, this.location.YCoordinate - i].getPiece() != null &&
                            board.Locations[this.location.XCoordinate, this.location.YCoordinate - i].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.XCoordinate - i >= 0 && this.location.YCoordinate - i >= 0)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate - i, this.location.YCoordinate - i].getPiece() != null &&
                            board.Locations[this.location.XCoordinate - i, this.location.YCoordinate - i].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.XCoordinate + i <= 7)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate + i, this.location.YCoordinate].getPiece() != null &&
                            board.Locations[this.location.XCoordinate + i, this.location.YCoordinate].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.YCoordinate + i <= 7)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate, this.location.YCoordinate + i].getPiece() != null &&
                            board.Locations[this.location.XCoordinate, this.location.YCoordinate + i].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.XCoordinate + i <= 7 && this.location.YCoordinate + i <= 7)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate + i, this.location.YCoordinate + i].getPiece() != null &&
                            board.Locations[this.location.XCoordinate + i, this.location.YCoordinate + i].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.XCoordinate - i >= 0 && this.location.YCoordinate + i <= 7)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate - i, this.location.YCoordinate + i].getPiece() != null &&
                            board.Locations[this.location.XCoordinate - i, this.location.YCoordinate + i].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }

                    if (this.location.XCoordinate + i <= 7 && this.location.YCoordinate - i >= 0)
                    {
                        inCheck = (board.Locations[this.location.XCoordinate + i, this.location.YCoordinate - i].getPiece() != null &&
                            board.Locations[this.location.XCoordinate + i, this.location.YCoordinate - i].getPiece().ValidateMove(this.location, ref board, true));
                        if (inCheck)
                        {
                            break;
                        }
                    }
                }
            }
            return inCheck;
        }
    }
}
