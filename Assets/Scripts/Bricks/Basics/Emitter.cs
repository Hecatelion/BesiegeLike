using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

		// get first conductors to test
		List<Conductor> conductorsToTest = (from ctt in GetConnected<Conductor>() where ctt.isUsable select ctt).ToList();
		Conductor testedConductor = null;
		int i = 0;

		// foreach (var testedCircuit in circuitsToTest) 
		// (while is used instead of classic foreach because it allows to add items to circuitsToTest dynamically where foreach would crash)
		while (i < conductorsToTest.Count)
		{
			testedConductor = conductorsToTest[i];

			// propagate power to currently tested brick
			testedConductor.GivePower();

			// find circuits connected to currently tested circuit
			List<Conductor> connectedConductors = testedConductor.GetConnected<Conductor>();

			// keep usable connected conductors and discard not usable ones
			connectedConductors = (from cc in connectedConductors where cc.isUsable select cc).ToList();

			// add those which are not in circuitsToTest yet
			foreach (var connectedConductor in connectedConductors)
			{
				if (!conductorsToTest.Contains(connectedConductor))
				{
					conductorsToTest.Add(connectedConductor);
				}
			}

			++i;
		}
	}
}
