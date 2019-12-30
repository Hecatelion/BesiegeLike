using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : Brick
{
	public bool isEmittingPower = true;

	protected override void Start()
	{
		base.Start();
	}

	private void Update()
	{
		PropagatePower();
	}

	private void PropagatePower()
	{
		// from emitter, find all linked circuit and switch them ON step by step, 
		// stop when all encountered circuits are ON

		List<CircuitBrick> circuitsToTest = GetConnectedCircuits();
		do
		{
			foreach (var testedcircuit in circuitsToTest)
			{
				if (!testedcircuit.isConductingPower)
				{
					List<CircuitBrick> connectedCircuits = GetConnectedCircuits();
					circuitsToTest.AddRange(GetConnectedCircuits());
					testedcircuit.SetON();
				}
			}

		} while (circuitsToTest.Count > 0);
	}
}
