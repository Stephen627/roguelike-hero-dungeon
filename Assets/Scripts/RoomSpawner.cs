using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        int rand;
        switch (this.openingDirection) {
            case 1:
                // Need to spawn a room with a BOTTOM door
                rand = Random.Range(0, this.templates.bottomRooms.Length);
                Instantiate(
                    this.templates.bottomRooms[rand],
                    this.transform.position,
                    this.templates.bottomRooms[rand].transform.rotation
                );
                break;
            case 2:
                // Need to spawn a room with a TOP door
                rand = Random.Range(0, this.templates.topRooms.Length);
                Instantiate(
                    this.templates.topRooms[rand],
                    this.transform.position,
                    this.templates.topRooms[rand].transform.rotation
                );
                break;
            case 3:
                // Need to spawn a room with a LEFT door
                rand = Random.Range(0, this.templates.leftRooms.Length);
                Instantiate(
                    this.templates.leftRooms[rand],
                    this.transform.position,
                    this.templates.leftRooms[rand].transform.rotation
                );
                break;
            case 4:
                // Need to spawn a room with a RIGHT door
                rand = Random.Range(0, this.templates.rightRooms.Length);
                Instantiate(
                    this.templates.rightRooms[rand],
                    this.transform.position,
                    this.templates.rightRooms[rand].transform.rotation
                );
                break;
        }

        this.spawned = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && this.spawned == false) {
                Instantiate(
                    this.templates.closedRoom,
                    this.transform.position,
                    Quaternion.identity
                );
                Destroy(this.gameObject);
            }
            this.spawned = true;
        }
    }
}
