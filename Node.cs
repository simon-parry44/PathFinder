namespace PathFinder
{
    public class Node : Coordinate
    {
        public bool IsTerrain;
        public int TentativeDistanceValue;
        public int DistanceValue;
        public Node Parent;

        public Node(Coordinate position, bool isTerrain)
        {
            X = position.X;
            Y = position.Y;
            IsTerrain = isTerrain;
            TentativeDistanceValue = int.MaxValue;
            DistanceValue = int.MaxValue;
            Parent = null;
        }
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}



