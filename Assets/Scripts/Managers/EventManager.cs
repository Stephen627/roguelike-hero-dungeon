using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NotifyTile(Tile tile);
public delegate void NotifyHero(BaseHero hero);

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event NotifyTile TileClick;
    public event NotifyHero HeroClick;

    private void Awake()
    {
        EventManager.Instance = this;

        this.TileClick += this.IsHeroTileClick;
    }

    public void IsHeroTileClick(Tile tile)
    {
        if (tile.OccupiedUnit != null && tile.OccupiedUnit.Faction == Faction.Hero)
            this.HeroClick?.Invoke((BaseHero) tile.OccupiedUnit);
    }

    public void Invoke(Tile tile)
    {
        this.TileClick?.Invoke(tile);
    }
}
