using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Labyrinth;

namespace Maze
{
    public class Player
    {
        private Coordinate _position;
        private Level _level;
        private ConsoleColor _color;

        private bool _canMove;
        private int _steps;
        private int _remainingSteps;


        public Coordinate Coordinate => _position;
        public Level Level => _level;
        public ConsoleColor Color => _color;

        public Player()
        {
            _position = new Coordinate(1, 0);
            _level = Level.Easy;
            _color = ConsoleColor.Green;
        }
        public Player(Coordinate position)
        {
            _position = position;
            _level = Level.Easy;
            _color = ConsoleColor.Green;
        }
        public Player(Coordinate position, Level level)
        {
            _position = position;
            _level = level;
            _color = ConsoleColor.Green;
        }
        public Player(Coordinate position, Level level, ConsoleColor color)
        {
            _position = position;
            _level = level;
            _color = color;
        }

        private void SetRemainingSteps()
        {
            switch (_level)
            {
                case Level.Easy: _remainingSteps = int.MaxValue; break;
                case Level.Medium: _remainingSteps = 100; break;
                case Level.Hard: _remainingSteps = 50; break;
                case Level.GigaChad: _remainingSteps = 16; break;
                default: break;
            }
        }
        public void Step(Coordinate coordinate)
        {
            _position = coordinate;
            _steps++;
            _remainingSteps--;
        }
    }
}