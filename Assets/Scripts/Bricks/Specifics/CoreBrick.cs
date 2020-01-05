using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// always emits eletric power
public class CoreBrick : Emitter
{
    protected override void Start()
    {
		base.Start();
		type = e_BrickType.Core;

		isEmittingPower = true;
    }
}
