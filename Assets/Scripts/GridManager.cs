using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform mainCamera;
    private Dictionary<Vector2, Tile> tiles;

    private void Start() {
        this.GenerateGrid();
    }

    private void GenerateGrid() {
        this.tiles = new Dictionary<Vector2, Tile>();
        GameObject tilesParentObject = new GameObject("Tiles");
        for (int x = 0; x < this.width; x++) {
            for (int y = 0; y < this.height; y++) {
                Tile spawnedTile = Instantiate<Tile>(this.tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = tilesParentObject.transform;
                spawnedTile.name = $"Tile {x}x{y}";

                bool isOffset = (x + y) % 2 == 1;
                spawnedTile.Init(isOffset);

                this.tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        this.mainCamera.position = new Vector3((float) this.width / 2 - 0.5f, (float) this.height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 position) {
        if (this.tiles.TryGetValue(position, out Tile tile))
            return tile;

        return null;
    }
}
