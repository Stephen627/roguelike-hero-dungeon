using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
     public GameObject entryRoom;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject topTerminatingRoom;
    public GameObject bottomTerminatingRoom;
    public GameObject leftTerminatingRoom;
    public GameObject rightTerminatingRoom;
    public int maxDepth = 5;
    public LayerMask roomLayer;
    private List<GameObject> rooms;

    public void Spawn()
    {
        this.rooms = new List<GameObject>();
        GameObject instance = Instantiate(this.entryRoom, new Vector3(0, 0, 0), Quaternion.identity);

        this.rooms.Add(instance);
        this.SpawnConnectingRooms(instance);
    }

    private void SpawnConnectingRooms(GameObject room, int depth = 1)
    {
        if (depth > this.maxDepth)
        {
            // Spawn terminating room
            return;
        }

        ConnectionPoint[] connectionPoints = room.GetComponentsInChildren<ConnectionPoint>();

        for (int i = 0; i < connectionPoints.Length; i++)
        {
            ConnectionPoint connectionPoint = connectionPoints[i];

            if (!connectionPoint.enabled)
                continue;

            Transform transform = connectionPoint.gameObject.transform;
            GameObject spawnRoom = this.FindAppropriateRoom(connectionPoint);

            if (spawnRoom == null)
                continue;
            
            GameObject instance = Instantiate(
                spawnRoom,
                new Vector3(transform.position.x, transform.position.y, 0),
                Quaternion.identity
            );
            this.rooms.Add(instance);
            //Destroy(connectionPoint.gameObject);
            //this.CleanUpGeneratedRoom(instance, connectionPoint);
            connectionPoint.enabled = false;
            //connectionPoint.gameObject.SetActive(false);

            this.SpawnConnectingRooms(instance, ++depth);
        }
    }

    private void CleanUpGeneratedRoom(GameObject room, ConnectionPoint fromConnectionPoint)
    {
        List<ConnectionPoint.Direction> toDelete = new List<ConnectionPoint.Direction>();
        ConnectionPoint.DirectionResult fromOpenings = fromConnectionPoint.GetRequiredOpenings(this.roomLayer);
        List<ConnectionPoint.Direction> fromRequiredOpeningDirections = fromOpenings.requiredDirections;

        for (int i = 0; i < fromRequiredOpeningDirections.Count; i++)
        {
            ConnectionPoint.Direction direction = fromRequiredOpeningDirections[i];
            toDelete.Add(fromConnectionPoint.FindOppositeDirection(direction));
        }

        ConnectionPoint[] connectionPoints = room.GetComponentsInChildren<ConnectionPoint>();
        for (int i = 0; i < connectionPoints.Count(); i++)
        {
            ConnectionPoint connectionPoint = connectionPoints[i];
            ConnectionPoint.DirectionResult openings = connectionPoint.GetRequiredOpenings(this.roomLayer);
            List<ConnectionPoint.Direction> requiredOpeningDirections = openings.requiredDirections;

            for (int j = 0; j < requiredOpeningDirections.Count; j++)
            {
                ConnectionPoint.Direction direction = requiredOpeningDirections[j];

                if (toDelete.Contains(direction))
                {
                    connectionPoint.enabled = false;
                    connectionPoint.gameObject.SetActive(false);
                }
            }
        }
    }

    private GameObject FindAppropriateRoom(ConnectionPoint connectionPoint)
    {
        GameObject[] rooms = null;
        ConnectionPoint.DirectionResult openings = connectionPoint.GetRequiredOpenings(this.roomLayer);
        List<ConnectionPoint.Direction> requiredOpeningDirections = openings.requiredDirections;

        if (requiredOpeningDirections.Count == 0)
            return null;
        
        for (int i = 0; i < requiredOpeningDirections.Count; i++)
        {
            ConnectionPoint.Direction requiredDirection = requiredOpeningDirections[i];
            GameObject[] tmp = this.FindRoomsForDirection(requiredDirection);

            if (rooms == null)
                rooms = tmp;
            else
                rooms = rooms.Intersect(tmp).ToArray();
        }

        /*
            NOTE: remove all rooms that have a path to a closed direction
        */

        int rand = Random.Range(0, rooms.Length);
        return rooms[rand];
    }

    private GameObject[] FindRoomsForDirection(ConnectionPoint.Direction direction)
    {
        switch (direction)
        {
            case ConnectionPoint.Direction.Top:
                return this.topRooms;
            case ConnectionPoint.Direction.Bottom:
                return this.bottomRooms;
            case ConnectionPoint.Direction.Left:
                return this.leftRooms;
            case ConnectionPoint.Direction.Right:
                return this.rightRooms;
            default:
                return this.topRooms; // This is impossible to reach
        }
    }
}
