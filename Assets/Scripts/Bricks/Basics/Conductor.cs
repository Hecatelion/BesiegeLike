using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// conducts electric power, is set ON by Emitters
public class Conductor : Brick, IActivable
{
	ActivableGraphics activableGraph;
	public bool isConductingPower = false;

	// used to switch OFF if not electrically powered for some frames
	int nbFrameBeforeOFF = 2;	// from 1 to inf, can't be 0
	int nbFrameLeft = int.MaxValue;

	protected override void Start()
	{
		base.Start();

		activableGraph = GetComponent<ActivableGraphics>();
		nbFrameLeft = nbFrameBeforeOFF;
	}

	void Update()
	{
		// if isnt powered for the delay frames allowed, then switch OFF
		if (nbFrameLeft == 0)
		{
			SetOFF();
		}
		else
		{
			nbFrameLeft--;
		}
	}

	public void GivePower()
	{
		SetON();

		// reinits timer
		nbFrameLeft = nbFrameBeforeOFF;
	}

	// IActivable implentation
	public void SetON()
	{
		isConductingPower = true;
		activableGraph.SetON();
	}

	public void SetOFF()
	{
		isConductingPower = false;
		activableGraph.SetOFF();
	}
}
