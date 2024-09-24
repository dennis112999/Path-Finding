using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector2 Position { get; set; }
    public float Cost { get; set; }

    public Node(Vector2 position, float cost)
    {
        Position = position;
        Cost = cost;
    }

    public int CompareTo(Node other)
    {
        return Cost.CompareTo(other.Cost);
    }
}
