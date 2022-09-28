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
        EventManager.Instance.ChangeGameState += this.ChangeGameState;
    }

    private void OnDestroy()
    {
        EventManager.Instance.ChangeGameState -= this.ChangeGameState;    
    }

    private void ChangeGameState(GameStateEventArgs args)
    {
        this.ChangeState(args.state);
    }

    public void ChangeState(GameState newState)
    {
        this.gameState = newState;

        switch (newState) {
            case GameState.GenerateGrid:
                EventManager.Instance.Invoke(EventType.GenerateMap);
                this.ChangeState(GameState.SpawnEnemies);
            break;
            case GameState.SpawnEnemies:
                FactionEventArgs spawnEnemiesArgs = new FactionEventArgs(Faction.Enemy);
                EventManager.Instance.Invoke(EventType.SpawnUnits, spawnEnemiesArgs);

                this.ChangeState(GameState.SpawnHeroes);
            break;
            case GameState.SpawnHeroes:
                FactionEventArgs spawnHeroesArgs = new FactionEventArgs(Faction.Hero);
                EventManager.Instance.Invoke(EventType.SpawnUnits, spawnHeroesArgs);

                this.ChangeState(GameState.DecideEnemyNextTurn);
            break;
            case GameState.DecideEnemyNextTurn:
                // Display an animation to show the player what the next move will be
                // Trigger an event to change the state when this state is done
                this.ChangeState(GameState.HeroesTurn);
            break;
            case GameState.HeroesTurn:
                FactionEventArgs heroTurnArgs = new FactionEventArgs(Faction.Hero);
                EventManager.Instance.Invoke(EventType.StartTurn, heroTurnArgs);
            break;
            case GameState.EnemiesTurn:
                FactionEventArgs enemyTurnArgs = new FactionEventArgs(Faction.Enemy);
                EventManager.Instance.Invoke(EventType.StartTurn, enemyTurnArgs);

                // Trigger an event to change the state when this state is done
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
    EnemiesTurn = 4,
    DecideEnemyNextTurn = 5,
}
