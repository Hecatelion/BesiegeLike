using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Brick : MonoBehaviour
{
	[HideInInspector] public GameObject conductiveWiresGO;
	[HideInInspector] public Wires wires;

	virtual protected void Start()
	{
		// find wire GO
		conductiveWiresGO = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bricks/Wires.prefab", typeof(GameObject));

		// Instantiate Conductive Wires Object and store its script
		GameObject temp = Instantiate(conductiveWiresGO, transform);
		wires = temp.GetComponent<Wires>();
	}

	public List<Brick> GetConnectedBricks()
	{
		List<Brick> connectedBricks = new List<Brick>();

		foreach (var col in wires.connectedColliders)
		{
			Brick tempBrick = col.GetComponent<Brick>();
			if (tempBrick != null)
			{
				connectedBricks.Add(tempBrick);
			}
		}

		return connectedBricks;
	}

	public List<CircuitBrick> GetConnectedCircuits()
	{
		return (from brick in GetConnectedBricks()
				where brick is CircuitBrick
				select brick).ToList().Cast<CircuitBrick>().ToList();
	}
}