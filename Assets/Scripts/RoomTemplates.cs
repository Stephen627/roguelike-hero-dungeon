using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;
    public List<GameObject> rooms;

    public float waitTime;
    public GameObject boss;

    private bool spawnedBoss;

    void Update()
    {
        if (this.spawnedBoss == false) {
            if (this.waitTime <= 0)
            {
                GameObject bossRoom = this.rooms[this.rooms.Count - 1];
                Instantiate(this.boss, bossRoom.transform.position, Quaternion.identity);
                this.spawnedBoss = true;
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
