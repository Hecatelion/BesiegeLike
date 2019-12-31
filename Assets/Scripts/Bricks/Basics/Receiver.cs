using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Receiver : Brick, IActivable
{
	[SerializeField] bool pointBreak = false;
	public bool isReceivingPower = false;

	Brick brick;
	ActivableGraphics activableGraph;

	protected override void Start()
	{
		base.Start();

		brick = GetComponent<Brick>();
		activableGraph = GetComponent<ActivableGraphics>();
	}

	virtual protected void Update()
	{
		TestPowerReceiving();
	}

	protected void TestPowerReceiving()
	{
		isReceivingPower = false;

		List<Conductor> conductors = brick.GetConnected<Conductor>();
		foreach (var c in conductors)
		{
			// if this receiver is colliding with a powered conductor, then switch ON
			if (c.isConductingPower)
			{
				SetON();
				return;
			}
		}

		// if isnt connected to any conductor ON, then switch OFF
		SetOFF();
	}

	// IActivable implentation
	public void SetON()
	{
		isReceivingPower = true;
		activableGraph.SetON();
	}

	public void SetOFF()
	{
		isReceivingPower = false;
		activableGraph.SetOFF();
	}
}
