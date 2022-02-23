using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject entryRoom;
    public GameObject[] topDoors;
    public GameObject[] bottomDoors;
    public GameObject[] leftDoors;
    public GameObject[] rightDoors;
    public int maxDepth;
    private List<GameObject> rooms;

    private void Start()
    {
        this.rooms = new List<GameObject>();
        GameObject instance = Instantiate(this.entryRoom, new Vector3(0, 0, 0), Quaternion.identity);

        this.rooms.Add(instance);
    }
}
