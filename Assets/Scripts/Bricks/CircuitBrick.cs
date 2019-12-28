using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// conducts electric power if it's connected to an emitter
public class CircuitBrick : MonoBehaviour
{
	Emitter emitter;
	Receiver receiver;
	
    void Start()
    {
		emitter = gameObject.AddComponent<Emitter>();
		receiver = gameObject.AddComponent<Receiver>();
    }
	
    void Update()
    {
		if (receiver.isReceivingPower)
		{
			emitter.isEmittingPower = true;
		}
    }
}
