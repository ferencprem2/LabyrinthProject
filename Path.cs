using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Labyrinth;

namespace Maze
{
    public class Path
    {
        // test
        private readonly char _item;
        private bool _isExit, _west, _north, _east, _south;
        private Coordinate _coordinate;
        public char Item => _item;
        public bool West => _west;
        public bool North => _north;
        public bool East => _east;
        public bool South => _south;
        public bool IsExit => _isExit;
        public Coordinate Coordinate => _coordinate;
        // idegesít
        public Path() { }
        public Path(char item, bool west, bool north, bool east, bool south)
        {
            _item = item;

            _west = west;
            _north = north;
            _east = east;
            _south = south;
        }
        public Path(char item, Coordinate coordinate)
        {
            _item = item;
            _coordinate = coordinate;

            _west = false;
            _north = false;
            _east = false;
            _south = false;
        }
        public Path(char item, Coordinate coordinate, bool west, bool north, bool east, bool south)
        {
            _item = item;
            _coordinate = coordinate;

            _west = west;
            _north = north;
            _east = east;
            _south = south;
        }
        public void SetIndex(Coordinate coordinate)
        {
            _coordinate = coordinate;
        }
        public void SetExit(bool isExit)
        {
            _isExit = isExit;
        }
    }
}