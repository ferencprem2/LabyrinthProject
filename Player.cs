using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class Player
    {
        private Coordinate _position;
        public Coordinate Coordinate => _position;

        private ConsoleColor _color;
        public ConsoleColor Color => _color;

        private int _steps;
        public int Steps => _steps;

        private int _remainingSteps;
        public int RemainingSteps => _remainingSteps;

        private int _treasure;
        public int Treasure => _treasure;

        private bool _canMove;
        public bool CanMove => _canMove;

        public Player(ConsoleColor color)
        {
            _position = new Coordinate(0, 0);
            _color = color;
            _steps = 0;
            _remainingSteps = 0;
            _treasure = 0;
            _canMove = true;
        }
        public Player(Coordinate position, ConsoleColor color, int steps, int treasure)
        {
            _position = position;
            _color = color;
            _steps = steps;
            _remainingSteps = 0;
            _treasure = treasure;
            _canMove = true;
        }

        public void Step(Coordinate coordinate)
        {
            if (!_canMove) throw new OutOfStepsException();
            _position = coordinate;
            _steps++;
            _remainingSteps--;
            if (_remainingSteps <= 0) _canMove = false;
        }
        public void SetRemainingSteps(int remainingSteps)
        {
            _remainingSteps = remainingSteps;
        }
        public void SetColor(ConsoleColor consoleColor)
        {
            _color = consoleColor;
        }
        public void AddTreasure()
        {
            _treasure++;
        }
    }
}