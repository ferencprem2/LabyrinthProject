using System;
using System.Collections.Generic;

namespace ConsoleApp23
{
    class Program
    {
        static int GetRoomNumber(char[,] map)
        {
            var query = from char room in map
                        where room == '█'
                        select room;
            if (query.Count() > 0)
            {
                return query.Count();
            }
            else
            {
                return -1;
            }
        }

        static int GetSuitableEntrance(char[,] map)
        {
            int suitableEntrances = 0;
            int rows = map.GetLength(0);
            int columns = map.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == 0  i == rows - 1   j == 0 || j == columns - 1 ) 
{
                if (map[i, j]) == '█' suitableEntrances++;
            }
            return -1;
        }
   
    static bool IsInvalidElement(char[,] map)
    {
        string allowedCharacters = ".█╬═╦╩║╣╠╗╝╚╔";

        int rows = map.GetLength(0);
        int columns = map.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (!allowedCharacters.Contains(map[i, j]))
                {
                    return true;
                }
            }
        }

        return false;
    }
    }
}