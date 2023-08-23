namespace DirectedGraph.Classes
{
	public class Vertex
	{
		public Vertex() { }

		public string Name;

		public double X ;

		public double Y ;

		public string Color;

		public double IconSize;

		public bool IsInGroup;

		public List<Vertex> Adjacents = new List<Vertex>();

		public List<Vertex> Entries = new List<Vertex>();
	}
}
