using System;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance;
    
    private void Awake()
    {
        ControlManager.Instance = this;    
    }

    public void OnMouseEnterTile(Tile tile)
    {
        tile.highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(tile);
        if (tile.OccupiedUnit)
            tile.OccupiedUnit.ShowHealth(true);

    }

    public void OnMouseExitTile(Tile tile)
    {
        tile.highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        if (tile.OccupiedUnit)
            tile.OccupiedUnit.ShowHealth(false);
    }

    public void OnMouseDownTile(Tile tile)
    {
        if (GameManager.Instance.gameState != GameState.HeroesTurn) return;

        if (tile.OccupiedUnit != null) {
            if (tile.OccupiedUnit.Faction == Faction.Hero)
                UnitManager.Instance.SetSelectedHero((BaseHero) tile.OccupiedUnit);
            else
                UnitManager.Instance.AttackAtLocation(tile.transform.position);
        } else if (UnitManager.Instance.SelectedHero != null && tile.Walkable) {
            if (UnitManager.Instance.SelectedMove)
                UnitManager.Instance.AttackAtLocation(tile.transform.position);
            else
                tile.SetUnit(UnitManager.Instance.SelectedHero);
        }
    }
}