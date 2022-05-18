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

    public void AttackAtLocation(Vector3 pos, Move move) 
    {
        // Position out of range
        if ((this.SelectedHero.transform.position - pos).magnitude > move.Range)
            return;

        // Hero doesn't have enough action points
        if (this.SelectedHero.CurrentActionPoints < move.ActionPoints)
            return;

        var units = move.Behaviour.GetAffectedUnits(pos);
        for (int i = 0; i < units.Length; i++) {
            this.SelectedHero.Attack(move, units[i]);
        }

        this.SelectedHero.PerformedAction(move.ActionPoints);

        if (this.heros.Where(hero => hero.TurnEnded).Count() == this.heros.Count())
            GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T) this.units.Where(u => u.faction == faction)
            .OrderBy(o => Random.value)
            .First()
            .unitPrefab;
    }
}
