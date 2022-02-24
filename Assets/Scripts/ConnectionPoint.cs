using System;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    [Serializable]
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    public List<Direction> requiredOpeningDirections;
    [HideInInspector]
    public bool dontDestory = false;

    public Direction FindOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Top:
                return Direction.Bottom;
            case Direction.Bottom:
                return Direction.Top;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Top; // This case can't happen
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Connection Point")
        {
            ConnectionPoint otherConnectionPoint = other.gameObject.GetComponent<ConnectionPoint>();
            if (!otherConnectionPoint.dontDestory)
            {
                this.dontDestory = true;
                this.requiredOpeningDirections.AddRange(otherConnectionPoint.requiredOpeningDirections);
                Destroy(other.gameObject);
            }
        }
    }
}
