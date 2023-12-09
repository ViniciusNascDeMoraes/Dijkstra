namespace Dijkstra
{
    public class Program
    {
        static void Main()
        {
            var one = new Road("1");
            var two = new Road("2");
            var three = new Road("3");
            var four = new Road("4");
            var five = new Road("5");
            var six = new Road("6");
            var seven = new Road("7");
            var eight = new Road("8");
            var nine = new Road("9");
            var ten = new Road("10");
            var eleven = new Road("11");
            var twelve = new Road("12");

            one.Neighbours[two] = 66;
            one.Neighbours[three] = 19;
            one.LowestCost = 0;

            two.Neighbours[four] = 48;
            two.Neighbours[five] = 74;

            three.Neighbours[four] = 12;
            three.Neighbours[five] = 78;

            four.Neighbours[six] = 38;

            five.Neighbours[seven] = 21;

            six.Neighbours[seven] = 1;
            six.Neighbours[eight] = 100;

            seven.Neighbours[six] = 77;
            seven.Neighbours[nine] = 9;

            eight.Neighbours[ten] = 33;
            eight.Neighbours[eleven] = 83;

            nine.Neighbours[ten] = 36;
            nine.Neighbours[eleven] = 62;

            ten.Neighbours[twelve] = 8;

            eleven.Neighbours[twelve] = 84;

            twelve.IsEnd = true;

            var roads = new List<Road>() {one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve};

            Road actualRoad = new("dummy");

            while (!roads.All(r => r.AlreadyVisited))
            {
                int lowestCost = int.MaxValue;

                foreach (var road in roads)
                {
                    if (road.LowestCost < lowestCost && !road.AlreadyVisited)
                    {
                        actualRoad = road;
                        lowestCost = road.LowestCost;
                    }
                }

                foreach (var neighbours in actualRoad.Neighbours)
                {
                    if (!neighbours.Key.AlreadyVisited)
                    {
                        var costUsingActualRoad = neighbours.Value + actualRoad.LowestCost;

                        if (costUsingActualRoad < neighbours.Key.LowestCost)
                        {
                            neighbours.Key.LowestCost = costUsingActualRoad;
                            neighbours.Key.BestNeighbour = actualRoad;
                        }
                    }
                }

                actualRoad.AlreadyVisited = true;
            }

            var bestWay = new Stack<string>();

            var node = twelve;

            bestWay.Push(node.Name);
            var totalCostRoute = node.LowestCost;

            while (node != null && node.BestNeighbour != null)
            {
                bestWay.Push(node.BestNeighbour.Name);
                node = node.BestNeighbour;
            }

            Console.WriteLine($"Total cost: {totalCostRoute}");

            while (bestWay.Count > 0)
            {
                Console.Write(bestWay.Pop());

                if(bestWay.Count >= 1)
                {
                    Console.Write(" -> ");
                }
            }

            Console.ReadLine();
        }

        public class Road(string name)
        {
            public string Name { get; set; } = name;
            public Road BestNeighbour { get; set; }
            public int LowestCost { get; set; } = int.MaxValue;
            public Dictionary<Road, int> Neighbours { get; set; } = [];
            public bool IsEnd { get; set; } = false;
            public bool AlreadyVisited { get; set; } = false;
        }
    }
}
