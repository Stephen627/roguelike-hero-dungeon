using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool areDoorsOpen = false;

    private RoomSpawner roomSpawner;

    // Start is called before the first frame update
    void Start()
    {
        this.roomSpawner = this.GetComponent<RoomSpawner>();
    }

    void Update()
    {
        for (int i = 0; i < this.roomSpawner.doors.Count; i++)
        {
            this.roomSpawner.doors[i].GetComponent<Door>().isDoorOpen = this.areDoorsOpen;
        }
    }
}
