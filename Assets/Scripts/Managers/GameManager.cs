using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;

    public Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        GameManager.Instance = this;    
    }

    private void Start()
    {
        this.ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        this.gameState = newState;

        switch (newState) {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                this.ChangeState(GameState.SpawnHeroes);
            break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                this.ChangeState(GameState.HeroesTurn);
            break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                this.ChangeState(GameState.SpawnEnemies);
            break;
            case GameState.HeroesTurn:
                UnitManager.Instance.SetToDefaultHero();
                UnitManager.Instance.BeginNewTurn(Faction.Hero);
            break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.BeginNewTurn(Faction.Enemy);
                this.ChangeState(GameState.HeroesTurn);
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        this.OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}
