using System;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class Edge
    {
        public Vector2 StartPosition { get; private set; }
        public Vector2 EndPosition { get; private set; }
        public int RelativeDistance { get; private set; }
        public EdgeState State { get; private set; }

        public event Action<EdgeState> OnEdgeChanged;

        public Edge(Node startNode)
        {
            StartPosition = startNode.Position;
            State = EdgeState.NotComplete;
        }

        public void SetEndPosition(Vector2 endPosition)
        {
            EndPosition = endPosition;
            OnEdgeChanged?.Invoke(State);
        }

        public void Complete()
        {
            RelativeDistance = (int)(Vector2.Distance(StartPosition, EndPosition) * 10);

            State = EdgeState.Complete;
            OnEdgeChanged?.Invoke(State);
        }

        public void LightUp()
        {
            State = EdgeState.Highlighted;
            OnEdgeChanged?.Invoke(State);
        }

        public void Dim()
        {
            State = EdgeState.Dimmed;
            OnEdgeChanged?.Invoke(State);
        }

        public void Destroy()
        {
            State = EdgeState.Destroyed;
            OnEdgeChanged?.Invoke(State);
        }
    }

    public enum EdgeState
    {
        NotComplete = 0,
        Complete = 1,
        Highlighted = 2,
        Dimmed = 3,
        Destroyed = 4,
    }
}