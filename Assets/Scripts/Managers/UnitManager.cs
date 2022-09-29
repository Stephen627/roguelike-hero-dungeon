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

    private void Awake()
    {
        UnitManager.Instance = this;

        this.units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        this.heros = new List<BaseHero>();
        this.enemies = new List<BaseEnemy>();
    }

    private void Start()
    {
        EventManager.Instance.TileClick += this.OnTileClick;
        EventManager.Instance.StartTurn += this.BeginNewTurn;
        EventManager.Instance.SpawnUnits += this.SpawnFaction;
    }

    private void OnDestroy()
    {
        EventManager.Instance.TileClick -= this.OnTileClick;
        EventManager.Instance.StartTurn -= this.BeginNewTurn;
        EventManager.Instance.SpawnUnits -= this.SpawnFaction;
    }

    public void BeginNewTurn(FactionEventArgs args)
    {
        switch (args.faction) {
            case Faction.Hero:
                this.heros.ForEach(unit => unit.BeginNewTurn());
                this.SetToDefaultHero();
            break;
            case Faction.Enemy:
                this.enemies.ForEach(unit => unit.BeginNewTurn());
            break;
        }
    }

    public void SpawnFaction(FactionEventArgs args)
    {
        switch (args.faction) {
            case Faction.Hero:
                this.SpawnHeroes();
            break;
            case Faction.Enemy:
                this.SpawnEnemies();
            break;
        }
    }

    private void SpawnHeroes()
    {
        int heroCount = 1;

        for (int i = 0; i < heroCount; i++) {
            BaseHero randomPrefab = this.GetRandomUnit<BaseHero>(Faction.Hero);
            BaseHero spawnedHero = Instantiate(randomPrefab);

            HeroEventArgs args = new HeroEventArgs(spawnedHero);
            EventManager.Instance.Invoke(EventType.SpawnHero, args);
            
            this.heros.Add(spawnedHero);
        }
    }

    private void SpawnEnemies()
    {
        int enemyCount = 1;

        for (int i = 0; i < enemyCount; i++) {
            BaseEnemy randomPrefab = this.GetRandomUnit<BaseEnemy>(Faction.Enemy);
            BaseEnemy spawnedEnemy = Instantiate(randomPrefab);

            EnemyEventArgs args = new EnemyEventArgs(spawnedEnemy);
            EventManager.Instance.Invoke(EventType.SpawnEnemy, args);

            this.enemies.Add(spawnedEnemy);
        }
    }

    public void SetSelectedHero(BaseHero hero)
    {
        HeroEventArgs args = new HeroEventArgs(hero);
        EventManager.Instance.Invoke(EventType.SelectHero, args);
        EventManager.Instance.Invoke(EventType.ShowMoveableTiles, args);
        this.SelectedHero = hero;
    }

    public void SetToDefaultHero()
    {
        this.SetSelectedHero(this.heros[0]);
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
        } else if (this.SelectedHero != null && tile.Walkable) {
            if (ControlManager.Instance.SelectedMove)
                this.SelectedHero.AttackAtLocation(tile.transform.position, ControlManager.Instance.SelectedMove);
            else {
                if (!tile.IsMoveable)
                    return;

                float range = (this.SelectedHero.transform.position - tile.transform.position).magnitude;
                int usedActionPoints = (int) System.Math.Ceiling(range / this.SelectedHero.Speed);
                Debug.Log(usedActionPoints);
                tile.SetUnit(this.SelectedHero);
                this.SelectedHero.PerformedAction(usedActionPoints);
                HeroEventArgs moveableTileArgs = new HeroEventArgs(this.SelectedHero);
                EventManager.Instance.Invoke(EventType.ShowMoveableTiles, moveableTileArgs);
            }
        }
    }
}
