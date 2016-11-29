using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chess
{
    class Commands
    {
        public static void ProcessCommands(string input, ref Board board)
        {
            string ValidPlacementString = "^([KQBNRP])([ld])([a-h])([1-8])$";
            string ValidMoveString = "^([a-h])([1-8]) ?([a-h])([1-8])$";
            string ValidMoveCaptureString = @"^([a-h])([1-8]) ?([a-h])([1-8])\*$";
            string ValidDoubleMoveString = "^([a-h])([1-8]) ?([a-h])([1-8]) ?([a-h])([1-8]) ? ([a-h])([1-8])$";

            Regex ValidPlacementFormat = new Regex(ValidPlacementString);
            Regex ValidMoveFormat = new Regex(ValidMoveString);
            Regex ValidMoveCaptureFormat = new Regex(ValidMoveCaptureString);
            Regex ValidDoubleMoveFormat = new Regex(ValidDoubleMoveString);

            if (ValidPlacementFormat.IsMatch(input)) // If it's a placement command.
            {
                Console.WriteLine(input);

                string[] parts = Regex.Split(input, ValidPlacementString);
                placeAPiece(parts, ref board);

                Console.WriteLine();
            }
            else if (ValidMoveFormat.IsMatch(input)) // If it's a movement command.
            {
                Console.WriteLine(input);

                string[] parts = Regex.Split(input, ValidMoveString);
                moveAPiece(parts, ref board);

                Console.WriteLine();
            }
            else if (ValidMoveCaptureFormat.IsMatch(input)) // If it's a movement capture command.
            {
                Console.WriteLine(input);

                string[] parts = Regex.Split(input, ValidMoveCaptureString);
                capAPiece(parts, ref board);
                
                Console.WriteLine();
            }
            else if (ValidDoubleMoveFormat.IsMatch(input)) // If it's a double movement command.
            {
                Console.WriteLine(input);

                string[] parts = Regex.Split(input, ValidDoubleMoveString);
                doubleMove(parts, ref board);

                Console.WriteLine();
            }
            else // If it's anything else
            {
                Console.WriteLine($"{input} is invalid!");
                Console.WriteLine();
            }
        }

        private static string changeRowLetterToNumber(string toReplace)
        {
            switch (toReplace)
            {
                case "a":
                    toReplace = "1";
                    break;

                case "b":
                    toReplace = "2";
                    break;

                case "c":
                    toReplace = "3";
                    break;

                case "d":
                    toReplace = "4";
                    break;

                case "e":
                    toReplace = "5";
                    break;

                case "f":
                    toReplace = "6";
                    break;

                case "g":
                    toReplace = "7";
                    break;

                case "h":
                    toReplace = "8";
                    break;
            }

            return toReplace;
        }

        private static bool processColor(string color)
        {
            bool isWhite = (color == "l");
            return isWhite;
        }

        private static Piece processPiece(string pieceString, bool isWhite, ref Location location, ref Board board)
        {
            Piece piece;

            switch (pieceString)
            {
                case "R":
                    piece = new Rook(isWhite, ref location);
                    break;
                case "N":
                    piece = new Knight(isWhite, ref location);
                    break;
                case "B":
                    piece = new Bishop(isWhite, ref location);
                    break;
                case "K":
                    piece = new King(isWhite, ref location);
                    board.Kings.Add(piece);
                    break;
                case "Q":
                    piece = new Queen(isWhite, ref location);
                    break;
                default:
                    piece = new Pawn(isWhite, ref location);
                    break;
            }

            return piece;
        }

        private static void placeAPiece(string[] parts, ref Board board)
        {
            parts[3] = changeRowLetterToNumber(parts[3]);

            int y = int.Parse(parts[3]) - 1;
            int x = int.Parse(parts[4]) - 1;

            bool isWhite = processColor(parts[2]);
            Piece piece = processPiece(parts[1], isWhite, ref board.Locations[x, y], ref board);

            board.PlacePiece(piece, board.Locations[x, y]);  

            BoardDrawer.DrawBoard(board);
        }

        private static void moveAPiece(string[] parts, ref Board board)
        {
            string y1String = changeRowLetterToNumber(parts[1]);
            string y2String = changeRowLetterToNumber(parts[3]);

            int y1 = int.Parse(y1String) - 1;
            int x1 = int.Parse(parts[2]) - 1;
            int y2 = int.Parse(y2String) - 1;
            int x2 = int.Parse(parts[4]) - 1;

            if (board.Locations[x1, y1].getPiece() != null)
            {
                if (board.WhiteTurn == board.Locations[x1, y1].getPiece().IsWhite)
                {
                    if (board.Locations[x1, y1].getPiece().ValidateMove(board.Locations[x2, y2], ref board))
                    {
                        board.MovePiece(board.Locations[x1, y1], board.Locations[x2, y2]);
                        BoardDrawer.DrawBoard(board);
                        board.WhiteTurn = !board.WhiteTurn;
                    }
                    else
                    {
                        Console.WriteLine($"{board.Locations[x1, y1].getPiece().GetType().ToString()} cannot move from [{parts[1]}, {parts[2]}] to [{parts[3]}, {parts[4]}]");
                    }
                }
                else
                {
                    Console.WriteLine($"The piece at [{x1}, {y1}] does not belong to you!");
                }
            }
            else
            {
                Console.WriteLine($"There is not a piece to move at [{parts[1]}, {parts[2]}]");
            }

            foreach (King king in board.Kings)
            {
                if (king.InCheck(ref board))
                {
                    string player;

                    if (king.IsWhite)
                    {
                        player = "white";
                    }
                    else
                    {
                        player = "black";
                    }

                    Console.WriteLine($"The {player} king is in check!");

                    for (int i = 1; i < 7; i++)
                    {
                        Board simulatedBoard = board;  //I was working here
                    }
                }
            }
        }

        private static void capAPiece(string[] parts, ref Board board)
        {
            string y1String = changeRowLetterToNumber(parts[1]);
            string y2String = changeRowLetterToNumber(parts[3]);

            int y1 = int.Parse(y1String) - 1;
            int x1 = int.Parse(parts[2]) - 1;
            int y2 = int.Parse(y2String) - 1;
            int x2 = int.Parse(parts[4]) - 1;

            if (board.Locations[x1, y1].getPiece() != null)
            {
                if (board.WhiteTurn == board.Locations[x1, y1].getPiece().IsWhite)
                {
                    if (board.Locations[x1, y1].getPiece().ValidateMove(board.Locations[x2, y2], ref board, true))
                    {
                        board.RemovePiece(board.Locations[x2, y2]);
                        board.MovePiece(board.Locations[x1, y1], board.Locations[x2, y2]);
                        BoardDrawer.DrawBoard(board);
                        board.WhiteTurn = !board.WhiteTurn;
                    }
                    else
                    {
                        Console.WriteLine($"{board.Locations[x1, y1].getPiece().GetType().ToString()} cannot move from [{parts[1]}, {parts[2]}] to [{parts[3]}, {parts[4]}]");
                    }
                }
                else
                {
                    Console.WriteLine($"The piece at [{x1}, {y1}] does not belong to you!");
                }
            }
            else
            {
                Console.WriteLine($"There is not a piece to move at [{parts[1]}, {parts[2]}]");
            }

            foreach (King king in board.Kings)
            {
                if (king.InCheck(ref board))
                {
                    string player;

                    if (king.IsWhite)
                    {
                        player = "white";
                    }
                    else
                    {
                        player = "black";
                    }

                    Console.WriteLine($"The {player} king is in check!");
                }
            }
        }
        
        private static void doubleMove(string[] parts, ref Board board)
        {
            string y1String = changeRowLetterToNumber(parts[1]);
            string y2String = changeRowLetterToNumber(parts[3]);

            int y1 = int.Parse(y1String) - 1;
            int x1 = int.Parse(parts[2]) - 1;
            int y2 = int.Parse(y2String) - 1;
            int x2 = int.Parse(parts[4]) - 1;
            
            string y3String = changeRowLetterToNumber(parts[5]);
            string y4String = changeRowLetterToNumber(parts[7]);

            int y3 = int.Parse(y3String) - 1;
            int x3 = int.Parse(parts[6]) - 1;
            int y4 = int.Parse(y4String) - 1;
            int x4 = int.Parse(parts[8]) - 1;

            if (board.Locations[x1, y1].getPiece() != null && board.Locations[x3, y3].getPiece() != null)
            {
                if (board.WhiteTurn == board.Locations[x1, y1].getPiece().IsWhite)
                {
                    if (board.Locations[x1, y1].getPiece().ValidateMove(board.Locations[x2, y2], ref board))
                    {
                        if (board.Locations[x3, y3].getPiece().ValidateMove(board.Locations[x4, y4], ref board))
                        {
                            board.MovePiece(board.Locations[x1, y1], board.Locations[x2, y2]);
                            board.MovePiece(board.Locations[x3, y3], board.Locations[x4, y4]);
                            BoardDrawer.DrawBoard(board);
                            board.WhiteTurn = !board.WhiteTurn;
                        }
                        else
                        {
                            Console.WriteLine($"{board.Locations[x3, y3].getPiece().GetType().ToString()} cannot move from [{parts[5]}, {parts[6]}] to [{parts[7]}, {parts[8]}]");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{board.Locations[x1, y1].getPiece().GetType().ToString()} cannot move from [{parts[1]}, {parts[2]}] to [{parts[3]}, {parts[4]}]");
                    }
                }
                else
                {
                    Console.WriteLine($"The piece at [{x1}, {y1}] does not belong to you!");
                }
            }
            else
            {
                Console.WriteLine("One of the spaces that you are trying to move from does not have a space.");
            }

            foreach (King king in board.Kings)
            {
                if (king.InCheck(ref board))
                {
                    string player;

                    if (king.IsWhite)
                    {
                        player = "white";
                    }
                    else
                    {
                        player = "black";
                    }

                    Console.WriteLine($"The {player} king is in check!");
                }
            }
        }
    }
}
