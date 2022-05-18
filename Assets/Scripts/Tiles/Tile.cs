using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject highlight;
    [SerializeField] private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => this.isWalkable && this.OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
            unit.OccupiedTile.OccupiedUnit = null;

        unit.transform.position = this.transform.position;
        this.OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    private void OnMouseEnter()
    {
        ControlManager.Instance.OnMouseEnterTile(this);
    }

    private void OnMouseExit()
    {
        ControlManager.Instance.OnMouseExitTile(this);
    }

    private void OnMouseDown()
    {
        ControlManager.Instance.OnMouseDownTile(this);
    }
}
