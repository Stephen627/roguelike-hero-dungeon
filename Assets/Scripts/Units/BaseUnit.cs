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
    private int CurrentActionPoints;

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

    private void Start()
    {
        this.CurrentHealthPoints = this.MaxHealthPoints;
        this.HealthBar.SetHealth(this.CurrentHealthPoints, this.MaxHealthPoints);
    }

    private void OnMouseEnter()
    {
        this.OccupiedTile.highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this.OccupiedTile);
        this.ShowHealth(true);
    }

    private void OnMouseExit()
    {
        this.OccupiedTile.highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        this.ShowHealth(false);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.HeroesTurn) return;

        if (this.Faction == Faction.Hero)
            UnitManager.Instance.SetSelectedHero((BaseHero) this);
        else
            UnitManager.Instance.AttackAtLocation(this.transform.position);
    }
}
