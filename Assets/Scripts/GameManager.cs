using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject entryRoom;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] terminatingRooms;
    public int maxDepth = 5;
    private List<GameObject> rooms;

    private void Start()
    {
        this.rooms = new List<GameObject>();
        GameObject instance = Instantiate(this.entryRoom, new Vector3(0, 0, 0), Quaternion.identity);

        this.rooms.Add(instance);
        this.SpawnConnectingRooms(instance);
    }

    private void SpawnConnectingRooms(GameObject room)
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
        }
    }

    private GameObject FindAppropriateRoom(ConnectionPoint connectionPoint)
    {
        GameObject[] rooms = {};
        switch (connectionPoint.requiredOpeningDirection)
        {
            case ConnectionPoint.Direction.Top:
                rooms = this.topRooms;
                break;
            case ConnectionPoint.Direction.Bottom:
                rooms = this.bottomRooms;
                break;
            case ConnectionPoint.Direction.Left:
                rooms = this.leftRooms;
                break;
            case ConnectionPoint.Direction.Right:
                rooms = this.rightRooms;
                break;
        }

        int rand = Random.Range(0, rooms.Length);
        return rooms[rand];
    }
}
