using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Start()
    {
        this.templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        this.templates.rooms.Add(this.gameObject);
    }
}
