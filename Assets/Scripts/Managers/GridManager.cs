using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, mountainTile;
    [SerializeField] private Transform mainCamera;
    private Dictionary<Vector2, Tile> tiles;

    private void Awake()
    {
        GridManager.Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.SpawnHero += this.SpawnHero;
        EventManager.Instance.SpawnEnemy += this.SpawnEnemy;
        EventManager.Instance.GenerateMap += this.GenerateGrid;
        EventManager.Instance.ShowMoveableTiles += this.ShowMoveableTiles;
    }

    private void OnDestroy()
    {
        EventManager.Instance.SpawnHero -= this.SpawnHero;
        EventManager.Instance.SpawnEnemy -= this.SpawnEnemy;
        EventManager.Instance.GenerateMap -= this.GenerateGrid;
        EventManager.Instance.ShowMoveableTiles -= this.ShowMoveableTiles;
    }

    public void GenerateGrid()
    {
        this.tiles = new Dictionary<Vector2, Tile>();
        GameObject tilesParentObject = new GameObject("Tiles");
        for (int x = 0; x < this.width; x++) {
            for (int y = 0; y < this.height; y++) {
                Tile randomTile = this.grassTile;
                Tile spawnedTile = Instantiate<Tile>(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.parent = tilesParentObject.transform;
                spawnedTile.name = $"Tile {x}x{y}";

                spawnedTile.Init(x, y);

                this.tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        this.mainCamera.position = new Vector3((float) this.width / 2 - 0.5f, (float) this.height / 2 - 0.5f, -10);
    }

    public void SpawnHero(HeroEventArgs args)
    {
        Tile randomTile = this.GetHeroSpawnTile();
        randomTile.SetUnit(args.hero);
    }

    public Tile GetHeroSpawnTile()
    {
        return this.tiles.Where(t => t.Key.x < this.width / 2 && t.Value.Walkable)
            .OrderBy(o => Random.value)
            .First()
            .Value;
    }

    public void SpawnEnemy(EnemyEventArgs args)
    {
        Tile randomTile = this.GetEnemySpawnTile();
        randomTile.SetUnit(args.enemy);
    }

    public Tile GetEnemySpawnTile()
    {
        return this.tiles.Where(t => t.Key.x > this.width / 2 && t.Value.Walkable)
            .OrderBy(o => Random.value)
            .First()
            .Value;
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (this.tiles.TryGetValue(position, out Tile tile))
            return tile;

        return null;
    }

    public void ShowMoveableTiles(HeroEventArgs args)
    {
        this.HideMoveableTiles();

        Tile centerTile = args.hero.OccupiedTile;
        float moveRadius = args.hero.CurrentActionPoints * args.hero.Speed;

        Tile[] moveableTiles = Physics2D.OverlapCircleAll(centerTile.transform.position, moveRadius)
            .Select(c=>c.gameObject.GetComponent<Tile>())
            .Where(c=>c)
            .ToArray();


        for (int i = 0; i < moveableTiles.Length; i++) {
            moveableTiles[i].moveable.SetActive(true);
            moveableTiles[i].IsMoveable = true;
        }
    }

    public void HideMoveableTiles()
    {
        Tile[] allTiles = UnityEngine.Object.FindObjectsOfType<Tile>();
        foreach (KeyValuePair<Vector2, Tile> tile in tiles) {
            tile.Value.moveable.SetActive(false);
            tile.Value.IsMoveable = false;
        }
    }
}
