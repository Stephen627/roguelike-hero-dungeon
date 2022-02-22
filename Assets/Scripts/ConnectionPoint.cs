using System;
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

    public Direction requiredOpeningDirection;
}
