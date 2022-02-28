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

    [Serializable]
    public enum DirectionOption
    {
        Required,
        Closed,
        None,
    }

    [Serializable]
    public class DirectionResult
    {
        public readonly List<Direction> requiredDirections;
        public readonly List<Direction> closedDirections;

        public DirectionResult(List<Direction> requiredDirections, List<Direction> closedDirections)
        {
            this.requiredDirections = requiredDirections;
            this.closedDirections = closedDirections;
        }
    }

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

    public DirectionResult GetRequiredOpenings(int roomLayer)
    {
        List<ConnectionPoint.Direction> requiredDirections = new List<ConnectionPoint.Direction>();
        List<ConnectionPoint.Direction> closedDirections = new List<ConnectionPoint.Direction>();

        DirectionOption topResult = this.RoomAtLocation(Vector2.up, roomLayer);
        DirectionOption bottomResult = this.RoomAtLocation(Vector2.down, roomLayer);
        DirectionOption leftResult = this.RoomAtLocation(Vector2.left, roomLayer);
        DirectionOption rightResult = this.RoomAtLocation(Vector2.right, roomLayer);

        if (topResult == DirectionOption.Required)
            requiredDirections.Add(ConnectionPoint.Direction.Top);
        else if (topResult == DirectionOption.Closed)
            closedDirections.Add(ConnectionPoint.Direction.Top);

        if (bottomResult == DirectionOption.Required)
            requiredDirections.Add(ConnectionPoint.Direction.Bottom);
        else if (bottomResult == DirectionOption.Closed)
            closedDirections.Add(ConnectionPoint.Direction.Bottom);
            
        if (leftResult == DirectionOption.Required)
            requiredDirections.Add(ConnectionPoint.Direction.Left);
        else if (leftResult == DirectionOption.Closed)
            closedDirections.Add(ConnectionPoint.Direction.Left);

        if (rightResult == DirectionOption.Required)
            requiredDirections.Add(ConnectionPoint.Direction.Right);
        else if (rightResult == DirectionOption.Closed)
            closedDirections.Add(ConnectionPoint.Direction.Right);

        return new DirectionResult(
            requiredDirections,
            closedDirections
        );
    }

    private DirectionOption RoomAtLocation(Vector2 direction, int roomLayer)
    {
        this.gameObject.SetActive(false);
        RaycastHit2D hit = Physics2D.Raycast(
            this.transform.position,
            direction,
            10
        );
        this.gameObject.SetActive(true);

        if (hit.collider == null)
            return DirectionOption.None;

        if (hit.collider.gameObject.tag == "Door")
            return DirectionOption.Required;
        
        if (hit.collider.gameObject.tag == "Wall")
            return DirectionOption.Closed;

        return DirectionOption.None;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Connection Point")
        {
            ConnectionPoint otherConnectionPoint = other.gameObject.GetComponent<ConnectionPoint>();
            if (!otherConnectionPoint.dontDestory)
            {
                this.dontDestory = true;
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Room")
            Destroy(this.gameObject);
    }
}
