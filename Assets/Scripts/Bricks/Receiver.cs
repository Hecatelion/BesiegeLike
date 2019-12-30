using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Receiver : MonoBehaviour
{
	[SerializeField] bool pointBreak = false;
	public bool isReceivingPower = false;

	Brick brick;

	private void Start()
	{
		brick = GetComponent<Brick>();
	}

	private void Update()
	{
		if (pointBreak)
		{
			int i = 0;
		}
		List<Brick> bricks = brick.GetConnectedBricks();
		TestPowerReceiving(bricks); // must merge l.24 & l.25
	}

	protected void TestPowerReceiving(List<Brick> _connectedBricks)
	{
		isReceivingPower = false;

		foreach (var b in _connectedBricks)
		{
			Emitter otherEmitter = b.GetComponent<Emitter>();

			// if brick is colliding with another conductive brick which is electrically charged, then conducts electricity
			if (otherEmitter != null && otherEmitter.isEmittingPower)
			{
				isReceivingPower = true;
			}
		}
	}
}
