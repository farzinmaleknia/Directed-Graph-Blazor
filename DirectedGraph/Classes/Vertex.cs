namespace DirectedGraph.Classes
{
	public class Vertex
	{
		public Vertex() { }

		public int Id;

		public string Name;

		public double X ;

		public double Y ;

		public string Color;

		public double IconSize;

		public bool IsInGroup;

		public bool IsStore;

		public List<string> Adjacents = new List<string>();

		public List<string> Entries = new List<string>();
	}
}
