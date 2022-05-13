using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;
    public int ActionPoints;
    public float MaxHealthPoints;
    public HealthBar HealthBar;
    public float Damage;
    private float CurrentHealthPoints;

    public void TakeDamage(float damage)
    {
        this.CurrentHealthPoints -= damage;
        this.HealthBar.SetHealth(this.CurrentHealthPoints, this.MaxHealthPoints);
        if (this.CurrentHealthPoints <= 0) {
            Destroy(this.gameObject);
        }
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
}
