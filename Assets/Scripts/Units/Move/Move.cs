using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Move")]
public class Move : ScriptableObject
{
    public string Name;
    public float Damage;
    public float Range;
    public int ActionPoints;
    [SerializeField] public Behaviour Behaviour;

}

