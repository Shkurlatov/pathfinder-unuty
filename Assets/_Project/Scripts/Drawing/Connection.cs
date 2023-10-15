namespace GraphPathfinder.Drawing
{
    public class Connection
    {
        public readonly Node ConnectedNode;
        public readonly Edge Edge;

        public Connection(Node connectedNode, Edge edge)
        {
            ConnectedNode = connectedNode;
            Edge = edge;
        }
    }
}