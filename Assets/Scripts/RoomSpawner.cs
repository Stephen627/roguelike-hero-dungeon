using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomSpawner : MonoBehaviour
{
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    public int openingDirection;
    public float waitTime = 4f;

    private RoomTemplates templates;
    private float spawnDelay = 0.1f;
    private bool spawned = false;

    void Start()
    {
        Destroy(gameObject, this.waitTime);
        this.templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", this.spawnDelay);
    }

    void Spawn()
    {
        if (this.spawned == true)
            return;

        GameObject[] rooms = this.GetAssociatedRooms(this.openingDirection);
        int rand = Random.Range(0, rooms.Length);
        Instantiate(rooms[rand], this.transform.position, rooms[rand].transform.rotation);

        this.spawned = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint")) {
            RoomSpawner otherRoom = other.GetComponent<RoomSpawner>();
            if (otherRoom.spawned == false && this.spawned == false) {
                GameObject[] myRooms = this.GetAssociatedRooms(this.openingDirection);
                GameObject[] otherRooms = this.GetAssociatedRooms(otherRoom.openingDirection);

                GameObject[] sharedRooms = myRooms.Intersect(otherRooms).ToArray();
                int rand = Random.Range(0, sharedRooms.Length);

                Instantiate(
                    sharedRooms[rand],
                    this.transform.position,
                    Quaternion.identity
                );
                Destroy(this.gameObject);
            }
            this.spawned = true;
        }
    }

    GameObject[] GetAssociatedRooms(int openingDirection)
    {
        switch (openingDirection) {
            case 1:
                return this.templates.bottomRooms;
            case 2:
                return this.templates.topRooms;
            case 3:
                return this.templates.leftRooms;
            case 4:
                return this.templates.rightRooms;
            default:
                return this.templates.bottomRooms;
        }
    }
}
