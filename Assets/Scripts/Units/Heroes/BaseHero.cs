using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public override void PerformedAction(int actionPoints)
    {
        base.PerformedAction(actionPoints);
        HeroEventArgs args = new HeroEventArgs();
        args.hero = this;
        EventManager.Instance.Invoke(EventType.SelectHero, args);
    }
}
