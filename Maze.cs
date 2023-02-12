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


        private Difficulty _difficulty;
        public Difficulty Difficulty => _difficulty;

        public Maze(string[] data, Difficulty difficulty, Player player)
        {
            _map = null;
            LoadMap(data);


            GetYes();

            // set difficulty
            _difficulty = difficulty;


            // give player and set to the start point.
            _player = player;
            _player.SetRemainingSteps(StepByDifficulty());
            player.Step(_entrance!);
        }

        public Maze(int height, int width)
        {
            _map = new Path[height, width];
            _treasureChambers = new List<Coordinate>();
            _exits = new List<Coordinate>();
            GetYes();
            _player = new Player(_entrance!, StepByDifficulty());
        }
        public Maze(int height, int width, Player player)
        {
            _map = new Path[height, width];
            _treasureChambers = new List<Coordinate>();
            _exits = new List<Coordinate>();
            GetYes();
            _player = player;
            _player.Step(_entrance!);
        }
        public void LoadMap(string[] data)
        {
            _map = new Path[data.Length, data[0].Length];
            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    //TODO rename
                    Path asd = dirtyWordsXAML.Where(z => z.Item == data[y][x]).First();
                    _map[y, x] = new Path(asd.Item, new Coordinate(y, x), asd.West, asd.North, asd.East, asd.South);
                }
            }
        }
        public char GetMapItem(Coordinate coordinate)
        {
            return _map[coordinate.Y, coordinate.X].Item;
        }
        public void MovePlayer(Direction direction)
        {
            Coordinate coordinate = _player.Coordinate;
            switch (direction)
            {
                case Direction.West:
                    if (!_map[_player.Coordinate.Y, _player.Coordinate.X].West) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y, _player.Coordinate.X - 1);
                    break;
                case Direction.North:
                    if (!_map[_player.Coordinate.Y, _player.Coordinate.X].North) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y - 1, _player.Coordinate.X);
                    break;
                case Direction.East:
                    if (!_map[_player.Coordinate.Y, _player.Coordinate.X].East) throw new ImpassablePathException();
                    coordinate = new Coordinate(_player.Coordinate.Y, _player.Coordinate.X + 1);
                    break;
                case Direction.South:
                    if (!_map[_player.Coordinate.Y, _player.Coordinate.X].South) throw new ImpassablePathException();
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
            if (_map[coordinate.Y, coordinate.X].Item == '.')
            {
                throw new ImpassablePathException();
            }
            if (_map[coordinate.Y, coordinate.X].Item == '█')
            {
                foreach (Coordinate item in _treasureChambers)
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

        public void GetYes()
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

        public int GetMapPassableLength()
        {
            int length = _map.Length;
            foreach (var item in _map)
            {
                if (item.Item == '.' || item.Item == '█' || item.Item == ' ')
                {
                    length--;
                }
            }
            return length;
        }

        public int StepByDifficulty()
        {
            return GetMapPassableLength() * (int)_difficulty;
        }
    }
}
