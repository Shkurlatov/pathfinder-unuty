using System;
using System.Collections.Generic;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class Node
    {
        public int Number { get; private set; }
        public Vector2 Position { get; private set; }
        public List<Connection> Connections { get; private set; }
        public NodeState State { get; private set; }

        public event Action<NodeState> OnNodeChanged;

        public Node(int number, Vector2 position)
        {
            Number = number;
            Position = position;
            Connections = new List<Connection>();
            State = NodeState.NotConnected;

            if (number == 0)
            {
                State = NodeState.Connected;
            }
        }

        public void UpdateConnectionState()
        {
            if (State == NodeState.Connected)
            {
                return;
            }

            State = NodeState.Connected;
            OnNodeChanged?.Invoke(State);

            foreach (Connection connection in Connections)
            {
                connection.ConnectedNode.UpdateConnectionState();
            }
        }

        public void Connect(Connection connection)
        {
            Connections.Add(connection);

            if (State == NodeState.Connected)
            {
                connection.ConnectedNode.UpdateConnectionState();
            }
        }

        public void Pick()
        {
            State = NodeState.Picked;
            OnNodeChanged?.Invoke(State);
        }

        public void Skip()
        {
            State = NodeState.Connected;
            OnNodeChanged?.Invoke(State);
        }

        public void Destroy()
        {
            State = NodeState.Destroyed;
            OnNodeChanged?.Invoke(State);
        }
    }

    public enum NodeState
    {
        NotConnected = 0,
        Connected = 1,
        Picked = 2,
        Destroyed = 3,
    }
}