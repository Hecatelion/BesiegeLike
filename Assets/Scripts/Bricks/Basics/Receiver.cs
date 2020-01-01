using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Receiver : Brick, IActivable
{
	[SerializeField] bool pointBreak = false;
	public bool isReceivingPower = false;
	
	GraphicsActivable activableGraph;

	protected override void Start()
	{
		base.Start();
		
		activableGraph = GetComponent<GraphicsActivable>();
	}

	virtual protected void Update()
	{
		TestPowerReceiving();
	}

	protected void TestPowerReceiving()
	{
		isReceivingPower = false;

		List<Conductor> conductors = base.GetConnected<Conductor>();
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
