using DirectedGraph.Interfaces;

namespace DirectedGraph.Classes
{
    public abstract class AbstractGraph : IGraph
    {
        public readonly List<Vertex> VertexSet = new List<Vertex>();
        public readonly List<Vertex> SingleVertexSet = new List<Vertex>();
        public readonly List<List<Vertex>> VertexesGroups = new List<List<Vertex>>();
        public readonly List<PairValueImplementation<Vertex>> EdgeSet = new List<PairValueImplementation<Vertex>>();

        public bool AddVertex(Vertex vertex)
        {
            if (vertex == null) throw new ArgumentNullException();
            if(VertexSet.Contains(vertex)) return false;

            VertexSet.Add(vertex);
            return true;
        }

        public bool AddVertexGroup(List<Vertex> vertexGroup)
        {
            if (vertexGroup == null) throw new ArgumentNullException();
            if(VertexesGroups.Contains(vertexGroup)) return false;

            if(vertexGroup.Count > 0)
                VertexesGroups.Add(vertexGroup);
                return true;
        }

        public void AddVertex(IEnumerable<Vertex> vertexSet)
        {
            if (vertexSet == null) throw new ArgumentNullException();

            foreach(var v in vertexSet)
            {
				if (v != null && !VertexSet.Contains(v))
					VertexSet.Add(v);
			}
        }

        public void AddSingleVertex(List<Vertex> vertexSet)
        {
            if (vertexSet == null) throw new ArgumentNullException();

            foreach (var v in vertexSet)
            {
                if (v != null && !SingleVertexSet.Contains(v))
                    SingleVertexSet.Add(v);
            }
        }

        public void AddVertexGroup(List<List<Vertex>> vertexGroups)
        {
            if (vertexGroups == null) throw new ArgumentNullException();

            foreach(var g in vertexGroups)
            {
				if (g != null && !VertexesGroups.Contains(g) && g.Count > 0)
                    VertexesGroups.Add(g);
			}
        }

        public bool DeleteVertex(Vertex vertex)
        {
            if (vertex == null) throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex)) return false;

            VertexSet.Remove(vertex);
            return true;

        }

        public void DeleteVertex(IEnumerable<Vertex> vertexSet)
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
        public abstract bool AddEdge(Vertex v1, Vertex v2, string weigth);

        public abstract bool DeleteEdge(Vertex v1, Vertex v2);

        public abstract bool AreAdjacent(Vertex v1, Vertex v2);

        public abstract int Degree(Vertex vertex);

        public abstract int OutDegree(Vertex vertex);

        public abstract int InDegree(Vertex vertex);

        public int VerticesNumber()
        {
            return VertexSet.Count;
        }

        public int EdgesNumber()
        {
            return EdgeSet.Count;
        }

        public abstract void AdjacentVertices(Vertex vertex);

        public IEnumerable<Vertex> GetVertexSet()
        {
            return VertexSet;
        }

        public List<List<Vertex>> GetVertexGroups()
        {
            return VertexesGroups;
        }

        public List<Vertex> GetSingleVertexSet()
        {
            return SingleVertexSet;
        }

        public IEnumerable<PairValueImplementation<Vertex>> GetEdgeSet()
        {
            return EdgeSet;
        }
    }
}
