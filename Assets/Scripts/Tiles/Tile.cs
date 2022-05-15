using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => this.isWalkable && this.OccupiedUnit == null;

    public virtual void Init(int x, int y) {
    }

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null)
            unit.OccupiedTile.OccupiedUnit = null;

        unit.transform.position = this.transform.position;
        this.OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    private void OnMouseEnter() {
        this.highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
        if (this.OccupiedUnit)
            this.OccupiedUnit.ShowHealth(true);
    }

    private void OnMouseExit() {
        this.highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        if (this.OccupiedUnit)
            this.OccupiedUnit.ShowHealth(false);
    }

    private void OnMouseDown() {
        if (GameManager.Instance.gameState != GameState.HeroesTurn) return;

        if (this.OccupiedUnit != null) {
            if (this.OccupiedUnit.Faction == Faction.Hero)
                UnitManager.Instance.SetSelectedHero((BaseHero) this.OccupiedUnit);
            else if (UnitManager.Instance.SelectedHero != null)
                UnitManager.Instance.SelectedHero.Attack((BaseEnemy) this.OccupiedUnit);
        } else if (UnitManager.Instance.SelectedHero != null)
            this.SetUnit(UnitManager.Instance.SelectedHero);
    }
}
