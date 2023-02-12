using System;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace Maze
{
    internal class Program
    {
        static private Maze _maze;
        static private Player _player;
        static private string message = String.Empty;
        static private Dictionary<int, string>? _hungarianWords;
        static private Dictionary<int, string>? _englishWords;
        static string GlobalLanguage(int index)
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "hu" ? _hungarianWords![index] : _englishWords![index];
        }
        static void InitializeLanguages()
        {
            _hungarianWords = new Dictionary<int, string> {
                { 0, "Játék indítása"},
                { 1, "Beállítások"},
                { 2, "Kilépés"},
                { 3, "Nehézségi beállítások"},
                { 4, "Válasszon nehézséget: "},
                { 6, "A fájl nem található"},
                { 7, "Nyomja meg az (esc) gombot a játékbenü megnyitásához"},
                { 8, "Játék elmentve"},
                { 10, "Karakter színe"},
                { 11, "Nyelv"},
                { 12, "Vissza a menübe"},
                { 13, "Oda nem léphetsz"},
                { 16, "Sor"},
                { 17, "Oszlop"},
                { 18, "Pálya neve: "},
                { 19, "Pálya mérete: "},
                { 20, "Lépések száma: "},
                { 21, "Rendelkezésre álló lépések száma: "},
                { 22, "Felfedezett szobák száma: "},
                { 23, "Következő lépés: "},
                { 24, "Hátralévő idő: "},
                { 30, "Magyar"},
                { 31, "Angol"},
                { 32, "Válasszon nyelvet: "},
                { 40, "Játék betöltése: "},
                { 42, "Elfogytak a lépései" },
                { 50, "Könnyű"},
                { 51, "Haladó"},
                { 52, "Nehéz"},
                { 53, "GigaChad"},
                { 90, "Folytatás"},
                { 91, "Mentés"},
                { 92, "Kilépés"},
                { 98, "Nyomja meg az igen (y) gombot a megerősítéshez/ Nyomja meg a nem (n) gombot a visszalépéshez"},
                { 99, "Biztosan kiakarsz lépni?"},
                { 101, "Tényleg ugrani akartál egy felülnézetes játékban?"},
                { 110, "Piros"},
                { 111, "Zöld"},
                { 112, "Kék"},
                { 113, "Sárga"},
                { 114, "Lila"},
                { 115, "Játék vége"},
                { 116, "Mentés neve: "},
                { 117, "Nyomja meg az (y) a kilépéshez"},
                { 118, "Fájlnév: " },
                { 119, "Üres a labirintus"},
                { 120, "Biztosan ki akarsz lépni a labirintusból? "}
            };
            _englishWords = new Dictionary<int, string> {
                { 0, "Start the game" },
                { 1, "Settings"},
                { 2, "Exit the game"},
                { 3, "Difficulty"},
                { 4, "Choose Difficulty: "},
                { 6, "Couldn't find the file"},
                { 7, "Press (esc) to open the Pause Menu"},
                { 8, "Game saved"},
                { 10, "Player Color" },
                { 11, "Language" },
                { 12, "Return to menu"},
                { 13, "You can't go there"},
                { 16, "Row"},
                { 17, "Coloumn"},
                { 18, "Name of the map: "},
                { 19, "Size of the map: "},
                { 20, "Number of steps: "},
                { 21, "Number of remaining steps: "},
                { 22, "Number of detected treasure chambers: "},
                { 23, "Steppable directions: "},
                { 24, "Remaining time: "},
                { 30, "Hungarian"},
                { 31, "English"},
                { 32, "Choose Language: "},
                { 40, "Load Game"},
                { 42, "Out of Steps"},
                { 50, "Easy"},
                { 51, "Medium"},
                { 52, "Hard"},
                { 53, "GigaChad"},
                { 90, "Continue" },
                { 91, "Save"},
                { 92, "Exit"},
                { 98, "Press yes(y) to confirm/Press no(n) to go back"},
                { 99, "Are you sure you want to exit the game?"},
                { 101, "You really wanted to jump in a top-view game?"},
                { 110, "Red"},
                { 111, "Green"},
                { 112, "Blue"},
                { 113, "Yellow"},
                { 114, "Magenta"},
                { 115, "Game Over"},
                { 116, "Name of the save: "},
                { 117, "Press (y) to leave"},
                { 118, "File name: " },
                { 119, "The labyrinth is empty"},
                { 120, "Are you sure you want to leave the labyrinth?"}
            };

        }
        static void Main(string[] args)
        {
            InitializeLanguages();
            _player = new Player();
            MainMenu();
        }
        static void MainMenu()
        {
            int selectedOption = 0;
            bool exit = false;
            do
            {
                Console.Clear();
                Console.Write($"\n{message}\n\n");
                for (int i = 0; i < 3; i++)
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
                        selectedOption = selectedOption >= 2 ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 0 ? 2 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 0:
                                StartGameMenu();
                                Stopwatch timer = new Stopwatch();
                                timer.Start();
                                Game();
                                timer.Stop();
                                EndGame(timer.Elapsed.ToString());
                                break;
                            case 1:
                                SettingsMenu();
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine(GlobalLanguage(99));
                                Console.WriteLine(GlobalLanguage(98));

                                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                    Environment.Exit(0);
                                if (Console.ReadKey(true).Key == ConsoleKey.N)
                                    exit = true;
                                MainMenu();
                                break;
                        }
                        break;
                }
            } while (!exit);
        }
        static void SettingsMenu()
        {
            bool exit = false;
            int selectedOption = 10;
            do
            {
                Console.Clear();
                Console.Write($"\n{message}\n\n");
                for (int i = 10; i < 13; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(GlobalLanguage(i));
                    Console.ResetColor();
                }

                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedOption = selectedOption >= 12 ? 10 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 10 ? 12 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 10:
                                ChooseColor();
                                break;
                            case 11:
                                ChooseLanguage();
                                break;
                            case 12:
                                exit = true;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            } while (!exit);
        }
        static void ChooseLanguage()
        {
            bool exit = false;
            int selectedOption = 30;
            do
            {
                Console.Clear();
                Console.Write(GlobalLanguage(32));
                for (int i = 30; i < 32; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(GlobalLanguage(i));
                    Console.ResetColor();
                }

                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedOption = selectedOption >= 31 ? 30 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 30 ? 31 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 30:
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("hu-HU");
                                exit = true;
                                break;
                            case 31:
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");
                                exit = true;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            } while (!exit);
        }
        static void ChooseColor()
        {
            bool exit = false;
            int selectedOption = 110;
            do
            {
                Console.Clear();
                Console.Write($"\n{GlobalLanguage(32)}\n{message}\n");
                for (int i = 110; i < 115; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _englishWords[selectedOption]);
                    }
                    Console.WriteLine(GlobalLanguage(i));
                    Console.ResetColor();
                }
                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedOption = selectedOption >= 114 ? 110 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 110 ? 114 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        _player.SetColor((ConsoleColor)Enum.Parse(typeof(ConsoleColor), _englishWords[selectedOption]));
                        exit = true;
                        break;
                }
            } while (!exit);
        }
        static void StartGameMenu()
        {
            bool exit = false;
            int selectedOption = 40;
            do
            {
                Console.Clear();
                //Console.Write($"\n{GlobalLanguage(32)}\n{message}\n");
                for (int i = 40; i < 41; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(GlobalLanguage(i));
                    Console.ResetColor();
                }

                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 40:
                                LoadGame();
                                exit = true;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            } while (!exit);
        }
        static void Display(string? message)
        {
            Console.Clear();
            Console.Write($"{GlobalLanguage(20)} {_maze.Player.Steps}\t {GlobalLanguage(21)} {_maze.Player.RemainingSteps}\t TC:{_maze.Player.Treasure}\n");
            try
            {
                for (int y = 0; y < _maze!.Height; y++)
                {
                    for (int x = 0; x < _maze!.Width; x++)
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
                Console.WriteLine(GlobalLanguage(119));
                throw;
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
        static void Game()
        {
            bool exit = false;
            string message = string.Empty;
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
                            message = GlobalLanguage(101);
                            break;
                    }
                }
                catch (ImpassablePathException)
                {
                    message = GlobalLanguage(13);
                }
                catch (OutOfStepsException)
                {
                    message = GlobalLanguage(42);
                    exit = true;
                }
                catch (GiveupException)
                {
                    //TODO rename
                    
                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Thread.Sleep(1000);
                        exit = true;
                    }
                }
                catch (EndGameException)
                {
                    //TODO rename
                    Console.WriteLine(GlobalLanguage(120));
                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Thread.Sleep(1000);
                        exit = true;
                    }
                }
                catch (Exception ex)
                {
                    //TODO rename
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex);
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
                                message = GlobalLanguage(8);
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
        static void EndGame(string elapsedTime)
        {
            Console.WriteLine($"{GlobalLanguage(115)} - {elapsedTime}");
            Console.WriteLine(GlobalLanguage(117));
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    break;
                }
            }
        }
        static void LoadGame()
        {
            bool exit = false;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write($"\n{message}\n{GlobalLanguage(118)}");
                    string filename = Console.ReadLine();
                    if (filename.EndsWith(".txt"))
                    {
                        _maze = new Maze(File.ReadAllLines(filename), ChooseDifficulty(), _player);
                        exit = true;
                    }
                    else if (filename.EndsWith(".sav"))
                    {
                        StreamReader streamReader = new StreamReader(filename);
                        // player betölése
                        Console.WriteLine(streamReader.ReadLine());
                        // pálya betöltése
                        while (!streamReader.EndOfStream)
                        {
                            foreach (Char item in streamReader.ReadLine())
                            {
                                Console.Write(item);
                            }
                            Console.WriteLine();
                        }
                        exit = true;
                    }
                }
                catch (FileNotFoundException)
                {
                    message = GlobalLanguage(6);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            } while (!exit);
        }
        static Difficulty ChooseDifficulty()
        {
            bool exit = false;
            int selectedOption = 50;
            Difficulty difficulty = Difficulty.Easy;
            do
            {
                Console.Clear();
                Console.Write($"\n{GlobalLanguage(4)}\n{message}\n");
                for (int i = 50; i < 54; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(GlobalLanguage(i));
                    Console.ResetColor();
                }

                Console.Write("\n\n\n");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedOption = selectedOption >= 53 ? 50 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 50 ? 53 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), _englishWords[selectedOption]);
                        exit = true;
                        break;
                }
            } while (!exit);
            return difficulty;
        }
        static void SaveGame()
        {
            try
            {
                Console.Clear();
                Console.WriteLine(GlobalLanguage(116));
                string filename = Console.ReadLine();
                if (!filename.EndsWith(".sav")) filename += ".sav";

                StreamWriter streamWriter = new StreamWriter(filename);

                // infos
                // direkt nem tároljuk a maradék lépéseket, hogy könnyebben lehessen csalni.
                streamWriter.WriteLine($"P:{_player.Coordinate};S:{_player.Steps};D:{_maze.Difficulty};TC:{_player.Treasure};T:{""}");
                Console.WriteLine($"P:{_player.Coordinate};S:{_player.Steps};D:{_maze.Difficulty};TC:{_player.Treasure};T:{""}");

                // map
                for (int y = 0; y < _maze.Height; y++)
                {
                    for (int x = 0; x < _maze.Width; x++)
                    {
                        streamWriter.Write(_maze.Map[y, x].Item);
                    }
                    streamWriter.WriteLine();
                }
            }
            catch (Exception ex)
            {
                //TODO rename
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}