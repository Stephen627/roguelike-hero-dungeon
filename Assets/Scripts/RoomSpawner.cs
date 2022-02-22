using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    [Serializable]
    public class Coordinates
    {
        public float x;
        public float y;

        public Coordinates(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    public class Range
    {
        public int max;
        public int min;

        public Range(int max, int min)
        {
            this.max = max;
            this.min = min;
        }
    }

    public Range columns;
    public Range rows;
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
    public GameObject wallSideLeft;
    public GameObject wallSideRight;
    public Door door;
    public Coordinates[] doorLocations;

    [HideInInspector]
    public List<GameObject> doors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int columns = Random.Range(this.columns.min, this.columns.max);
        int rows = Random.Range(this.rows.min, this.rows.max);
        Room roomController = this.GetComponent<Room>();
        int halfColumns = columns / 2;
        int halfRows = rows / 2;

        int firstRow = halfColumns * -1;
        int lastRow = halfColumns - 1;

        int wallTopRow = halfRows + 1;
        int topWallsRow = halfRows;
        int bottomWallsRow = (halfRows * -1) - 1;

        for (int x = halfColumns * -1; x < halfColumns; x++)
        {
            bool inDoorCoords = this.WithinDoorTile(x);
            for (int y = (halfRows * -1) - 1; y < halfRows + 2; y++) // Modifications made to accommodate walls
            {
                GameObject toInstantiate = new GameObject();

                // First row is just all wall tops
                if (wallTopRow == y && !inDoorCoords)
                {
                    // Output wall tops
                    if (firstRow == x)
                        toInstantiate = this.wallLeftTopTile;
                    else if (lastRow == x)
                        toInstantiate = this.wallRightTopTile;
                    else
                        toInstantiate = this.wallMidTopTile;
                }
                else if (topWallsRow == y && !inDoorCoords)
                {
                    // Output walls and door in the middle
                    if (firstRow == x)
                        toInstantiate = this.wallCornerLeftTop;
                    else if (lastRow == x)
                        toInstantiate = this.wallCornerRightTop;
                    else
                        toInstantiate = this.wallMidTile;
                }
                else if (bottomWallsRow == y && !inDoorCoords)
                {
                    // Output walls and door in the middle
                    if (firstRow == x)
                        toInstantiate = this.wallLeftTile;
                    else if (lastRow == x)
                        toInstantiate = this.wallRightTile;
                    else
                        toInstantiate = this.wallMidTile;
                }
                else
                {
                    if (wallTopRow != y && topWallsRow != y)
                        toInstantiate = this.GetFloorTile();

                    if (firstRow == x && bottomWallsRow + 1 != y && !inDoorCoords)
                    {
                        GameObject sideWallInstance = Instantiate(this.wallSideLeft, new Vector3(x, y, 0), Quaternion.identity, this.gameObject.transform);
                    } 
                    else if (lastRow == x && bottomWallsRow + 1 != y && !inDoorCoords)
                    {
                        GameObject sideWallInstance = Instantiate(this.wallSideRight, new Vector3(x, y, 0), Quaternion.identity, this.gameObject.transform);
                    }

                    if (bottomWallsRow + 1 == y && !inDoorCoords)
                    {
                        GameObject wallTop = new GameObject();
                        if (firstRow == x)
                        {
                            wallTop = this.wallCornerLeftBottom;    
                        }
                        else if (lastRow == x)
                        {
                            wallTop = this.wallCornerRightBottom;
                        }
                        else
                        {
                            wallTop = this.wallMidTopTile;
                        }

                        GameObject wallTopInstance = Instantiate(wallTop, new Vector3(x, y, 0), Quaternion.identity, this.gameObject.transform);
                    }
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity, this.gameObject.transform);
                
            }
        }

        for (int i = 0; i < this.doorLocations.Length; i++)
        {
            Coordinates doorCoords = this.doorLocations[i];
            GameObject doorObject = this.door.gameObject;
            this.door.isDoorOpen = roomController.areDoorsOpen;
            GameObject instance = Instantiate(doorObject, new Vector3(doorCoords.x, doorCoords.y, 0), Quaternion.identity, this.gameObject.transform);

            this.doors.Add(instance);
        }
    }

    private bool WithinDoorTile(int x)
    {
        bool inCoords = false;

        for (int i = 0; i < this.doorLocations.Length; i++)
        {
            Coordinates doorCoords = this.doorLocations[i];
            inCoords = x >= doorCoords.x - 2 && x <= doorCoords.x + 2;
            if (inCoords)
                break;
        }

        return inCoords;
    }

    private GameObject GetFloorTile()
    {
        int rand = Random.Range(0, this.floorTiles.Length);
        return this.floorTiles[rand];
    }
}
