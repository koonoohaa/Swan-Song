﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 灼烧
/// </summary>
public class Burn : RoundBuff
{
    protected override void OnDisable()
    {
        base.OnDisable();
        Unit.TurnBeginning -= Unit_TurnBeginning;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Unit.TurnBeginning += Unit_TurnBeginning;
    }

    private void Unit_TurnBeginning()
    {
        (Unit as IHurtable).Hurt(Unit.UnitData.BloodMax * 0.05f, HurtType.FromBuff, this);
    }
}