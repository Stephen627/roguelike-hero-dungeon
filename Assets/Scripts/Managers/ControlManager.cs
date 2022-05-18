using System;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance;
    public Move SelectedMove;
    
    private void Awake()
    {
        ControlManager.Instance = this;    
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            this.SelectedMove = null;
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
            else if (this.SelectedMove)
                UnitManager.Instance.AttackAtLocation(tile.transform.position, this.SelectedMove);
        } else if (UnitManager.Instance.SelectedHero != null && tile.Walkable) {
            if (this.SelectedMove)
                UnitManager.Instance.AttackAtLocation(tile.transform.position, this.SelectedMove);
            else
                tile.SetUnit(UnitManager.Instance.SelectedHero);
        }
    }

    public void SetSelectedMove(Move move)
    {
        this.SelectedMove = move;
    }
}