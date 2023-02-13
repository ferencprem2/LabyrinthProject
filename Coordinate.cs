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
        public Coordinate(string coordinate)
        {
            string[] coordinates = coordinate.Split(',');
            _y = int.Parse(coordinates[0]);
            _x = int.Parse(coordinates[1]);
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
            return $"Y:{_y}\tX:{_x}";
        }
        public string ToFile()
        {
            return $"{_y},{_x}";
        }
    }
}