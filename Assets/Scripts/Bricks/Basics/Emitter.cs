using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// emits power, give power to connected conductors links
public class Emitter : Brick
{
	public bool isEmittingPower = false;

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
		// from emitter, finds all linked circuit and give them power step by step, 
		// stops when all encountered circuits are ON

		List<Conductor> circuitsToTest = GetConnected<Conductor>();
		Conductor testedCircuit = null;
		int i = 0;

		// foreach (var testedCircuit in circuitsToTest) 
		// (while is used instead of classic foreach because it allows to add items to circuitsToTest dynamically where foreach would crash)
		while (i < circuitsToTest.Count)
		{
			testedCircuit = circuitsToTest[i];

			// propagate power to currently tested brick
			testedCircuit.GivePower();

			// find circuits connected to currently tested circuit
			List<Conductor> connectedCircuits = testedCircuit.GetConnected<Conductor>();

			// add those which are not in circuitsToTest yet
			foreach (var connectedCircuit in connectedCircuits)
			{
				if (!circuitsToTest.Contains(connectedCircuit))
				{
					circuitsToTest.Add(connectedCircuit);
				}
			}

			++i;
		}
	}
}
