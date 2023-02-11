namespace Maze
{
    public class Coordinate
    {
        private int _y;
        private int _x;

        public int Y => _y;
        public int X => _x;

        public Coordinate(int y, int x)
        {
            _y = y;
            _x = x;
        }

        public bool CheckCoordinate(int y, int x)
        {
            return y == _y && x == _x;
        }
    }
}