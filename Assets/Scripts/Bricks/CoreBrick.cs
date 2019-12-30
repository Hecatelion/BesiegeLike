using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// always emits eletric power
public class CoreBrick : Brick
{
    protected override void Start()
    {
		base.Start();

		gameObject.AddComponent<Emitter>().isEmittingPower = true;
    }
}
