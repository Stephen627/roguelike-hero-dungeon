using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    private List<BaseHero> heros;
    private List<BaseEnemy> enemies;
    public BaseHero SelectedHero;

    private void Update()
    {
        if (this.SelectedHero)
            MenuManager.Instance.ShowSelectedHero(this.SelectedHero);
    }

    private void Awake()
    {
        UnitManager.Instance = this;

        this.units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        this.heros = new List<BaseHero>();
        this.enemies = new List<BaseEnemy>();

        EventManager.Instance.TileClick += this.OnTileClick;
    }

    private void OnDestroy()
    {
        EventManager.Instance.TileClick -= this.OnTileClick;    
    }

    public void BeginNewTurn(Faction faction)
    {
        switch (faction) {
            case Faction.Hero:
                this.heros.ForEach(unit => unit.BeginNewTurn());
            break;
            case Faction.Enemy:
                this.enemies.ForEach(unit => unit.BeginNewTurn());
            break;
        }
    }

    public void SpawnHeroes()
    {
        int heroCount = 3;

        for (int i = 0; i < heroCount; i++) {
            BaseHero randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            BaseHero spawnedHero = Instantiate(randomPrefab);
            Tile randomTile = GridManager.Instance.GetHeroSpawnTile();

            randomTile.SetUnit(spawnedHero);
            this.heros.Add(spawnedHero);
        }
    }

    public void SpawnEnemies()
    {
        int enemyCount = 1;

        for (int i = 0; i < enemyCount; i++) {
            BaseEnemy randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            BaseEnemy spawnedEnemy = Instantiate(randomPrefab);
            Tile randomTile = GridManager.Instance.GetEnemySpawnTile();

            randomTile.SetUnit(spawnedEnemy);
            this.enemies.Add(spawnedEnemy);
        }
    }

    public void SetSelectedHero(BaseHero hero)
    {
        MenuManager.Instance.ShowSelectedHero(hero);
        MenuManager.Instance.ShowMoves(hero);
        this.SelectedHero = hero;
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T) this.units.Where(u => u.faction == faction)
            .OrderBy(o => Random.value)
            .First()
            .unitPrefab;
    }

    private void OnTileClick(TileEventArgs args)
    {
        Tile tile = args.tile;
        if (GameManager.Instance.gameState != GameState.HeroesTurn) return;

        if (tile.OccupiedUnit != null) {
            if (tile.OccupiedUnit.Faction == Faction.Hero)
                this.SetSelectedHero((BaseHero) tile.OccupiedUnit);
            else if (ControlManager.Instance.SelectedMove)
                this.SelectedHero.AttackAtLocation(tile.transform.position, ControlManager.Instance.SelectedMove);
        } else if (UnitManager.Instance.SelectedHero != null && tile.Walkable) {
            if (ControlManager.Instance.SelectedMove)
                this.SelectedHero.AttackAtLocation(tile.transform.position, ControlManager.Instance.SelectedMove);
            else
                tile.SetUnit(UnitManager.Instance.SelectedHero);
        }
    }
}
