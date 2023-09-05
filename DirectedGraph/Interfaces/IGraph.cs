using DirectedGraph.Classes;

namespace DirectedGraph.Interfaces
{
    public interface IGraph
    {
        bool AddVertex(Vertex vertex);
        void AddVertex(IEnumerable<Vertex> vertexSet);
        bool DeleteVertex(Vertex vertex);
        void DeleteVertex(IEnumerable<Vertex> vertexSet);

        bool AddEdge(Vertex v1, Vertex v2, string weigth);
        bool DeleteEdge(Vertex v1, Vertex v2);
        bool AreAdjacent(Vertex v1, Vertex v2);

        int Degree(Vertex vertex);
        int OutDegree(Vertex vertex);
        int InDegree(Vertex vertex);
        int VerticesNumber();
        int EdgesNumber();

        void AdjacentVertices(Vertex vertex);
        IEnumerable<Vertex> GetVertexSet();
        IEnumerable<PairValueImplementation<Vertex>> GetEdgeSet();

    }

}
 