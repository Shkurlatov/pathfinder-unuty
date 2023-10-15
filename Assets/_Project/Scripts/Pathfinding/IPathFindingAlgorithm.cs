namespace GraphPathfinder.Pathfinding
{
    public interface IPathFindingAlgorithm
    {
        int[] FindShortestPath(int[,] graph, int source, int verticesCount);
    }
}