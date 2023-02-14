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
        //The positon of the player
        private Coordinate _position;
        //Player position getter
        public Coordinate Coordinate => _position;

        //Player color
        private ConsoleColor _color;
        //Player color getter
        public ConsoleColor Color => _color;
        
        //Steps made by the player
        private int _steps;
        //Player made steps getter
        public int Steps => _steps;

        //Remaining player steps
        private int _remainingSteps;
        //Remaining player steps getter
        public int RemainingSteps => _remainingSteps;

        //Available treasure chambers
        private int _treasure;
        //Getter of the available treasure chambers
        public int Treasure => _treasure;

        //Decides if the player can move or not
        private bool _canMove;
        //Getter of can move
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

        //Operates the player steps
        public void Step(Coordinate coordinate)
        {
            if (!_canMove) throw new OutOfStepsException();
            _position = coordinate;
            _steps++;
            _remainingSteps--;
            if (_remainingSteps <= 0) _canMove = false;
        }
        //Sets the remaining steps of the player
        public void SetRemainingSteps(int remainingSteps)
        {
            _remainingSteps = remainingSteps;
        }
        //Sets the color of the player
        public void SetColor(ConsoleColor consoleColor)
        {
            _color = consoleColor;
        }
        //If the player has found a treasure, increases the treause value
        public void AddTreasure()
        {
            _treasure++;
        }
    }
}