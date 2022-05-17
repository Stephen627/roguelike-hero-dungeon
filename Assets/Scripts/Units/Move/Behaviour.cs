using UnityEngine;

public abstract class Behaviour : ScriptableObject
{
    public abstract BaseUnit[] GetAffectedUnits(Vector3 initialPos);
}