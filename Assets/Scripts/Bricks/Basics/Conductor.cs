﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// conducts electric power, is set ON by Emitters
public class Conductor : Brick
{
	Activable activable;
	public bool isConductingPower = false;

	// used to switch OFF if not electrically powered for some frames
	int nbFrameBeforeOFF = 2;	// from 1 to inf, can't be 0
	int nbFrameLeft = int.MaxValue;

	protected override void Start()
	{
		base.Start();

		activable = GetComponent<Activable>();
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

	protected void SetON()
	{
		isConductingPower = true;
		activable.SetON();
	}

	protected void SetOFF()
	{
		isConductingPower = false;
		activable.SetOFF();
	}

	public void GivePower()
	{
		SetON();

		// reinits timer
		nbFrameLeft = nbFrameBeforeOFF;
	}
}
