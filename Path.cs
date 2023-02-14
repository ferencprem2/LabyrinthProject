using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class Path
    {
        
        //The item(character) of each coordinate
        private readonly char _item;
        //Setting up some cases for the game
        private bool _isExit, _isWall, _isTreasure, _west, _north, _east, _south;
        private Coordinate _coordinate;
        public char Item => _item;
        public bool West => _west;
        public bool North => _north;
        public bool East => _east;
        public bool South => _south;
        public bool IsExit => _isExit;
        public Coordinate Coordinate => _coordinate;

        public Path() { }
        public Path(char item, bool isWall, bool isTreasure, bool west, bool north, bool east, bool south)
        {
            _item = item;

            _isWall = isWall;
            _isTreasure = isTreasure;

            _west = west;
            _north = north;
            _east = east;
            _south = south;
        }
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

        //Returns the coordinate of something you want
        public Coordinate GetCoordinateOf(Direction direction)
        {
            switch (direction)
            {
                case Direction.West:
                    return new Coordinate(_coordinate.Y, _coordinate.X - 1);
                case Direction.North:
                    return new Coordinate(_coordinate.Y - 1, _coordinate.X);
                case Direction.East:
                    return new Coordinate(_coordinate.Y, _coordinate.X + 1);
                case Direction.South:
                    return new Coordinate(_coordinate.Y + 1, _coordinate.X);
            }
            return _coordinate;
        }
    }
}
