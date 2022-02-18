    using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public int columns;
    public int rows;
    public GameObject[] floorTiles;
    public GameObject wallMidTile;
    public GameObject wallLeftTile;
    public GameObject wallRightTile;
    public GameObject wallMidTopTile;
    public GameObject wallLeftTopTile;
    public GameObject wallRightTopTile;
    public GameObject wallCornerLeftBottom;
    public GameObject wallCornerRightBottom;
    public GameObject wallCornerLeftTop;
    public GameObject wallCornerRightTop;
    public GameObject openDoor;
    public GameObject closedDoor;
    public bool isDoorOpen = false;

    private Transform roomHolder;

    // Start is called before the first frame update
    void Start()
    {
        this.roomHolder = new GameObject("Room").transform;

        int halfColumns = this.columns / 2;
        int halfRows = this.rows / 2;

        int[] floorRotation = { 0, 90, 180, 270 };

        for (int x = halfColumns * -1; x < halfColumns; x++)
        {
            for (int y = halfRows * -1; y < halfRows; y++)
            {
                int rotation = floorRotation[Random.Range(0, floorRotation.Length)];
                GameObject floorTile = this.GetFloorTile();
                GameObject instance = Instantiate(floorTile, new Vector3(x, y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));//rotation)));

                instance.transform.SetParent(roomHolder);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetFloorTile()
    {
        int rand = Random.Range(0, this.floorTiles.Length);
        return this.floorTiles[rand];
    }
}
