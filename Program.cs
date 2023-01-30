using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Labyrinth
{



    internal class Program
    {

        static void Main(string[] args)
        {
            int xAxis = 0;
            int yAxis = 0;
            



            string path = "minta.txt";
            bool leaveGame = false;
            Console.WriteLine("Press (s) to start the game");
            Console.WriteLine("Press (d) to choose difficulty");
            Console.WriteLine("Press (l) to leave the game");
            do
            {
                var userOptions = Console.ReadKey().Key;
                switch (userOptions)
                {
                    case ConsoleKey.S:
                        Console.Clear();
                        LoadMapFromFile(path);
                        DisplayMapOnScreen(LoadMapFromFile(path), xAxis, yAxis);
                        RoomInMap(LoadMapFromFile(path));
                        LocateExitPoints(LoadMapFromFile(path));
                        FakeExitChecker(LocateExitPoints(LoadMapFromFile(path)), LoadMapFromFile(path));
                        break;
                    case ConsoleKey.D:
                        break;
                    case ConsoleKey.L:
                        Environment.Exit(0);
                        break;
                }


            } while (!leaveGame);
        }

        static char[,] LoadMapFromFile(string path)
        {
            string[] data = File.ReadAllLines(path);
            char[,] map = new char[data.Length, data[0].Length];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[0].Length; j++)
                {
                    map[i, j] = data[i][j];
                }
            }

            return map;
        }

        static void DisplayMapOnScreen(char[,] map, int xAxis, int yAxis)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.SetCursorPosition(xAxis + j, yAxis + i);
                    Console.Write(map[i, j]);
                    Console.WriteLine();
                }
            }
        }

        static int RoomInMap(char[,] map)
        {
            char room = '█';
            int roomsInMap = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == room)
                    {
                        roomsInMap++;
                    }
                }
            }



            Console.WriteLine($"The number of rooms on the map is: {roomsInMap}");

            return roomsInMap;
        }

        static List<string> LocateExitPoints(char[,] map)
        {
            
            
            char[] leftsideExits = {'╬', '═', '╦', '╩', '╣', '╗', '╝'};
            char[] rightsideExits = {'╬', '═', '╦', '╩','╠', '╚', '╔' };
            char[] topsideExits = {'╬','╩', '║', '╣', '╠','╝', '╚'};
            char[] bottomtsideExits = {'╬','╦','║', '╣', '╠', '╗','╔' };
            int coordinateIndex = 0;
            ValueTuple<int, int, int> exitCoordinates = new ValueTuple<int, int, int>();
            List<string> realExitCoordinates = new List<string>();
            


            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == 0 && topsideExits.Contains(map[i, j]))
                    {
                        exitCoordinates.Item1 = coordinateIndex;
                        exitCoordinates.Item2 = i;
                        exitCoordinates.Item3 = j;
                        coordinateIndex++;
                        realExitCoordinates.Add($"{i}-{j}");

                    }
                    if (i == map.GetLength(0)-1 && bottomtsideExits.Contains(map[i, j]))
                    {
                        realExitCoordinates.Add($"{i}-{j}");
                    }
                    if (j == 0 && leftsideExits.Contains(map[i, j]))
                    {
                        realExitCoordinates.Add($"{i}-{j}");
                    }
                    if (j == map.GetLength(1)-1 && rightsideExits.Contains(map[i, j]))
                    {
                        realExitCoordinates.Add($"{i}-{j}");
                    }
                }
            }

            Console.WriteLine(exitCoordinates);

            return realExitCoordinates;

        }

        static void FakeExitChecker(List<string>exits, char[,] map)
        {
            ValueTuple<int, int, int> realExitCoordinates = new ValueTuple<int, int, int>();   
          
            


        }
    }

}