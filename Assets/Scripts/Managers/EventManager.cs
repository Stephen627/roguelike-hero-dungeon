using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventArgs
{
}
public class TileEventArgs : EventArgs
{
    public Tile tile { get; }

    public TileEventArgs(Tile tile)
    {
        this.tile = tile;
    }
}
public class HeroEventArgs : EventArgs
{
    public BaseHero hero { get; }

    public HeroEventArgs(BaseHero hero)
    {
        this.hero = hero;
    }
}
public class EnemyEventArgs : EventArgs
{
    public BaseEnemy enemy { get; }

    public EnemyEventArgs(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }
}
public class FactionEventArgs : EventArgs
{
    public Faction faction { get; }

    public FactionEventArgs(Faction faction)
    {
        this.faction = faction;
    }
}
public class GameStateEventArgs : EventArgs
{
    public GameState state { get; }

    public GameStateEventArgs(GameState state)
    {
        this.state = state;
    }
}

public delegate void TileEvent(TileEventArgs args);
public delegate void HeroEvent(HeroEventArgs args);
public delegate void EnemyEvent(EnemyEventArgs args);
public delegate void FactionEvent(FactionEventArgs args);
public delegate void GameStateEvent(GameStateEventArgs args);
public delegate void EmptyEvent();

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event TileEvent TileClick;
    public event TileEvent TileFocus;
    public event TileEvent TileBlur;
    public event GameStateEvent ChangeGameState;
    public event HeroEvent SelectHero;
    public event HeroEvent SpawnHero;
    public event EnemyEvent SpawnEnemy;
    public event FactionEvent StartTurn;
    public event FactionEvent SpawnUnits;
    public event EmptyEvent GenerateMap;

    private void Awake()
    {
        EventManager.Instance = this;
    }

    public void Invoke(EventType evt, EventArgs args = null)
    {
        switch (evt) {
            case EventType.TileClick:
                this.TileClick?.Invoke((TileEventArgs) args);
            break;
            case EventType.TileFocus:
                this.TileFocus?.Invoke((TileEventArgs) args);
            break;
            case EventType.TileBlur:
                this.TileBlur?.Invoke((TileEventArgs) args);
            break;
            case EventType.ChangeGameState:
                this.ChangeGameState?.Invoke((GameStateEventArgs) args);
            break;
            case EventType.SelectHero:
                this.SelectHero?.Invoke((HeroEventArgs) args);
            break;
            case EventType.SpawnHero:
                this.SpawnHero?.Invoke((HeroEventArgs) args);
            break;
            case EventType.SpawnEnemy:
                this.SpawnEnemy?.Invoke((EnemyEventArgs) args);
            break;
            case EventType.StartTurn:
                this.StartTurn?.Invoke((FactionEventArgs) args);
            break;
            case EventType.SpawnUnits:
                this.SpawnUnits?.Invoke((FactionEventArgs) args);
            break;
            case EventType.GenerateMap:
                this.GenerateMap?.Invoke();
            break;
        }
    }
}

public enum EventType
{
    TileClick,
    TileFocus,
    TileBlur,
    ChangeGameState,
    SelectHero,
    SpawnHero,
    SpawnEnemy,
    StartTurn,
    SpawnUnits,
    GenerateMap,
}