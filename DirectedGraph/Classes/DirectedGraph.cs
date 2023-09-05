using DirectedGraph.Interfaces;
using System.Diagnostics.Metrics;

namespace DirectedGraph.Classes
{
    public class DirectedGraph : AbstractGraph
    {
        public override bool AddEdge(Vertex v1, Vertex v2, string weigth)
        {
            if (v1 == null || v2 == null || weigth == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2)) return false;
            PairValueImplementation<Vertex> pair = new PairValueImplementation<Vertex>(v1, v2);
            if (EdgeSet.Contains(pair)) return false;

            EdgeSet.Add(pair);
            return true;
        }



        public override bool DeleteEdge(Vertex v1, Vertex v2)
        {
            if (v1 == null || v2 == null) throw new ArgumentNullException();

            PairValueImplementation<Vertex> pair = new PairValueImplementation<Vertex>(v1, v2);
            if (EdgeSet.Contains(pair))
            {
                EdgeSet.Remove(pair);
                return true;
            };

            return false;
        }

        public override bool AreAdjacent(Vertex v1, Vertex v2)
        {
            if (v1 == null || v2 == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2)) throw new ArgumentException();

            return EdgeSet.Contains(new PairValueImplementation<Vertex>(v1, v2));

        }

        public override int Degree(Vertex vertex)
        {
            if (vertex == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(vertex)) throw new ArgumentException();

            return InDegree(vertex) + OutDegree(vertex);

        }

        public override int OutDegree(Vertex vertex)
        {
            if (vertex == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(vertex)) throw new ArgumentException();

            int counter = 0;
            foreach (var pair in EdgeSet)
                if (pair.Start.Equals(vertex))
                    counter++;
            return counter;
        }

        public override int InDegree(Vertex vertex)
        {
            if (vertex == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(vertex)) throw new ArgumentException();

            int counter = 0;
            foreach (var pair in EdgeSet)
                if (pair.End.Equals(vertex))
                    counter++;
            return counter;
        }

        public override void AdjacentVertices(Vertex vertex)
        {
            foreach (PairValueImplementation<Vertex> p in EdgeSet)
            {
                if (p.End.Equals(vertex))
                {
                    vertex.Adjacents.Add(p.Start.Name);

                }
                if (p.Start.Equals(vertex))
                {
                    vertex.Adjacents.Add(p.End.Name);

                }
            }
        }

        public void EntriesVertices(Vertex vertex)
        {
            foreach (PairValueImplementation<Vertex> p in EdgeSet)
            {
                if (p.End.Equals(vertex))
                {
                    vertex.Entries.Add(p.Start.Name);

                }
            }
        }
    }
}