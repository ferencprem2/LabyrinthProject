using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Path
    {
        private readonly char _item;
        private bool _isExit, _isEntrace ,_west, _north, _east, _south;
        private int[,] _index;
        public char Item => _item;
        public bool West => _west;
        public bool North => _north;
        public bool East => _east;
        public bool South => _south;
        public bool IsExit => _isExit;
        public bool IsEntrance => _isEntrace;
        public int[,] Index => _index;
        public Path(char item, bool west, bool north, bool east, bool south)
        {
            _item = item;

            _west = west;
            _north = north;
            _east = east;
            _south = south;
        }
        public Path(char item, int[,] index)
        {
            _item = item;
            _index = index;

            _west = false;
            _north = false;
            _east = false;
            _south = false;
        }
        public Path(char item, int[,] index, bool west, bool north, bool east, bool south)
        {
            _item = item;
            _index = index;

            _west = west;
            _north = north;
            _east = east;
            _south = south;
        }
        public void SetIndex(int[,] index)
        {
            _index = index;
        }
        public void SetExit(bool isExit)
        {
            _isExit = isExit;
        }
        public void SetEntrance(bool isEntrance)
        {
            _isEntrace = isEntrance;
        }
        public int[,] GoWest()
        {
            if (!_west) throw new ImpossiblePathException();
            return new int[_index.GetLength(0), _index.GetLength(1) - 1];
        }
        public int[,] GoNorth()
        {
            if (!_north) throw new ImpossiblePathException();
            return new int[_index.GetLength(0) - 1, _index.GetLength(1)];
        }
        public int[,] GoEast()
        {
            if (!_east) throw new ImpossiblePathException();
            return new int[_index.GetLength(0), _index.GetLength(1) + 1];
        }
        public int[,] GoSouth()
        {
            if (!_south) throw new ImpossiblePathException();
            return new int[_index.GetLength(0) + 1, _index.GetLength(1)];
        }
    }
}
