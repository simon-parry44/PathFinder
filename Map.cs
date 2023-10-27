using System;
using System.Collections.Generic;

namespace PathFinder
{
    public class Map    
    {
        /// <summary>
        /// Create a random map from a given size
        /// </summary>
        /// <param name="mapSize"></param>
        /// <returns></returns>
        public static List<Node> GenerateMap(Coordinate mapSize)
        {
            var map = new List<Node>();
            var random = new Random();
            for (var y = 0; y < mapSize.Y; y++)
            {
                for (var x = 0; x < mapSize.X; x++)
                {
                    var isTerrain = random.NextDouble() < 0.1; // create a random terrain, increase value for more terrain
                    map.Add(new Node(new Coordinate { X = x, Y = y }, isTerrain));
                }
            }

            return map;
        }

        public static List<Node> FindShortestPath(List<Node> map, Coordinate startPosition, Coordinate endPosition)
        {
            // find index of the start and end points
            int startIndex = map.FindIndex(a => a.X == startPosition.X && a.Y == startPosition.Y);
            int endIndex = map.FindIndex(a => a.X == endPosition.X && a.Y == endPosition.Y);

            map[startIndex].IsTerrain = false; // Leave the start and end free of mountains
            map[endIndex].IsTerrain = false;

            var start = map[startIndex]; // start top left
            var end = map[endIndex]; // finish bottom right

            var shortestPath = AStarAlgorithm.AStar(map, start, end);
            return shortestPath;
        }

        /// <summary>
        /// Draw map with the route highlighted in Red
        /// </summary>
        /// <param name="map"></param>
        /// <param name="path"></param>
        /// <param name="mapSize"></param>
        public static void DrawMap(List<Node> map, List<Node> path, Coordinate mapSize)
        {
            foreach (var node in map)
            {
                var response = path.Find(r => r.X == node.X && r.Y == node.Y);
                Console.ForegroundColor = response != null ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write(node.IsTerrain ? "X" : ".");
                if (node.X >= mapSize.X - 1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }
        }
    }
}
