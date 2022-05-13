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
    private float CurrentHealthPoints;


    private void Start()
    {
        this.CurrentHealthPoints = this.MaxHealthPoints;
        this.HealthBar.SetHealth(this.CurrentHealthPoints, this.MaxHealthPoints);
    }
}
