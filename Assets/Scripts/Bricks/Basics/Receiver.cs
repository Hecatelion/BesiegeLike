using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Receiver : Brick, IActivable
{
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
		// really give power only in Play mode, in Editor vehicle musnt use receivers (e.g. reactors, cannons, etc.)
		if (TheGameManager.GameMode == e_GameMode.Play)
		{
			isReceivingPower = true;
		}

		// always show graphic feedback to inform player that electrical connection works
		activableGraph.SetON();
	}

	public void SetOFF()
	{
		isReceivingPower = false;
		activableGraph.SetOFF();
	}
}
