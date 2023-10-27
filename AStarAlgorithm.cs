using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    /// <summary>
    /// A* Algorithm https://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    public class AStarAlgorithm
    {
        private static readonly int[][] Directions =  {
            new [] { 0, -1 }, // Up
            new [] { 1, 0 }, // Right
            new [] { -1, 0 }, // Left
            new [] { 0, 1 }, // Down
        };

        public static List<Node> AStar(List<Node> map, Node start, Node end)
        {
            var openSet = new List<Node> { start };

            start.TentativeDistanceValue = 0;
            start.DistanceValue = ManhattanDistance(start, end);
            var current = new Node(new Coordinate(), false);
            while (openSet.Count > 0)
            {
                openSet.Sort((a, b) => a.TentativeDistanceValue + a.DistanceValue - (b.TentativeDistanceValue + b.DistanceValue));
                current = openSet[0];
                openSet.RemoveAt(0);

                if (current == end)
                {
                    return ReturnShortestPath(current);
                }

                var neighbours = FindAvailableNeighbours(map, current).ToList();

                ClosestNode(end, neighbours, current, openSet);
            }

            return ReturnShortestPath(current);
        }

        /// <summary>
        /// Loop through the available neighbouring nodes and find the closest node to the end
        /// </summary>
        /// <param name="end"></param>
        /// <param name="neighbours"></param>
        /// <param name="current"></param>
        /// <param name="openSet"></param>
        private static void ClosestNode(Node end, List<Node> neighbours, Node current, List<Node> openSet)
        {
            neighbours.ForEach(x =>
            {
                var tentativeG = current.TentativeDistanceValue+1;
                if (tentativeG < x.TentativeDistanceValue)
                {
                    x.Parent = current;
                    x.TentativeDistanceValue = tentativeG;
                    x.DistanceValue = ManhattanDistance(x, end);
                    if (!openSet.Contains(x))
                    {
                        openSet.Add(x);
                    }
                }
            });
        }

        private static List<Node> ReturnShortestPath(Node current)
        {
            var path = new List<Node>();
            var node = current;
            while (node != null)
            {
                path.Insert(0, node);
                node = node.Parent;
            }

            return path;
        }

        /// <summary>
        /// Returns a list of available neighbouring nodes from the current position
        /// </summary>
        /// <param name="map"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        private static IEnumerable<Node> FindAvailableNeighbours(List<Node> map, Node current)
        {
            var neighbours = (from direction in Directions let dx = direction[0] let dy = direction[1] let x = current.X + dx let y = current.Y + dy
                select map.Find(n => n.X == x && n.Y == y) 
                into neighbour
                where neighbour != null select neighbour).ToList();

            neighbours = neighbours.FindAll(n => !n.IsTerrain); // remove any terrain from neighbouring squares, we cannot travel through those

            return neighbours;
        }

        /// <summary>
        /// Calculate the distance between two points, https://en.wikipedia.org/wiki/Taxicab_geometry
        /// </summary>
        /// <param name="currentPosition">The current position on map</param>
        /// <param name="endPosition">The end destination</param>
        /// <returns></returns>
        public static int ManhattanDistance(Node currentPosition, Node endPosition)
        {
            return Math.Abs(currentPosition.X - endPosition.X) + Math.Abs(currentPosition.Y - endPosition.Y);
        }

        public static int EuclideanDistance(Node currentPosition, Node endPosition)
        {
            int square = (currentPosition.X - endPosition.X) * (currentPosition.X - endPosition.X) + (currentPosition.Y - endPosition.Y) * (currentPosition.Y - endPosition.Y);
            return square;
        }

    }
}
