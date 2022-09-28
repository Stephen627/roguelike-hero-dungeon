using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventArgs
{
}
public class TileEventArgs : EventArgs
{
    public Tile tile;
}
public class HeroEventArgs : EventArgs
{
    public BaseHero hero;
}
public delegate void TileEvent(TileEventArgs args);
public delegate void HeroEvent(HeroEventArgs args);
public delegate void EmptyEvent();

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event TileEvent TileClick;
    public event TileEvent TileFocus;
    public event TileEvent TileBlur;
    public event EmptyEvent EndPlayerTurn;
    public event HeroEvent SelectHero;

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
}