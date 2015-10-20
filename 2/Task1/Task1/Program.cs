using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        private const string inputPath = "in.txt";
        private const string outputPath = "out.txt";
        private static List<Vertex> allVertex = new List<Vertex>();
        private static List<Edge> allEdges = new List<Edge>();
        private static Dictionary<Vertex, Vertex> next = new Dictionary<Vertex, Vertex>();
        private static Dictionary<Vertex, int> name = new Dictionary<Vertex, int>();
        private static Dictionary<int, double> size = new Dictionary<int, double>();


        static void Merge(Vertex a, Vertex b, int nameA, int nameB)
        {
            name[b] = nameA;
            var c = next[b];
            while (name[c] != nameA)
            {
                name[c] = nameA;
                c = next[c];
            }
            size[nameA] += size[nameB];
            c = next[a];
            next[a] = next[b];
            next[b] = c;
        }
        static List<Edge> AlgorithmBoruvka()
        {
            foreach (var vertex in allVertex)
            {
                name.Add(vertex, vertex.Id);
                next.Add(vertex, vertex);
                size.Add(vertex.Id, 1);
            }
            var minOstov = new List<Edge>();
            while (minOstov.Count != allEdges.Count-1)
            {
                var edge = PopEdge();
                var nameA = name[edge.A];
                var nameB = name[edge.B];
                if (nameA == nameB) continue;
                if (size[nameA] > size[nameB])
                    Merge(edge.B, edge.A, nameB, nameA);
                else
                    Merge(edge.A, edge.B, nameA, nameB);
                minOstov.Add(edge);
            }
            return minOstov;
        }

        static Edge PopEdge()
        {
            var vertex = allEdges.First();
            allEdges = allEdges.Skip(1).ToList();
            return vertex;
        }

        static void InitEdges()
        {
            for(int i = 0; i < allVertex.Count; i++)
                for (int j = i+1; j < allVertex.Count; j++)
                    allEdges.Add(new Edge(allVertex[i], allVertex[j]));
            allEdges = allEdges.OrderBy(a => a.Size).ToList();
        }
        static List<Vertex> Input() => File.ReadAllLines(inputPath)
            .Skip(1)
            .Where(line => line.Any())
            .Select(line => line.Split(' ').Select(a=>a.Trim()).Select(int.Parse))
            .Select(pair => new Vertex(pair.First(), pair.Last()))
            .ToList();

        static void Output(List<Edge> minOstov)
        {
            var output = new List<List<int>>(Vertex.Count);
            for(int i=0; i<Vertex.Count; i++) output.Add(new List<int>());
            foreach (var edge in minOstov)
            {
                output[edge.A.Id].Add(edge.B.Id+1);
                output[edge.B.Id].Add(edge.A.Id+1);
            }
            output = output.Select(line => line.OrderBy(a => a).ToList()).ToList();
            using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(stream))
            {
                foreach (var line in output)
                {
                    foreach (var num in line)
                    {
                        sw.Write(num + " ");
                    }
                    sw.WriteLine("0");
                }
                sw.Write(minOstov.Select(edge => edge.Size).Sum());
            }
        }
        static void Main(string[] args)
        {
            allVertex = Input();
            if (!allVertex.Any())
            {

                using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                using (var sw = new StreamWriter(stream))
                {
                    sw.WriteLine();
                    sw.WriteLine("\n\r0");
                }
                return;
            }
            InitEdges();
            var minOstov = AlgorithmBoruvka();
            Output(minOstov);

        }


    }
}
