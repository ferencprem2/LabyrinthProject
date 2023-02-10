using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth;

namespace Maze
{
    internal class Maze
    {
        static public List<Path> dirtyWordsXAML = new List<Path>()
        {
            new Path('.', false, false, false, false),
            new Path('█', true, true, true, true),
            new Path('╬', true, true, true, true),
            new Path('═', true, false, true, false),
            new Path('╦', true, false, true, true),
            new Path('╩', true, true, true, false),
            new Path('║', false, true, false, true),
            new Path('╣', true, true, false, true),
            new Path('╠', false, true, true, true),
            new Path('╗', true, false, false, true),
            new Path('╝', true, true, false, false),
            new Path('╚', false, true, true, false),
            new Path('╔', false, false, true, true)
        };

        private Player _player;
        private Path[,] _map;
        public List<Coordinate> exits;
        public List<Coordinate> entrances;
        public List<Coordinate> treasureChambers;

        public int Height => _map.GetLength(0);
        public int Width => _map.GetLength(1);
        public Player Player => _player;
        public Maze()
        {
            _map = new Path[0, 0];
            exits = new List<Coordinate>();
            entrances = new List<Coordinate>();
            treasureChambers = new List<Coordinate>();
            _player = new Player(new Coordinate(1, 0));
        }
        public Maze(int height, int width)
        {
            _map = new Path[height, width];
            exits = new List<Coordinate>();
            entrances = new List<Coordinate>();
            treasureChambers = new List<Coordinate>();
            _player = new Player(new Coordinate(1, 0));
        }
        public Maze(int height, int width, Player player)
        {
            _map = new Path[height, width];
            exits = new List<Coordinate>();
            entrances = new List<Coordinate>();
            treasureChambers = new List<Coordinate>();
        }
        public void LoadMap(List<string> data)
        {
            _map = new Path[data.Count, data[0].Length];
            Console.WriteLine(_map.GetLength(0));
            Console.WriteLine(_map.GetLength(1));
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

        //TODO ellenőrízni
        public List<Coordinate> LocateExits()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (i == 0 && _map[i,j].North == true)
                    {
                        exits.Add(_map[i,j].Coordinate);
                    }
                    else if (i == _map.GetLength(1)-1 && _map[i,j].South == true)
                    {
                        exits.Add(_map[i, j].Coordinate);
                    }
                    else if (j == 0 && _map[i,j].West == true)
                    {
                        exits.Add(_map[i, j].Coordinate);
                    }
                    else if (j == _map.GetLength(1)-1 && _map[i,j].East == true)
                    {
                        exits.Add(_map[i, j].Coordinate);
                    }
                }
            }
            return exits;
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
                    if (_map[_player.Coordinate.Y, _player.Coordinate.X - 1] == _map[coordinate.Y, coordinate.X])
                    {
                        Console.WriteLine("Faszom belerakom anyádba");
                    }
                    else
                    {
                        coordinate = new Coordinate(_player.Coordinate.Y, _player.Coordinate.X - 1);
                    }
                    
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
            if (_map[coordinate.Y, coordinate.X].Item == '.')
            {
                throw new ImpassablePathException();
            }
            _player.Step(coordinate);
        }
        public Path[,] GetMap()
        {
            return _map;
        }



        public List<Coordinate> GetTreasureChambers()
        {
            List<Coordinate> list = new List<Coordinate>();
            foreach (Path item in _map)
            {
                if (item.Item == '█')
                {
                    list.Add(item.Coordinate);
                }
            }
            return list;
        }

        
    }
}