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

    public List<ConnectionPoint.Direction> GetRequiredOpenings(int roomLayer)
    {
        List<ConnectionPoint.Direction> requireDirections = new List<ConnectionPoint.Direction>();

        if (this.RoomAtLocation(Vector2.up, roomLayer))
            requireDirections.Add(ConnectionPoint.Direction.Top);

        if (this.RoomAtLocation(Vector2.down, roomLayer))
            requireDirections.Add(ConnectionPoint.Direction.Bottom);
            
        if (this.RoomAtLocation(Vector2.left, roomLayer))
            requireDirections.Add(ConnectionPoint.Direction.Left);

        if (this.RoomAtLocation(Vector2.right, roomLayer))
            requireDirections.Add(ConnectionPoint.Direction.Right);

        return requireDirections;
    }

    private bool RoomAtLocation(Vector2 direction, int roomLayer)
    {
        this.gameObject.SetActive(false);
        RaycastHit2D hit = Physics2D.Raycast(
            this.transform.position,
            direction,
            10
        );
        this.gameObject.SetActive(true);

        if (hit.collider == null)
            return false;

        return hit.collider.gameObject.tag == "Door";
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
