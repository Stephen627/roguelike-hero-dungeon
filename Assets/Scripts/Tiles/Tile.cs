using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject highlight;
    [SerializeField] public GameObject moveable;
    [SerializeField] private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool IsMoveable = false;
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
        EventManager.Instance.Invoke(EventType.TileFocus, this.GetArgsForThis());
    }

    private void OnMouseExit()
    {
        EventManager.Instance.Invoke(EventType.TileBlur, this.GetArgsForThis());
    }

    private void OnMouseDown()
    {
        EventManager.Instance.Invoke(EventType.TileClick, this.GetArgsForThis());
    }

    private TileEventArgs GetArgsForThis()
    {
        TileEventArgs args = new TileEventArgs(this);
        return args;
    }
}
