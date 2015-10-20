using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Edge
    {
        public Edge(Vertex a, Vertex b)
        {
            A = a;
            B = b;
        }

        public Vertex A { get; }
        public Vertex B { get; }
        private int size=-1;
        public int Size
        {
            get
            {
                if (size == -1d)
                    size = SizeOf(A, B);
                return size;
            }
        }

        public static int SizeOf(Vertex a, Vertex b) =>
            Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

        public override string ToString() => $"{A}, {B}";
    }
}
