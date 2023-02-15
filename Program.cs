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
        //Initialize the maze
        static private Maze? _maze;
        //Initialize the color
        static private ConsoleColor _color = ConsoleColor.Green;
        //Message variable for custom meassages
        static private string message = string.Empty;
        //Dictionary with hungarian words in it for globalisation
        static private Dictionary<int, string>? _hungarianWords;
        //Dictionary with english words in it for globalisation
        static private Dictionary<int, string>? _englishWords;
        //Returns the words in accordance to the language choice
        static string GlobalLanguage(int index)
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "hu" ? _hungarianWords![index] : _englishWords![index];
        }
        //Holds the dictionarys with the words
        static void InitializeLanguages()
        {
            _hungarianWords = new Dictionary<int, string> {
                { 0, "Játék indítása"},
                { 1, "Beállítások"},
                { 2, "Kilépés"},
                { 3, "Nehézségi beállítások"},
                { 4, "Válassz nehézséget: "},
                { 6, "A fájl nem található"},
                { 7, "Nyomd meg az (esc) gombot a játékbenü megnyitásához"},
                { 8, "Játék elmentve"},
                { 9, "Irányítás" },
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
                { 32, "Válassz nyelvet: "},
                { 40, "Játék betöltése: "},
                { 42, "Elfogytak a lépéseid" },
                { 50, "Könnyű"},
                { 51, "Haladó"},
                { 52, "Nehéz"},
                { 53, "GigaChad"},
                { 90, "Folytatás"},
                { 91, "Mentés"},
                { 92, "Kilépés"},
                { 98, "Nyomd meg az igen (y) gombot a megerősítéshez/ Nyomd meg a nem (n) gombot a visszalépéshez"},
                { 99, "Biztosan kiakarsz lépni?"},
                { 101, "Tényleg ugrani akartál egy felülnézetes játékban?"},
                { 110, "Piros"},
                { 111, "Zöld"},
                { 112, "Kék"},
                { 113, "Sárga"},
                { 114, "Lila"},
                { 115, "Játék vége"},
                { 116, "Mentés neve: "},
                { 117, "Nyomd meg az (y) a kilépéshez"},
                { 118, "Fájlnév: " },
                { 119, "Üres a labirintus"},
                { 120, "Biztosan ki akarsz lépni a labirintusból? "},
                { 121, "Hibás fájl"},
                { 122, "Biztos ki szeretnél lépni a labirintusból?"},
                { 123, "El szeretnéd menteni a játékot? " },
                { 124, "Igen(y) / Nem(n)"},
                { 125, "A w;a;s;d billentyűkkel vagy a nyilakkal tudsz mozogni"},
                { 126, "Pause menu megynyitása az (esc) billentyűvel"},
                { 127, "Vissza a beállításokhoz"}
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
                { 9, "Controls"},
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
                { 120, "Are you sure you want to leave the labyrinth?"},
                { 121, "The file is incorrect"},
                { 122, "Are you sure you want to leave the game?"},
                { 123, "Do you want to save the game? "},
                { 124, "Yes(y) / No(n)"},
                { 125, "You can move with w;a;s;d or with the arrow keys"},
                { 126, "You can open the pause menu with the (esc) key"},
                { 127, "Back to the settings"}
            };

        }
        static void Main(string[] args)
        {
            InitializeLanguages();
            MainMenu();
        }
        //Function of the main menu
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
        //Function of the keybinds menu
        static void KeyBinds()
        {
            bool exit = false;
            int selectedOption = 125;
            do
            {
                Console.Clear();
                Console.WriteLine($"\n{GlobalLanguage(9)}:\n");

                for (int i = 125; i < 128; i++)
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
                        selectedOption = selectedOption >= 127 ? 125 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 125 ? 127 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 127:
                                exit = true;
                                break;
                        }
                        break;
                }

            } while (!exit);
        }

        //Function of the settings menu
        static void SettingsMenu()
        {
            bool exit = false;
            int selectedOption = 9;
            do
            {
                Console.Clear();
                for (int i = 9; i < 13; i++)
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
                        selectedOption = selectedOption >= 12 ? 9 : selectedOption + 1;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedOption = selectedOption <= 9 ? 12 : selectedOption - 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 9:
                                KeyBinds();
                                break;
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
        //The function the player can select the language of the game with
        static void ChooseLanguage()
        {
            bool exit = false;
            int selectedOption = 30;
            do
            {
                Console.Clear();
                Console.Write($"\n{GlobalLanguage(32)}\n");
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
        //The function the player can select the color of their character with
        static void ChooseColor()
        {
            bool exit = false;
            int selectedOption = 110;
            do
            {
                Console.Clear();
                Console.Write($"\n{GlobalLanguage(32)}\n");
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
                        _color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _englishWords[selectedOption]);
                        exit = true;
                        break;
                }
            } while (!exit);
        }
        //Function of the game starter menu
        static void StartGameMenu()
        {
            bool exit = false;
            int selectedOption = 40;
            do
            {
                Console.Clear();
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
        //Funtion that displays the map and the other necessary informations
        static void Display(string? message)
        {
            Console.Clear();
            Console.Write($"{GlobalLanguage(20)} {_maze!.Player.Steps}\t {GlobalLanguage(21)} {_maze!.Player.RemainingSteps}\t TC:{_maze!.Player.Treasure}\n");
            Console.WriteLine($"{_maze!.Player.Coordinate}");
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
        //Changes the '.' character with white spaces
        static char GetVoidChar(char c)
        {
            if (c == '.')
            {
                return ' ';
            }
            return c;
        }
        //Function of the main game
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
                            _maze!.MovePlayer(Direction.West);
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            _maze!.MovePlayer(Direction.North);
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            _maze!.MovePlayer(Direction.East);
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            _maze!.MovePlayer(Direction.South);
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
                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Thread.Sleep(1000);
                        exit = true;
                    }
                }
                catch (EndGameException)
                {
                    Console.WriteLine(GlobalLanguage(120));
                    Console.WriteLine(GlobalLanguage(124));
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Y:
                            Thread.Sleep(1000);
                            exit= true;
                            break;
                        case ConsoleKey.N:
                            exit= false;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (!exit);
        }
        //Funtion of the game pause menu
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
                                Console.WriteLine(GlobalLanguage(122));
                                Console.WriteLine(GlobalLanguage(124));
                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.Y:
                                        Console.WriteLine(GlobalLanguage(123));
                                        Console.WriteLine(GlobalLanguage(124));
                                        switch (Console.ReadKey(true).Key)
                                        {
                                            case ConsoleKey.Y:
                                                SaveGame();
                                                MainMenu();
                                                exit = true;
                                                break;
                                            case ConsoleKey.N:
                                                MainMenu();
                                                exit = true;
                                                break;
                                        }
                                        break;
                                    case ConsoleKey.N:
                                        exit = false;
                                        break;
                                }
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
        //Function that ends the game with the 'y' key
        static void EndGame(string elapsedTime)
        {
            Console.WriteLine($"{GlobalLanguage(115)} - {elapsedTime} - {GlobalLanguage(22)}  {_maze!.Player.Treasure}");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    break;
                }
            }
        }
        //Function that loads the game from a txt or loads a save
        static void LoadGame()
        {
            bool exit = false;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write($"\n{GlobalLanguage(118)}");
                    string filename = Console.ReadLine()!;
                    if (filename.EndsWith(".txt"))
                    {
                        _maze = new Maze(File.ReadAllLines(filename), ChooseDifficulty(), new Player(_color));
                        exit = true;
                    }
                    else if (filename.EndsWith(".sav"))
                    {
                        StreamReader? streamReader = null;
                        try
                        {
                            streamReader = new StreamReader(filename);
                            // player betölése
                            string[] infos = streamReader.ReadLine()!.Split(';');

                            // pálya betöltése
                            List<string> tempMap = new List<string>();
                            while (!streamReader.EndOfStream)
                            {
                                tempMap.Add(streamReader.ReadLine()!);
                            }

                            Player player = new Player(new Coordinate(infos[0].Substring(2)),
                                _color,
                                int.Parse(infos[1].Substring(2)),
                                int.Parse(infos[3].Substring(2)));

                            _maze = new Maze(
                                tempMap.ToArray(),
                                (Difficulty)Enum.Parse(typeof(Difficulty), infos[2].Substring(2)),
                                player);

                            _maze.Player.SetRemainingSteps(_maze.StepByDifficulty() - _maze.Player.Steps);
                            exit = true;
                        }
                        finally
                        {
                            streamReader!.Peek();
                            streamReader!.Close();
                        }

                    }
                }
                catch (FileNotFoundException)
                {
                    message = GlobalLanguage(6);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(GlobalLanguage(121));
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(1000);
                    Console.ReadKey();
                    throw;
                }
            } while (!exit);
        }
        //Function that the player can choose a difficulty with
        static Difficulty ChooseDifficulty()
        {
            bool exit = false;
            int selectedOption = 50;
            Difficulty difficulty = Difficulty.Easy;
            do
            {
                Console.Clear();
                Console.Write($"\n{GlobalLanguage(4)}\n");
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
        //Function that saves the game according to the choices of the player
        static void SaveGame()
        {
            StreamWriter? streamWriter = null;
            try
            {
                Console.Clear();
                Console.WriteLine(GlobalLanguage(116));
                string filename = Console.ReadLine()!;
                if (!filename.EndsWith(".sav")) filename += ".sav";

                streamWriter = new StreamWriter(filename);

                // infos
                streamWriter.WriteLine($"P:{_maze!.Player.Coordinate.ToFile()};S:{_maze!.Player.Steps};D:{_maze!.Difficulty};T:{_maze!.Player.Treasure};C:{""}");

                // map
                for (int y = 0; y < _maze.Height; y++)
                {
                    for (int x = 0; x < _maze.Width; x++)
                    {
                        streamWriter.Write(_maze.GetMapItem(y, x));
                    }
                    streamWriter.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }           
        }
    }
}