using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Receiver : MonoBehaviour
{
	public GameObject conductiveWiresGO;
	public bool isReceivingPower = false;

	ConductiveWires wires;

	private void Start()
	{
		// find wire GO
		conductiveWiresGO = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bricks/ConductiveWires.prefab", typeof(GameObject));

		// Instantiate Conductive Wires Object and store its script
		GameObject temp = Instantiate(conductiveWiresGO, transform);
		wires = temp.GetComponent<ConductiveWires>();

		// test connectivity when Conductive Wires are colliding with another brick
		wires.ProcessTriggering += TestConductivity;
	}

	public void TestConductivity(Collider _other)
	{
		Emitter otherEmitter = _other.GetComponent<Emitter>();

		// if brick is colliding with another conductive brick which is electrically charged, then conducts electricity
		if (otherEmitter != null && otherEmitter.isEmittingPower)
		{
			isReceivingPower = true;
		}
	}
}
