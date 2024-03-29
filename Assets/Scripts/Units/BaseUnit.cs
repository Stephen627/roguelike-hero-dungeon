using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;
    public int MaxActionPoints;
    public float MaxHealthPoints;
    public HealthBar HealthBar;
    public Move[] Moves;
    private float CurrentHealthPoints;
    public int CurrentActionPoints;
    public float Speed = 2f;
    public bool TurnEnded => this.ended || this.CurrentActionPoints <= 0;
    private bool ended;

    public void TakeDamage(float damage)
    {
        this.CurrentHealthPoints -= damage;
        this.HealthBar.SetHealth(this.CurrentHealthPoints, this.MaxHealthPoints);
        if (this.CurrentHealthPoints <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void Attack(Move move, BaseUnit attackee)
    {
        attackee.TakeDamage(move.Damage);
    }

    public void ShowHealth(bool show)
    {
        this.HealthBar.gameObject.SetActive(show);
    }

    public void BeginNewTurn()
    {
        this.CurrentActionPoints = this.MaxActionPoints;
        this.ended = false;
    }

    public virtual void PerformedAction(int actionPoints)
    {
        this.CurrentActionPoints -= actionPoints;

        if (this.CurrentActionPoints < 0)
            this.CurrentActionPoints = 0;
    }

    public void AttackAtLocation(Vector3 pos, Move move) 
    {
        // Position out of range
        if ((this.transform.position - pos).magnitude > move.Range)
            return;

        // Hero doesn't have enough action points
        if (this.CurrentActionPoints < move.ActionPoints)
            return;

        var units = move.Behaviour.GetAffectedUnits(pos);
        for (int i = 0; i < units.Length; i++) {
            this.Attack(move, units[i]);
        }

        this.PerformedAction(move.ActionPoints);
    }

    private void Start()
    {
        this.CurrentHealthPoints = this.MaxHealthPoints;
        this.HealthBar.SetHealth(this.CurrentHealthPoints, this.MaxHealthPoints);
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
        TileEventArgs args = new TileEventArgs(this.OccupiedTile);
        return args;
    }
}
