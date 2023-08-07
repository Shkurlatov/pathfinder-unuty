namespace GraphPathfinder
{
    public interface IPathFindingAlgorithm
    {
        int[] FindShortestPath(int[,] graph);
    }
}