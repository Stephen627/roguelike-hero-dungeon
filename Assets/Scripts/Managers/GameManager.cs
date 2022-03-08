using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState gameState;

    private void Awake() {
        GameManager.Instance = this;    
    }

    private void Start() {
        this.ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState) {
        this.gameState = newState;

        switch (newState) {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                this.ChangeState(GameState.SpawnHeroes);
            break;
            case GameState.SpawnHeroes:
            break;
            case GameState.SpawnEnemies:
            break;
            case GameState.HeroesTurn:
            break;
            case GameState.EnemiesTurn:
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState {
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}
