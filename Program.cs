using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Labyrinth
{
    internal class Program
    {
        static Path[,] maze;
        static List<Path> mazeConponents = new List<Path>()
        {
            new Path('.', false, false, false, false),
            new Path('█', true, true, true, true),
            new Path('╬', true, true, true, true),
            new Path('═', true, false, true, false),
            new Path('╦', true, false, true, true),
            new Path('╩', true, true, true, false),
            new Path('║', false, true, false, true),
            new Path('╣', true, true, false, true),
            new Path('╠', false, true, true, true),
            new Path('╗', true, false, false, true),
            new Path('╝', true, true, false, false),
            new Path('╚', false, true, true, false),
            new Path('╔', false, false, true, true)
        };
        static string message = String.Empty;
        static void Main(string[] args)
        {
            Menu();
            string mazePath = "minta.txt";
            LoadMap(mazePath);
            message = $"'{mazePath}' pálya betöltve";
            NormalGame();
        }
        static void BlindGame()
        {
            int[,] index = new int[5, 6];
            int[,] asd = new int[10, 20];
            while (true)
            {
                Console.Clear();
                Console.WriteLine(asd.GetLength(0));
                Console.WriteLine(asd.GetLength(1));
                Console.WriteLine(index.GetLength(0));
                Console.WriteLine(index.GetLength(1));
                for (int y = 0; y < asd.GetLength(0); y++)
                {
                    for (int x = 0; x < asd.GetLength(1); x++)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        if (index.GetLength(0) == y && x == index.GetLength(1))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                        Console.Write(".");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        index = new int[index.GetLength(0) - 1, index.GetLength(1)];
                        break;
                    case ConsoleKey.A:
                        index = new int[index.GetLength(0), index.GetLength(1) - 1];
                        break;
                    case ConsoleKey.S:
                        index = new int[index.GetLength(0) + 1, index.GetLength(1)];
                        break;
                    case ConsoleKey.D:
                        index = new int[index.GetLength(0), index.GetLength(1) + 1];
                        break;
                }
            }
        }
        static void Menu()
        {
            bool isMenu = true;
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine();
                Console.WriteLine("Press (s) to start the game");
                Console.WriteLine("Press (d) to choose difficulty");
                Console.WriteLine("Press (l) to leave the game");
                Console.WriteLine("Press (b) to go back to menu");
                Console.WriteLine("Press (k) pálya betöltése");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                        isMenu = false;
                        break;
                    case ConsoleKey.K:
                        string mazePath = "minta.txt";
                        LoadMap(mazePath);
                        message = $"'{mazePath}' pálya betöltve";
                        break;
                    case ConsoleKey.D:
                        Console.Clear();
                        Console.WriteLine("Press (F1) for easy difficulty {The whole map is visible}");
                        Console.WriteLine("Press (F2) for easy difficulty {The map is not visible}");
                        Console.WriteLine("Press (F3) for easy difficulty {The whole map is visible but you must complete the game under the given time}");
                        Console.WriteLine("Press (F4) for easy difficulty {The map is not visible and you must complete the game under the given time}");
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.F1:
                                Console.Clear();
                                isMenu = false;
                                break;
                            case ConsoleKey.F2:
                                BlindGame();
                                break;
                        }
                        break;
                    case ConsoleKey.L:
                        Environment.Exit(0);
                        break;
                }
            } while (isMenu);
        }

        //Normal game
        static void NormalGame()
        {
            int[,] index = new int[1, 0];
            int[,] next = index;
            string? message = string.Empty;
            bool canMove = true;
            bool isRunning = true;
            maze[5, 8].SetExit(true);
            while (isRunning)
            {
                try
                {
                    Path current = maze[index.GetLength(0), index.GetLength(1)];
                    if (current.IsExit)
                    {
                        Console.WriteLine("This is an exit, are you sure you want to end the game? yes(enter)/no(backspace)");
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.Backspace:
                                canMove = true;
                                isRunning = true;
                                break;
                            case ConsoleKey.Enter:
                                message = "Vége a játéknak";
                                canMove = false;
                                isRunning = false;
                                break;
                        }
                    }
                    Display(index, message);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.W:
                            next = current.GoNorth();
                            break;
                        case ConsoleKey.A:
                            next = current.GoWest();
                            break;
                        case ConsoleKey.S:
                            next = current.GoSouth();
                            break;
                        case ConsoleKey.D:
                            next = current.GoEast();
                            break;
                    }
                    message = string.Empty;
                    if (maze[next.GetLength(0), next.GetLength(1)].Item == '.') throw new ImpossiblePathException();
                    index = next;
                }
                catch (ImpossiblePathException)
                {
                    message = "Nem bejárható út";
                }
                catch (IndexOutOfRangeException)
                {
                    message = "Nem bejárható út";
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
        }
        //Displays the player on the screen
        static void Display(int[,] index, string? message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            try
            {
                for (int y = 0; y < maze.GetLength(0); y++)
                {
                    for (int x = 0; x < maze.GetLength(1); x++)
                    {
                        if (index.GetLength(0) == y && x == index.GetLength(1))
                        {
                            //Player Color
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                        //Put player on screen
                        Console.Write(maze[y, x].Item);
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Üres a labirintus");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void LoadMap(string path)
        {
            List<string> lines = new List<string>();
            StreamReader streamReader = new StreamReader(path);
            string? line;
            while ((line = streamReader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            maze = new Path[lines.Count, lines[0].Length];
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    Path asd = mazeConponents.Where(z => z.Item == lines[y][x]).First();
                    maze[y, x] = new Path(asd.Item, new int[y, x], asd.West, asd.North, asd.East, asd.South);
                }
            }
        }

        static void FindExits() { }
        static void EditMap() { }
        static void SaveMap(string path) { }
        static void ChooseLanguage() { }
        static void ChooseDifficulty() { }
    }
}