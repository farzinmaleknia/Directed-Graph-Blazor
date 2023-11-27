namespace DirectedGraph.Models
{
	public class node
	{
		public string id { get; set; }
		public string title { get; set; }
		//public string name { get; set; }
		public string description { get; set; }
		public DataLabels dataLabels { get; set; }
		public Marker marker { get; set; }
	}
}
