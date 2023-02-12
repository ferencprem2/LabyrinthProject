using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class ImpassablePathException : Exception { }

    public class OutOfStepsException : Exception { }

    public class GiveupException : Exception { }

    public class EndGameException : Exception { }
}
