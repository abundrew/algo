using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algorithms.graphs
{
    // ----- Graph -------------------------------------------------------------
    //
    // Graph(IEnumerable<int> vertices, IEnumerable<Edge> edges, bool directed = false, bool weighted = false)
    // Graph(int N, IEnumerable<Edge> edges, bool directed = false, bool weighted = false)
    // int VertexCount { get { return vertices.Length; } }
    // int Vertex(int index) { return vertices[index]; }
    // int Degree(int vertex) { return adjacentVertex[vertex].Length; }
    // int AdjacentVertex(int vertex, int index) { return adjacentVertex[vertex][index]; }
    // int AdjacentWeight(int vertex, int index) { return adjacentWeight[vertex][index]; }
    // string ToString()
    // -------------------------------------------------------------------------
    public class Edge
    {
        public int V { get; set; }
        public int U { get; set; }
        public int W { get; set; }
    }

    public interface IGraph
    {
        int VertexCount { get; }
        int Vertex(int index);
        int Degree(int vertex);
        int AdjacentVertex(int vertex, int index);
    }

    public interface IWeightedGraph
    {
        int AdjacentWeight(int vertex, int index);
    }

    public class Graph: IWeightedGraph
    {
        int[] vertices = null;
        int[][] adjacentVertex = null;
        int[][] adjacentWeight = null;

        public Graph(IEnumerable<int> vertices, IEnumerable<Edge> edges, bool directed = false, bool weighted = false)
        {
            this.vertices = vertices.ToArray();

            int vmax = this.vertices[0];
            for (int i = 1; i < this.vertices.Length; i++)
                if (vmax < this.vertices[i]) vmax = this.vertices[i];
            adjacentVertex = new int[vmax + 1][];
            if (weighted) adjacentWeight = new int[vmax + 1][];

            int[] deg = new int[vmax + 1];
            foreach (Edge e in edges)
            {
                deg[e.V]++;
                if (!directed) deg[e.U]++;
            }
            for (int i = 0; i < vmax + 1; i++)
            {
                adjacentVertex[i] = new int[deg[i]];
                if (weighted) adjacentWeight[i] = new int[deg[i]];
            }

            for (int i = 0; i < vmax + 1; i++) deg[i] = 0;
            foreach (Edge e in edges)
            {
                adjacentVertex[e.V][deg[e.V]] = e.U;
                if (weighted) adjacentWeight[e.V][deg[e.V]] = e.W;
                deg[e.V]++;
                if (!directed)
                {
                    adjacentVertex[e.U][deg[e.U]] = e.V;
                    if (weighted) adjacentWeight[e.U][deg[e.U]] = e.W;
                    deg[e.U]++;
                }
            }
        }

        public Graph(int N, IEnumerable<Edge> edges, bool directed = false, bool weighted = false) : this(Enumerable.Range(0, N), edges, directed, weighted) {  }

        public int VertexCount { get { return vertices.Length; } }
        public int Vertex(int index) { return vertices[index]; }
        public int Degree(int vertex) { return adjacentVertex[vertex].Length; }
        public int AdjacentVertex(int vertex, int index) { return adjacentVertex[vertex][index]; }
        public int AdjacentWeight(int vertex, int index) { return adjacentWeight[vertex][index]; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int vertex in vertices)
                sb.AppendFormat("{0}:{1}", vertex, string.Join(",",
                    Enumerable.Range(0, adjacentVertex[vertex].Length).Select(p =>
                        adjacentWeight != null ? string.Format("{0}[{1}]", adjacentVertex[vertex][p], adjacentWeight[vertex][p]) : string.Format("{0}", adjacentVertex[vertex][p])
                    ).ToArray()
                )).AppendLine();
            return sb.ToString();
        }
    }
    // -------------------------------------------------------------------------
}
