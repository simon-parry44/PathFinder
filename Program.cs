using System;
using System.Collections.Generic;

namespace PathFinder
{
    public class Program
    {
        static readonly Coordinate MapSize = new Coordinate { X = 64, Y = 64 };
        static readonly Coordinate StartPosition = new Coordinate { X = 0, Y = 0 };
        static readonly Coordinate EndPosition = new Coordinate { X = MapSize.X - 1, Y = MapSize.Y - 1 };
        static List<Node> _map = null;

        public static void Main(string[] args)
        {
            ReadParameterArguments(args);

            ConsoleKeyInfo input;
            do
            {
                _map = Map.GenerateMap(MapSize);

                var shortestPath = Map.FindShortestPath(_map, StartPosition, EndPosition);

                Map.DrawMap(_map, shortestPath, MapSize);

                Console.WriteLine("Press ESC to quit, any key to repeat");
                input = Console.ReadKey();
                Console.Clear();

            } while (input.Key != ConsoleKey.Escape);

        }
        
        private static void ReadParameterArguments(string[] args)
        {
            if (args.Length == 6)
            {
                MapSize.X = Convert.ToInt32(args[0]);
                MapSize.Y = Convert.ToInt32(args[1]);
                StartPosition.X = Convert.ToInt32(args[2]);
                StartPosition.Y = Convert.ToInt32(args[3]);
                EndPosition.X = Convert.ToInt32(args[4]);
                EndPosition.Y = Convert.ToInt32(args[5]);
            }
        }
    }
}

