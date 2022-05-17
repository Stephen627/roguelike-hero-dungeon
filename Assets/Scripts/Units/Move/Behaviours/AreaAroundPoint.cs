using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Behaviour")]
public class AreaAroundPoint : Behaviour
{
    public int Radius = 1;

    public override BaseUnit[] GetAffectedUnits(Vector3 initialPos)
    {
        return Physics2D.OverlapCircleAll(initialPos, this.Radius)
            .Select(c=>c.gameObject.GetComponent<BaseUnit>())
            .Where(c=>c)
            .ToArray();
    }
}