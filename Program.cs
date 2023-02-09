using System;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.ComponentModel.DataAnnotations;
using Labyrinth;

namespace Maze
{
    internal class Program
    {
        static private Maze _maze;
        static private string message = String.Empty;
        static private Dictionary<int, string> _hungarianWords;
        static private Dictionary<int, string> _englishWords;
        static string GlobalLanguage(int index)
        {
            return Thread.CurrentThread.CurrentCulture.EnglishName == "hu-HU" ? _hungarianWords[index] : _englishWords[index];
        }

        static void InitializeLanguages()
        {
            _hungarianWords = new Dictionary<int, string> {
                { 0, "Nyomja meg az (s) gombot a játék elindításához"},
                { 1, "Nyomja meg a (q) gombot a beállítások megnyitásához"},
                { 2, "Nyomja meg az (e) a játékhoz való kilépéshez"},
                { 3, "Nyomja meg a (d) gombot a nehézség beállításához"},
                { 4, "Nyomja meg az (m) gombot új pálya készítéséhez"},
                { 5, "Nyomja meg az (l) gombot a pálya betöltéséhez"},
                { 6, "Nyomja meg a (b) gombot a menübe való visszalépéshez"},
                { 7, "Nyomja meg az (esc) gombot a játékbenü megnyitásához"},
                { 10, "Karakter színe"},
                { 11, "Nyelv"},
                { 90, "Folytatás"},
                { 91, "Mentés"},
                { 92, "Kilépés"},
                { 98, "Nyomja meg az igen (y) gombot a megerősítéshez/ Nyomja meg a nem (n) gombot a visszalépéshez"},
                { 99, "Biztosan kiakarsz lépni?"}
            };
            _englishWords = new Dictionary<int, string> {
                { 0, "Press (s) to start the game" },
                { 1, "Press (q) to open settings"},
                { 2, "Press (e) to exit the game"},
                { 3, "Press (d) to choose difficulty"},
                { 4, "Press (m) to make a map" },
                { 5, "Press (l) to load map" },
                { 6, "Press (b) to return to menu"},
                { 7, "Press (esc) to open the Pause Menu"},
                { 10, "Player Color" },
                { 11, "Language" },
                { 90, "Continue" },
                { 91, "Save"},
                { 92, "Exit"},
                { 98, "Press yes(y) to confirm/Press no(n) to go back"},
                { 99, "Are you sure you wanna exit the game?"}
            };
        }
        static void Main(string[] args)
        {
            InitializeLanguages();
            MainMenu();
            Game();
        }
        static void MainMenu()
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Console.Write($"\n{message}\n\n");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(GlobalLanguage(i));
                }
                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                        exit = true;
                        break;
                    case ConsoleKey.Q:
                        SettingsMenu();
                        break;
                    case ConsoleKey.E:
                        Console.Clear();
                        Console.WriteLine(GlobalLanguage(99));
                        Console.WriteLine(GlobalLanguage(98));

                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                            Environment.Exit(0);
                        if (Console.ReadKey(true).Key == ConsoleKey.N)
                            exit = true;
                        break;
                    default:
                        break;
                }
            } while (!exit);
        }
        static void SettingsMenu()
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Console.Write($"\n{message}\n\n");
                for (int i = 10; i < 12; i++)
                {
                    Console.WriteLine(GlobalLanguage(i));
                }
                Console.WriteLine(GlobalLanguage(6));
                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.B:
                        exit = true;
                        break;
                    default:
                        break;
                }
            } while (!exit);
        }

        static void GamePauseMenu()
        {
            bool exit = false;
            string message = string.Empty;
            int selectedOption = 90;
            do
            {
                Console.Clear();
                Console.Write($"\n{message}\n\n");
                message = string.Empty;
                for (int i = 90; i < 93; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(GlobalLanguage(i));
                    Console.ResetColor();
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedOption = selectedOption >= 92 ? 90 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 90 ? 92 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 90:
                                exit = true;
                                break;
                            case 91:
                                SaveGame();
                                message = "game saved";
                                break;
                            case 92:
                                MainMenu();
                                exit = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            } while (!exit);

        }
        static void MapMenu() { }
        static void Game()
        {
            LoadMap("minta.txt");
            bool exit = false;
            string message = string.Empty;
            _maze.exits = new Path[,] { {5, 7}, };
            do
            {
                Display(message);
                message = string.Empty;
                try
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape:
                            GamePauseMenu();
                            break;
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            _maze.MovePlayer(Direction.West);
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            _maze.MovePlayer(Direction.North);
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            _maze.MovePlayer(Direction.East);
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            _maze.MovePlayer(Direction.South);
                            break;
                        case ConsoleKey.Spacebar:
                            message = "Most komolyan ugrani szeretnél egy felülnézetes játékban?";
                            break;
                    }
                }
                catch (ImpassablePathException)
                {
                    message = "Ide nem léphetsz";
                }
            } while (!exit);

        }
        static void Display(string? message)
        {
            
            Console.Clear();
            Console.Write($"\n{message}\n\n");
            //Puts the player on screen.
            try
            {
                for (int y = 0; y < _maze.Height; y++)
                {
                    for (int x = 0; x < _maze.Width; x++)
                    {
                        if (_maze.Player.Coordinate.CheckCoordinate(y, x))
                        {
                            Console.BackgroundColor = _maze.Player.Color;
                        }
                        Console.Write(GetVoidChar(_maze.GetMapItem(new Coordinate(y, x))));
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
                throw;
            }
            Console.Write("\n\n\n");
        }
        static char GetVoidChar(char c)
        {
            if (c == '.')
            {
                return ' ';
            }
            return c;
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

            _maze = new Maze();
            _maze.LoadMap(lines);
        }
        static void EditMap() { }
        static void SaveMap(string path) { }
        static void ChooseLanguage() { }
        static void ChooseDifficulty() { }
        static void SaveGame()
        {
            Console.Clear();
            Console.WriteLine("name:");
            Console.ReadLine();
        }
    }
}
