using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class Maze
    {
        //List of all the coompontent of the map, and some conditions to them
        static public List<Path> dirtyWordsXAML = new List<Path>()
        {
            new Path('.', isWall: true, isTreasure: false, west: false, north: false, east: false, south: false),
            new Path('█', isWall: false, isTreasure: true, west: true, north: true, east: true, south: true),
            new Path('╬', isWall: false, isTreasure: false, west: true, north: true, east: true, south: true),
            new Path('═', isWall: false, isTreasure: false, west: true, north: false, east: true, south: false),
            new Path('╦', isWall: false, isTreasure: false, west: true, north: false, east: true, south: true),
            new Path('╩', isWall: false, isTreasure: false, west: true, north: true, east: true, south: false),
            new Path('║', isWall: false, isTreasure: false, west: false, north: true, east: false, south: true),
            new Path('╣', isWall: false, isTreasure: false, west: true, north: true, east: false, south: true),
            new Path('╠', isWall: false, isTreasure: false, west: false, north: true, east: true, south: true),
            new Path('╗', isWall: false, isTreasure: false, west: true, north: false, east: false, south: true),
            new Path('╝', isWall: false, isTreasure: false, west: true, north: true, east: false, south: false),
            new Path('╚', isWall: false, isTreasure: false, west: false, north: true, east: true, south: false),
            new Path('╔', isWall: false, isTreasure: false, west: false, north: false, east: true, south: true)
        };

        // Player
        private readonly Player _player;
        public Player Player => _player;

        // Exits
        private List<Coordinate>? _exits;
        public string Exit => string.Join(", ", from e in _exits select String.Format("X: {0} Y:{1}", e.X, e.Y));
        public List<Coordinate> Exits => _exits!;

        // Entrance
        private Coordinate? _entrance;
        public string Entrance => _entrance!.ToString();

        // TreasureChambers
        private List<Coordinate>? _treasureChambers;
        public string TreasureChambers => string.Join(", ", from e in _treasureChambers select String.Format("X: {0} Y:{1}", e.X, e.Y));

        // Map
        private Path[,]? _map;
        public Path[,] Map => _map!;

        // Map Size
        public int Height => _map!.GetLength(0);
        public int Width => _map!.GetLength(1);

        // Difficulty
        private Difficulty _difficulty;
        public Difficulty Difficulty => _difficulty;

        public Maze(string[] data, Difficulty difficulty, Player player)
        {
            _map = null;
            LoadMap(data);

            // get exits and treasure chambers and entrance
            GetInteractivePaths();

            // set difficulty
            _difficulty = difficulty;

            // give player and set to the start point.
            _player = player;
            _player.SetRemainingSteps(StepByDifficulty());
            if (_player.Coordinate.CheckCoordinate(0, 0))
            {
                _player.Step(_entrance!);
            }
        }

        //Loads the map from the given file
        public void LoadMap(string[] data)
        {
            _map = new Path[data.Length, data[0].Length];
            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    Path path = dirtyWordsXAML.Where(z => z.Item == data[y][x]).First();
                    _map[y, x] = new Path(path.Item, new Coordinate(y, x), path.West, path.North, path.East, path.South);
                }
            }
        }
        //Returns the item on each coordinate
        public char GetMapItem(Coordinate coordinate)
        {
            return _map![coordinate.Y, coordinate.X].Item;
        }

        //Returns the item on each index of the map
        public char GetMapItem(int y, int x)
        {
            return _map![y, x].Item;
        }
        //Moves the player int the maze
        public void MovePlayer(Direction direction)
        {
            Coordinate coordinate = _player.Coordinate;
            switch (direction)
            {
                case Direction.West:
                    if (!_map![_player.Coordinate.Y, _player.Coordinate.X].West) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y, _player.Coordinate.X - 1);
                    break;
                case Direction.North:
                    if (!_map![_player.Coordinate.Y, _player.Coordinate.X].North) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y - 1, _player.Coordinate.X);
                    break;
                case Direction.East:
                    if (!_map![_player.Coordinate.Y, _player.Coordinate.X].East) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y, _player.Coordinate.X + 1);
                    break;
                case Direction.South:
                    if (!_map![_player.Coordinate.Y, _player.Coordinate.X].South) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y + 1, _player.Coordinate.X);
                    break;
            }
            if (!IsOnTheMap(coordinate))
            {
                if (_entrance!.CheckCoordinate(_player.Coordinate))
                {
                    throw new GiveupException();
                }
                throw new EndGameException();
            }
            if (_map![coordinate.Y, coordinate.X].Item == '.')
            {
                throw new ImpassablePathException();
            }
            if (_map[coordinate.Y, coordinate.X].Item == '█')
            {
                foreach (Coordinate item in _treasureChambers!)
                {
                    if (item.CheckCoordinate(coordinate))
                    {
                        _player.AddTreasure();
                        _treasureChambers.Remove(item);
                    }
                }
            }
            _player.Step(coordinate);
        }
        public void GetInteractivePaths()
        {
            _treasureChambers = new List<Coordinate>();
            _exits = new List<Coordinate>();
            _entrance = null;

            List<Coordinate> temp = new List<Coordinate>();

            foreach (Path item in _map!)
            {
                // tc
                if (item.Item == '█')
                {
                    _treasureChambers.Add(item.Coordinate);
                    continue;
                }
                // exits
                if ((item.West && !IsOnTheMap(item.GetCoordinateOf(Direction.West))) ||
                    (item.North && !IsOnTheMap(item.GetCoordinateOf(Direction.North))) ||
                    (item.East && !IsOnTheMap(item.GetCoordinateOf(Direction.East))) ||
                    (item.South && !IsOnTheMap(item.GetCoordinateOf(Direction.South)))
                    )
                {
                    temp.Add(item.Coordinate);
                    continue;
                }
            }
            // entrance
            foreach (Coordinate item in temp)
            {
                if (_entrance == null)
                {
                    _entrance = item;
                    continue;
                }
                _exits!.Add(item);
            }
        }
        //Watches if 
        public bool IsOnTheMap(Coordinate coordinate)
        {
            foreach (Path item in _map!)
            {
                if (item.Coordinate.Y == coordinate.Y && item.Coordinate.X == coordinate.X)
                {
                    return true;
                }
            }
            return false;
        } 
        //Returns the number of the map compontent that you can go on
        public int GetMapPassableLength()
        {
            int length = _map!.Length;
            foreach (var item in _map)
            {
                if (item.Item == '.' || item.Item == '█' || item.Item == ' ')
                {
                    length--;
                }
            }
            return length;
        }
        //Calculates the max steps by the given difficulty
        public int StepByDifficulty()
        {
            return GetMapPassableLength() * (int)_difficulty;
        }
    }
}