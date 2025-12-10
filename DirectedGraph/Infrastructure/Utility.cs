using System;

namespace DirectedGraph.Infrastructure
{
    public class Utility
    {
        private static Utility _instance;
        public static Utility GetInstance()
        {
            if (_instance == null)
                _instance = new Utility();
            return _instance;
        }
        public Utility()
        { 

        }


        // ********************
        #region Constants

        #endregion

        // ********************
        #region Methods

        public static async Task<Classes.DirectedGraph> GroupingNodes(Classes.DirectedGraph tempGraph, bool isFakeData = true)
        {

			List<List<Classes.Vertex>> vertexesGroups = new List<List<Classes.Vertex>>();

			var allVertexes = new List<Classes.Vertex>();

            foreach (var thisItem in tempGraph.GetVertexSet())
            {
                allVertexes.Add(thisItem);
            }

            int storesEntriesCount = 0;

            foreach (var iStore in tempGraph.GetStoresSet())
            {
                storesEntriesCount = storesEntriesCount + iStore.Entries.Count;
            }

            int range = storesEntriesCount / tempGraph.GetStoresSet().Count;

            if(isFakeData) 
            {

                foreach (var thisItem in tempGraph.GetVertexSet())
                {
                    if (thisItem.Entries.Count > ((range / 4) * 2))
                    {
                        vertexesGroups.Add(new List<Classes.Vertex>() { thisItem });
                        thisItem.IsInGroup = true;

                        foreach (var i in thisItem.Adjacents)
                        {
                            var j = allVertexes.FirstOrDefault(v => v.Name == i);
                            if (!j.IsInGroup && j.Entries.Count < ((range / 4) * 2))
                            {
                                vertexesGroups.FirstOrDefault(l => l.Contains(thisItem)).Add(j);
                                j.IsInGroup = true;
                            }

                        }
                    }
                }

                foreach (var thisItem in allVertexes)
                {
                    if (thisItem.Entries.Count > (range / 4) && thisItem.Entries.Count < (range / 4) * 2 && !thisItem.IsInGroup && thisItem.IsStore)
                    {
                        vertexesGroups.Add(new List<Classes.Vertex>() { thisItem });
                        thisItem.IsInGroup = true;
                        Console.WriteLine("not red header added");

                        foreach (var i in thisItem.Adjacents)
                        {
                            var j = allVertexes.FirstOrDefault(v => v.Name == i);
                            if (!j.IsInGroup)
                            {
                                vertexesGroups.FirstOrDefault(l => l.Contains(thisItem)).Add(j);
                                j.IsInGroup = true;
                            }

                        }
                    }
                }
            }
            else
            {

                foreach (var thisItem in tempGraph.GetVertexSet())
                {
                    if (thisItem.IsStore)
                    {
                        vertexesGroups.Add(new List<Classes.Vertex>() { thisItem });
                        thisItem.IsInGroup = true;
                        foreach (var i in thisItem.Adjacents)
                        {

                            var j = allVertexes.FirstOrDefault(v => v.Name == i);
                            if (!j.IsInGroup)// && j.Entries.Count < ((range / 4) * 2))
                            {
                                vertexesGroups.FirstOrDefault(l => l.Contains(thisItem)).Add(j);
                                j.IsInGroup = true;
                            }

                        }
                    }
                }
            }



            vertexesGroups = vertexesGroups.FindAll(g => g.Count > 0);

            var count = 1;
            var vertexCount = 1;



            int isAlone = 0;


            for (int i = 0; i < 2; i++)
            {
                int isNotin = 0;
                int isNotin2 = 0;
                foreach (var thisItem in allVertexes)
                {
                    if (thisItem.Entries.Count < ((range / 4) * 2) || !isFakeData && !thisItem.IsInGroup)
                    {
                        if (!thisItem.IsInGroup)
                        {
                            isNotin++;
                        }
                        var itemList = vertexesGroups.FirstOrDefault(l => l.Contains(thisItem));
                        var AdjacentsList = new List<Classes.Vertex>();
                        foreach (var v in thisItem.Adjacents)
                        {
                            var tempV = allVertexes.FirstOrDefault(vertex => vertex.Name == v);
                            AdjacentsList.Add(tempV);
                        }

                        if (itemList == null)
                        {
                            itemList = new List<Classes.Vertex>();
                        }

                        int itemListSimilarity = AdjacentsList.Except(itemList).Count();

                        foreach (var list in vertexesGroups)
                        {
                            if (list != itemList)
                            {
                                var newListSimilarity = AdjacentsList.Except(list).Count();
                                if (itemListSimilarity > newListSimilarity)
                                {
                                    itemList.Remove(thisItem);
                                    list.Add(thisItem);
                                    itemList = list;
                                    itemListSimilarity = AdjacentsList.Except(list).Count();
                                    thisItem.IsInGroup = true;
                                }
                                else if (itemListSimilarity == newListSimilarity && list.Count < itemList.Count)
                                {
                                    itemList.Remove(thisItem);
                                    list.Add(thisItem);
                                    itemList = list;
                                    itemListSimilarity = AdjacentsList.Except(list).Count();
                                    thisItem.IsInGroup = true;
                                }
                            }
                        }

                        if (!thisItem.IsInGroup)
                        {
                            isNotin2++;

                            if (thisItem.Adjacents.Count == 0)
                            {
                                isAlone++;

                            }
                        }
                    }

                }

                Console.WriteLine($"isNotin - {isNotin}");
                Console.WriteLine($"isNotin2 - {isNotin2}");
            }

            Console.WriteLine($"isAlone - {isAlone}");
            int isNotInGroup = 0;

            Console.WriteLine("finishing ...");
            foreach (var list in vertexesGroups)
            {
                foreach (var v in list)
                {
                    allVertexes.Remove(v);
                }
            }


            var maxGroupMember = 1;

            foreach (var list in vertexesGroups)
            {
                if (maxGroupMember < list.Count)
                {
                    maxGroupMember = list.Count;
                }

            }

            var groupsNeededCount = allVertexes.Count / maxGroupMember + 1;

            for (int i = 0; i < groupsNeededCount; i++)
            {
                if (allVertexes.Count > 0)
                {
                    var tempItem = allVertexes.First();
                    vertexesGroups.Add(new List<Classes.Vertex>() { tempItem });
                    tempItem.IsInGroup = true;
                    allVertexes.Remove(tempItem);
                    var tempList = vertexesGroups.FirstOrDefault(l => l.Contains(tempItem));
                    while (tempList.Count <= maxGroupMember && allVertexes.Count > 0)
                    {
                        tempItem = allVertexes.First();
                        tempList.Add(tempItem);
                        tempItem.IsInGroup = true;
                        allVertexes.Remove(tempItem);
                    }
                }

            }

            foreach (var thisItem in tempGraph.GetVertexSet())
            {
                if (!thisItem.IsInGroup)
                {
                    isNotInGroup++;
                }
            }

            Console.WriteLine($"vertexesGroups.Count - {vertexesGroups.Count}");
            Console.WriteLine($"isNotInGroup - {isNotInGroup}");

            for (int i = 0; i < 2; i++)
            {
                foreach (var thisItem in tempGraph.GetVertexSet())
                {

                    if (thisItem.Entries.Count < ((range / 4) * 2))
                    {

                        var itemList = vertexesGroups.FirstOrDefault(l => l.Contains(thisItem));


                        if (itemList == null)
                        {
                            itemList = new List<Classes.Vertex>();
                        }

                        var AdjacentsList = new List<Classes.Vertex>();
                        foreach (var v in thisItem.Adjacents)
                        {
                            var tempV = tempGraph.GetVertexSet().FirstOrDefault(vertex => vertex.Name == v);
                            AdjacentsList.Add(tempV);
                        }

                        int itemListSimilarity = AdjacentsList.Except(itemList).Count();


                        foreach (var list in vertexesGroups)
                        {
                            if (list != itemList)
                            {
                                var newListSimilarity = AdjacentsList.Except(list).Count();

                                if (itemListSimilarity > newListSimilarity)
                                {
                                    Console.WriteLine("moved");
                                    itemList.Remove(thisItem);
                                    list.Add(thisItem);
                                    itemList = list;
                                    itemListSimilarity = AdjacentsList.Except(list).Count();
                                    thisItem.IsInGroup = true;
                                }
                                else if (itemListSimilarity == newListSimilarity && list.Count < itemList.Count)
                                {
                                    itemList.Remove(thisItem);
                                    list.Add(thisItem);
                                    itemList = list;
                                    itemListSimilarity = AdjacentsList.Except(list).Count();
                                    thisItem.IsInGroup = true;
                                }
                            }
                        }
                    }

                }
            }

            foreach (var g in vertexesGroups)
            {
                g.OrderBy(v => v.IconSize);
            }

            tempGraph.AddVertexGroup(vertexesGroups);
            tempGraph.AddSingleVertex(allVertexes);

            return tempGraph;
        }

		public static async Task<Classes.DirectedGraph> GetPositions(Classes.DirectedGraph tempGraph, double graphWidth, double graphHeight, bool isGraphLoaded)
		{
			Random number = new Random();

			var maxW = graphWidth;
			var maxH = graphHeight;

			var limitedWidth = maxW - 250;
			var limitedHeight = maxH - 250;

			var columns = Math.Floor(limitedWidth / 200);
			var rows = Math.Floor(limitedHeight / 200);

			double distance = 0;
			int counterX = 0;
			int counterY = 0;

			var headPositions = new List<List<double>>();

			foreach (var list in tempGraph.GetVertexGroups())
			{
				var tempList = list.OrderByDescending(l => l.Entries.Count).ToList();
				var center = tempList.First();
				//var center = tempList.FirstOrDefault(v => v.IsStore);

				foreach (var vertex in tempList)
				{
                    Console.WriteLine(vertex.Entries.Count);
					if (vertex == center)
					{
						vertex.X = (number.NextDouble() * ((limitedWidth - 0) + 0) + 125);
						vertex.Y = (number.NextDouble() * ((limitedHeight - 0) + 0) + 125);


						for (int i = 0; i < 5; i++)
						{
							foreach (var thisItem in headPositions)
							{
								distance = Math.Sqrt(((thisItem[0] - vertex.X) * (thisItem[0] - vertex.X)) + ((thisItem[1] - vertex.Y) * (thisItem[1] - vertex.Y)));
								while (distance < 150)
								{
									vertex.X = (number.NextDouble() * ((limitedWidth - 0) + 0) + 125);
									vertex.Y = (number.NextDouble() * ((limitedHeight - 0) + 0) + 125);
									distance = Math.Sqrt(((thisItem[0] - vertex.X) * (thisItem[0] - vertex.X)) + ((thisItem[1] - vertex.Y) * (thisItem[1] - vertex.Y)));

								}
							}
						}

						if (isGraphLoaded)
						{
							var thisItem = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == vertex.Name);
							thisItem.X = vertex.X;
							thisItem.Y = vertex.Y;
						}

						headPositions.Add(new List<double>() { vertex.X, vertex.Y });

						if (counterX == columns - 1 && counterY == rows - 1)
						{
							counterX = 0;
							counterY = 0;
						}
						else
						{
							if (counterX == columns - 1)
							{
								counterX = 0;
								counterY++;

								if (counterY == rows)
								{
									counterY = 0;
								}

							}
							else
							{
								counterX++;
							}


						}
					}
					else
					{
						if (vertex.Entries.Count > 2 && vertex.Entries.Count < 5)
						//if (vertex.Entries.Count > 2 && vertex.Entries.Count < 5)
						{
							vertex.X = number.NextDouble() * ((center.X + 40) - (center.X - 40)) + (center.X - 40);
							vertex.Y = number.NextDouble() * ((center.Y + 40) - (center.Y - 40)) + (center.Y - 40);

							while (distance < 20)
							{
								vertex.X = vertex.X > center.X ? vertex.X + 10 : vertex.X - 10;
								vertex.Y = vertex.Y > center.Y ? vertex.Y + 10 : vertex.Y - 10;
								distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							}
							while (distance > 40)
							{
								vertex.X = vertex.X < center.X ? vertex.X + 10 : vertex.X - 10;
								vertex.Y = vertex.Y < center.Y ? vertex.Y + 10 : vertex.Y - 10;
								distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							}

							if (isGraphLoaded)
							{
								var thisItem = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == vertex.Name);
								thisItem.X = vertex.X;
								thisItem.Y = vertex.Y;
							}
						}
                        //else if (vertex.Entries.Count == 2)
                        else if (vertex.Entries.Count > 2 && vertex.Entries.Count <= 5)
                        {
							vertex.X = number.NextDouble() * ((center.X + 60) - (center.X - 60)) + (center.X - 60);
							vertex.Y = number.NextDouble() * ((center.Y + 60) - (center.Y - 60)) + (center.Y - 60);



							distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							while (distance < 40)
							{
								vertex.X = vertex.X > center.X ? vertex.X + 10 : vertex.X - 10;
								vertex.Y = vertex.Y > center.Y ? vertex.Y + 10 : vertex.Y - 10;
								distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							}
							while (distance > 60)
							{
								vertex.X = vertex.X < center.X ? vertex.X + 10 : vertex.X - 10;
								vertex.Y = vertex.Y < center.Y ? vertex.Y + 10 : vertex.Y - 10;
								distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							}


							if (isGraphLoaded)
							{
								var thisItem = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == vertex.Name);
								thisItem.X = vertex.X;
								thisItem.Y = vertex.Y;
							}
						}
						else
						{
							vertex.X = number.NextDouble() * ((center.X + 80) - (center.X - 80)) + (center.X - 80);
							vertex.Y = number.NextDouble() * ((center.Y + 80) - (center.Y - 80)) + (center.Y - 80);


							distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							while (distance < 60)
							{
								vertex.X = vertex.X > center.X ? vertex.X + 10 : vertex.X - 10;
								vertex.Y = vertex.Y > center.Y ? vertex.Y + 10 : vertex.Y - 10;
								distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							}
							while (distance > 80)
							{
								vertex.X = vertex.X < center.X ? vertex.X + 10 : vertex.X - 10;
								vertex.Y = vertex.Y < center.Y ? vertex.Y + 10 : vertex.Y - 10;
								distance = Math.Sqrt(((center.X - vertex.X) * (center.X - vertex.X)) + ((center.Y - vertex.Y) * (center.Y - vertex.Y)));
							}


							if (isGraphLoaded)
							{
								var thisItem = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == vertex.Name);
								thisItem.X = vertex.X;
								thisItem.Y = vertex.Y;
							}
						}
					}
				}
			}

            return tempGraph;
		}

		public static async Task<Classes.DirectedGraph> Connect(Classes.DirectedGraph tempGraph, bool isReposition = false)
        {
   //         if(isReposition)
   //         {
			//	foreach (var edge in tempGraph.GetEdgeSet())
			//	{
			//		var start = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == edge.Start.Name);
			//		var end = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == edge.End.Name);
			//		if (start != null && end != null)
			//		{
			//			edge.T1 = start;
			//			edge.T2 = end;
			//		}
			//	}
			//}

            foreach (var edge in tempGraph.GetEdgeSet())
            {

                if (isReposition)
                {
                    var start = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == edge.Start.Name);
                    var end = tempGraph.GetVertexSet().FirstOrDefault(v => v.Name == edge.End.Name);
                    if (start != null && end != null)
                    {
                        edge.T1 = start;
                        edge.T2 = end;
                    }
                }

                var x1 = (edge.End.X + (edge.End.IconSize / 2));
                var y1 = (edge.End.Y + (edge.End.IconSize / 2));
                var x2 = (edge.Start.X + (edge.Start.IconSize / 2));
                var y2 = (edge.Start.Y + (edge.Start.IconSize / 2));
                double thickness = 0.08;

                var length = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));

                var cx = ((x1 + x2) / 2) - (length / 2);
                var cy = ((y1 + y2) / 2) - (thickness / 2);

                var angle = Math.Atan2((y1 - y2), (x1 - x2)) * (180 / Math.PI);

                edge.Length = length;
                edge.Cx = cx;
                edge.Cy = cy;
                edge.Angle = angle;

            }


            return tempGraph;
        }


        #endregion
    }
}
