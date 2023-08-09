using DirectedGraph.Interfaces;
using System.Diagnostics.Metrics;

namespace DirectedGraph.Classes
{
    public class DirectedGraph<T, K> : AbstractGraph<T, K>
    {
        public override bool AddEdge(T v1, T v2, K weigth)
        {
            if (v1 == null || v2 == null || weigth == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2)) return false;
            IPairValue<T> pair = new PairValueImplementation<T>(v1, v2);
            if(EdgeSet.Contains(pair)) return false;

            EdgeSet.Add(pair);
            Weigths[pair] = weigth;
            return true;
        }

        public override K GetWeigth(T v1, T v2)
        {
            if (v1 == null || v2 == null) throw new ArgumentNullException();

            IPairValue<T> pair = new PairValueImplementation<T>(v1, v2);
            if (!Weigths.ContainsKey(pair)) throw new ArgumentException();

            return Weigths[pair];
        }

        public override bool DeleteEdge(T v1, T v2)
        {
            if (v1 == null || v2 == null) throw new ArgumentNullException();

            IPairValue<T> pair = new PairValueImplementation<T>(v1, v2);
            if (EdgeSet.Contains(pair))
            {
                EdgeSet.Remove(pair);
                Weigths.Remove(pair);
                return true;
            };

            return false;
        }

        public override bool AreAdjacent(T v1, T v2)
        {
            if (v1 == null || v2 == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2)) throw new ArgumentException();

            return EdgeSet.Contains(new PairValueImplementation<T>(v1, v2));
            
        }

        public override int Degree(T vertex)
        {
            if (vertex == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(vertex)) throw new ArgumentException();

            return InDegree(vertex) + OutDegree(vertex);

        }

        public override int OutDegree(T vertex)
        {
            if (vertex == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(vertex)) throw new ArgumentException();

            int counter = 0;
            foreach (var pair in EdgeSet)
                if (pair.GetFirst().Equals(vertex))
                    counter++;
            return counter;
        }

        public override int InDegree(T vertex)
        {
            if (vertex == null) throw new ArgumentNullException();

            if (!VertexSet.Contains(vertex)) throw new ArgumentException();

            int counter = 0;
            foreach (var pair in EdgeSet)
                if (pair.GetSecond().Equals(vertex))
                    counter++;
            return counter;
        }

        public override IEnumerable<T> AdjacentVertices(T vertex)
        {
            foreach (IPairValue<T> p in EdgeSet)
            {
                if (p.GetFirst().Equals(vertex))
                    yield return p.GetSecond();
            }
        }
    }
}
