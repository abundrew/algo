using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algorithms.graphs
{
    public interface IGraph
    {
        IEnumerable<int> Vertices { get; }
        int Degree(int vertex);
        int AdjacentVertex(int vertex, int index);
        int AdjacentWeight(int vertex, int index);
    }
}
