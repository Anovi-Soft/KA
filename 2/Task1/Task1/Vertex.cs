using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Vertex
    {
        public Vertex (int x, int y)
        {
            X = x;
            Y = y;
            Id = ++lastId;
        }
        public int X { get; }
        public int Y { get; }
        public int Id { get; }
        private static int lastId=-1;
        public static int Count => lastId + 1;
        
        public override string ToString() => $"[{X}; {Y}]";
    }
}
