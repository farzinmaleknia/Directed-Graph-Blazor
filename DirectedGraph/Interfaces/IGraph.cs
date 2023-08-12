using DirectedGraph.Classes;

namespace DirectedGraph.Interfaces
{
    public interface IGraph <T, K>
    {
        bool AddVertex(T vertex);
        void AddVertex(IEnumerable<T> vertexSet);
        bool DeleteVertex(T vertex);
        void DeleteVertex(IEnumerable<T> vertexSet);

        bool AddEdge(T v1, T v2, K weigth);
        K GetWeigth(T v1, T v2);
        bool DeleteEdge(T v1, T v2);
        bool AreAdjacent(T v1, T v2);

        int Degree(T vertex);
        int OutDegree(T vertex);
        int InDegree(T vertex);
        int VerticesNumber();
        int EdgesNumber();

        IEnumerable<T> AdjacentVertices(T vertex);
        IEnumerable<T> GetVertexSet();
        IEnumerable<PairValueImplementation<T>> GetEdgeSet();

    }

}
 