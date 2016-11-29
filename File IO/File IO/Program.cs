using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "";
            try
            {
                fileName = args[0];
            }
            catch (IndexOutOfRangeException e)
            {

            }

            Directory.SetCurrentDirectory(@"..\..");

            string[] setupCommands = File.ReadAllLines("Setup.txt");

            Board board = new Board();

            foreach (string command in setupCommands)
            {
                Commands.ProcessCommands(command, ref board);
            }
            Console.Clear();
            BoardDrawer.DrawBoard(board);

            if (fileName != "")
            {
                try
                {
                    BoardDrawer.DrawBoard(board);
                    Console.WriteLine($"Attempting to load {Directory.GetCurrentDirectory()}\\{fileName}");

                    string[] commands = File.ReadAllLines(fileName);

                    foreach (string command in commands)
                    {
                        Commands.ProcessCommands(command, ref board);
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"Something went wrong while loading the file.  Input string was {fileName} {e}");
                }
            }

            bool playing;

            do
            {
                Console.WriteLine("Please input a command, 0 to quit.");
                string command = Console.ReadLine();

                if (command == "0")
                {
                    playing = false;
                }
                else
                {
                    Commands.ProcessCommands(command, ref board);
                    playing = !board.CheckMate();
                }
            } while (playing);

            Console.WriteLine("Game Over!");
            Console.Read();
        }
    }
}
