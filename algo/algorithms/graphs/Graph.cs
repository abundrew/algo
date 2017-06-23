using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algorithms.graphs
{
    public class Graph: IGraph
    {

        public Graph(IEnumerable<int> vertices, IEnumerable<int[]> edges, bool directed = false, bool weighted = false)
        {

        }

        public Graph(IEnumerable<int[]> edges, bool directed = false, bool weighted = false)
        {

        }

        public IEnumerable<int> Vertices { get {  } }

        public int AdjV(int v, int ix)
        {
            return adj[v][ix];
        }
        public int AdjW(int v, int ix)
        {
            return wt[v][ix];
        }
        public int Degree(int vertex)
        {
            return adj[v].Length;
        }

    }
}
