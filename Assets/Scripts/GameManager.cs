using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject entryRoom;
    public GameObject largeRoom;
    public GameObject mediumRoom;
    public GameObject smallRoom;
    public int level = 1;

    void Start()
    {
        this.entryRoom.transform.position = new Vector3(0, 0, 0);
        this.entryRoom.GetComponent<RoomSpawner>().Spawn();
    }
}
