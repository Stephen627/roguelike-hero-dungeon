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
    public GameObject wallSideLeft;
    public GameObject wallSideRight;
    public GameObject openDoor;
    public GameObject closedDoor;
    public bool isDoorOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        int halfColumns = this.columns / 2;
        int halfRows = this.rows / 2;

        int firstRow = halfColumns * -1;
        int lastRow = halfColumns - 1;

        int wallTopRow = halfRows + 1;
        int topWallsRow = halfRows;
        int bottomWallsRow = (halfRows * -1) - 1;

        for (int x = halfColumns * -1; x < halfColumns; x++)
        {
            for (int y = (halfRows * -1) - 1; y < halfRows + 2; y++) // Modifications made to accommodate walls
            {
                GameObject toInstantiate = new GameObject();

                // First row is just all wall tops
                if (wallTopRow == y)
                {
                    // Output wall tops
                    if (firstRow == x)
                        toInstantiate = this.wallLeftTopTile;
                    else if (lastRow == x)
                        toInstantiate = this.wallRightTopTile;
                    else
                        toInstantiate = this.wallMidTopTile;
                }
                else if (topWallsRow == y)
                {
                    // Output walls and door in the middle
                    if (firstRow == x)
                        toInstantiate = this.wallCornerLeftTop;
                    else if (lastRow == x)
                        toInstantiate = this.wallCornerRightTop;
                    else
                        toInstantiate = this.wallMidTile;
                }
                else if (bottomWallsRow == y)
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
                    toInstantiate = this.GetFloorTile();

                    if (firstRow == x && bottomWallsRow + 1 != y)
                    {
                        GameObject sideWallInstance = Instantiate(this.wallSideLeft, new Vector3(x, y, 0), Quaternion.identity);
                        sideWallInstance.transform.SetParent(this.gameObject.transform);
                    } 
                    else if (lastRow == x && bottomWallsRow + 1 != y)
                    {
                        GameObject sideWallInstance = Instantiate(this.wallSideRight, new Vector3(x, y, 0), Quaternion.identity);
                        sideWallInstance.transform.SetParent(this.gameObject.transform);
                    }

                    if (bottomWallsRow + 1 == y)
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

                        GameObject wallTopInstance = Instantiate(wallTop, new Vector3(x, y, 0), Quaternion.identity);
                        wallTopInstance.transform.SetParent(this.gameObject.transform);
                    }
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity);
                instance.transform.SetParent(this.gameObject.transform);
                
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
