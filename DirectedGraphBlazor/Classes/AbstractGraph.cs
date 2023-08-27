using DirectedGraph.Interfaces;

namespace DirectedGraph.Classes
{
    public abstract class AbstractGraph<T, K> : IGraph<T, K>
    {
        protected readonly List<T> VertexSet = new List<T>();
        protected readonly List<PairValueImplementation<T>> EdgeSet = new List<PairValueImplementation<T>>();
        protected readonly Dictionary<PairValueImplementation<T>, K> Weigths = new Dictionary<PairValueImplementation<T>, K>();

        public bool AddVertex(T vertex)
        {
            if (vertex == null) throw new ArgumentNullException();
            if(VertexSet.Contains(vertex)) return false;

            VertexSet.Add(vertex);
            return true;
        }

        public void AddVertex(IEnumerable<T> vertexSet)
        {
            if (vertexSet == null) throw new ArgumentNullException();

            foreach(var v in vertexSet)
            {
				if (v != null && !VertexSet.Contains(v))
					VertexSet.Add(v);
			}
        }

        public bool DeleteVertex(T vertex)
        {
            if (vertex == null) throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex)) return false;

            VertexSet.Remove(vertex);
            return true;

        }

        public void DeleteVertex(IEnumerable<T> vertexSet)
        {
            if (vertexSet == null) throw new ArgumentNullException();
            using (var it = VertexSet.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    if (it.Current != null) //  && VertexSet.Contains(it.Current)
                        VertexSet.Remove(it.Current);
                }
            }
        }
        public abstract bool AddEdge(T v1, T v2, K weigth);

        public abstract K GetWeigth(T v1, T v2);

        public abstract bool DeleteEdge(T v1, T v2);

        public abstract bool AreAdjacent(T v1, T v2);

        public abstract int Degree(T vertex);

        public abstract int OutDegree(T vertex);

        public abstract int InDegree(T vertex);

        public int VerticesNumber()
        {
            return VertexSet.Count;
        }

        public int EdgesNumber()
        {
            return EdgeSet.Count;
        }

        public abstract void AdjacentVertices(List<T> list, T vertex);

        public IEnumerable<T> GetVertexSet()
        {
            return VertexSet;
        }

        public IEnumerable<PairValueImplementation<T>> GetEdgeSet()
        {
            return EdgeSet;
        }
    }
}
