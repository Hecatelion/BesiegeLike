using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// conducts electric power if it's connected to an emitter
public class CircuitBrick : Brick
{
	//Emitter emitter;
	//Receiver receiver;
	Activable activable;
	public bool isConductingPower = false;
	
    protected override void Start()
    {
		base.Start();

		//emitter = gameObject.AddComponent<Emitter>();
		//receiver = gameObject.AddComponent<Receiver>();
		activable = GetComponent<Activable>();
    }
	
    void Update()
    {
		//TestConductivity();
		isConductingPower = false;
	}

	public void SetON()
	{
		isConductingPower = true;
		activable.SetON();
	}

	public void SetOFF()
	{
		isConductingPower = false;
		activable.SetOFF();
	}

	/*void TestConductivity()
	{
		// detects if this circuit has to switch ON/OFF
		if (receiver.isReceivingPower != emitter.isEmittingPower)
		{
			// circuit has to switch ON -> is receiving but isnt emitting yet
			if (!emitter.isEmittingPower)
			{
				emitter.isEmittingPower = true;
				activable.SetON();
			}
			// circuit has to switch OFF -> is not receving but is still emitting
			else
			{
				emitter.isEmittingPower = false;
				activable.SetOFF();
			}
		}
	}*/
}
