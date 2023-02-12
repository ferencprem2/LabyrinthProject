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

        public bool CheckCoordinate(Coordinate cordinate)
        {
            return cordinate.Y == _y && cordinate.X == _x;
        }

        public override string ToString()
        {
            return $"X:{_x}\tY:{_y}";
        }
    }
}
