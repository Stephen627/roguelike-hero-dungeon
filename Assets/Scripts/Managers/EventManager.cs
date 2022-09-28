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

public delegate void TileEvent(TileEventArgs args);
public delegate void HeroEvent(HeroEventArgs args);
public delegate void SpawnHero(HeroEventArgs args);
public delegate void SpawnEnemy(EnemyEventArgs args);
public delegate void EmptyEvent();

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event TileEvent TileClick;
    public event TileEvent TileFocus;
    public event TileEvent TileBlur;
    public event EmptyEvent EndPlayerTurn;
    public event HeroEvent SelectHero;
    public event SpawnHero SpawnHero;
    public event SpawnEnemy SpawnEnemy;

    private void Awake()
    {
        EventManager.Instance = this;
    }

    public void Invoke(EventType evt, EventArgs args)
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
            case EventType.EndPlayerTurn:
                this.EndPlayerTurn?.Invoke();
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
        }
    }
}

public enum EventType
{
    TileClick,
    TileFocus,
    TileBlur,
    EndPlayerTurn,
    SelectHero,
    SpawnHero,
    SpawnEnemy,
}