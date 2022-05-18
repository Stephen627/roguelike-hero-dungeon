using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    public BaseHero SelectedHero;

    private void Awake()
    {
        UnitManager.Instance = this;

        this.units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        int heroCount = 1;

        for (int i = 0; i < heroCount; i++) {
            BaseHero randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            BaseHero spawnedHero = Instantiate(randomPrefab);
            Tile randomTile = GridManager.Instance.GetHeroSpawnTile();

            randomTile.SetUnit(spawnedHero);
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
        if ((this.SelectedHero.transform.position - pos).magnitude > move.Range)
            return;
            
        var units = move.Behaviour.GetAffectedUnits(pos);
        for (int i = 0; i < units.Length; i++) {
            this.SelectedHero.Attack(move, units[i]);
        }
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T) this.units.Where(u => u.faction == faction)
            .OrderBy(o => Random.value)
            .First()
            .unitPrefab;
    }
}
