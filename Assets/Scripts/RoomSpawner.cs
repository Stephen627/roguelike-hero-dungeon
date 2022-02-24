using System.Collections.Generic;
using System.Linq;
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
    private List<GameObject> rooms;

    private void Start()
    {
        this.rooms = new List<GameObject>();
        GameObject instance = Instantiate(this.entryRoom, new Vector3(0, 0, 0), Quaternion.identity);

        this.rooms.Add(instance);
        this.SpawnConnectingRooms(instance);
    }

    private void SpawnConnectingRooms(GameObject room, int depth = 1)
    {
        ConnectionPoint[] connectionPoints = room.GetComponentsInChildren<ConnectionPoint>();

        for (int i = 0; i < connectionPoints.Length; i++)
        {
            ConnectionPoint connectionPoint = connectionPoints[i];
            Transform transform = connectionPoint.gameObject.transform;
            GameObject spawnRoom = this.FindAppropriateRoom(connectionPoint);
            
            GameObject instance = Instantiate(
                spawnRoom,
                new Vector3(transform.position.x, transform.position.y, 0),
                Quaternion.identity
            );
            this.rooms.Add(instance);
            Destroy(connectionPoint.gameObject);
        }
    }

    private GameObject FindAppropriateRoom(ConnectionPoint connectionPoint)
    {
        GameObject[] rooms = null;
        
        for (int i = 0; i < connectionPoint.requiredOpeningDirections.Count; i++)
        {
            ConnectionPoint.Direction requiredDirection = connectionPoint.requiredOpeningDirections[i];
            GameObject[] tmp = this.FindRoomsForDirection(requiredDirection);

            if (rooms == null)
                rooms = tmp;
            else
                rooms = rooms.Intersect(tmp).ToArray();
        }

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
