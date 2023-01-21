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
        public enum MapComponents
        {
            fillerElement = '.',
            roomElement = '█',
            fourDirectionalElement = '╬',
            horizontalRouteElement = '═',
            horizontalDownwardsThreeDirectionalElement = '╦',
            horizontalUpwardsThreeDirectionalElement = '╩',
            verticalRouteElement = '║',
            verticalLeftwardsThreeDirectionalElement = '╣',
            verticalRightwardsThreeDirectionalElement = '╠',
            horizontalLeftturnElement = '╗',
            horizontalRightturnElement = '╔',
            verticalLeftturnElement = '╝',
            verticalRightturnElement = '╔'
        }


        static void Main(string[] args)
        {
            int xAxis = 0;
            int yAxis = 0;
            //char[] mapComponents = { '.', '█', '╬', '═', '╦', '╩', '║', '╣', '╠', '╗', '╝', '╚', '╔' };



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
                        LocateExtractPoints(LoadMapFromFile(path), xAxis, yAxis);
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
            //char room = '█';
            int roomsInMap = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == (char)MapComponents.roomElement)
                    {
                        roomsInMap++;
                    }
                }
            }



            Console.WriteLine($"The number of rooms on the map is: {roomsInMap}");

            return roomsInMap;
        }

        static Dictionary<int, int> LocateExtractPoints(char[,] map, int xAxis, int yAxis)
        {
            Dictionary<int, int> extractPoints = new Dictionary<int, int>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {

                    if (j == 0 || j == map.GetLength(1) - 1)
                    {
                        switch (map[i, j])
                        {
                            case (char)MapComponents.horizontalRouteElement:
                                Console.WriteLine($"{MapComponents.horizontalRouteElement}--{i}-{j}");
                                break;
                        }
                    }
                    else if (j == 0 || i == map.GetLength(1) - 1)
                    {
                        switch (map[i, j])
                        {
                            case (char)MapComponents.horizontalLeftturnElement:
                                Console.WriteLine($"{MapComponents.horizontalLeftturnElement}--{i}-{j}");
                                break;

                        }
                    }
                    else if (i == 0 || i == map.GetLength(0) - 1)
                    {
                        switch (map[i, j])
                        {
                            case (char)MapComponents.verticalRouteElement:
                                Console.WriteLine($"{MapComponents.verticalRouteElement}--{i}-{j}");
                                break;
                        }
                    }
                }
            }


            return extractPoints;
        }


        static void RoomsFound(char[,] map, int roomsOnMap)
        {

        }
    }
}
//if (map[i, j] == mapComponents[3] && (j == 0 || j == map.GetLength(1) - 1))
//{
//    Console.WriteLine($"{i}-{j}");
//}
