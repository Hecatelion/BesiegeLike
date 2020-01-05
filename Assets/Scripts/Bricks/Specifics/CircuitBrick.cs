using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// simple conductor brick
public class CircuitBrick : Conductor
{
	protected override void Start()
	{
		base.Start();
		type = e_BrickType.Circuit;
	}
}
