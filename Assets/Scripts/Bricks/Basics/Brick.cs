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

	virtual public void Delete()
	{
		Destroy(gameObject);
	}

	virtual public void Detach(bool _playingMode)
	{
		// in play mode -> detach brick physically
		if (_playingMode)
		{
			transform.SetParent(null);

			Rigidbody tempRb = transform.GetComponent<Rigidbody>();
			tempRb.useGravity = true;
			tempRb.isKinematic = false;

			// bump it away
			tempRb.AddForce(Vector3.up * Random.Range(-500, 500)
							+ Vector3.forward * Random.Range(-500, 500)
							+ Vector3.right * Random.Range(-500, 500));
		}
		// in editor -> destroy brick
		else
		{
			Destroy(gameObject);
		}
	}

	public List<Brick> GetConnectedBricks()
	{
		List<Brick> connectedBricks = new List<Brick>();

		// find bricks collisioning with wires' colliders
		foreach (var col in wires.connectedColliders)
		{
			if (col) // prevent "null reference exception" when brick is deleted
			{ 
				Brick tempBrick = col.GetComponent<Brick>();
				if (tempBrick != null)
				{
					connectedBricks.Add(tempBrick);
				}
			}
		}

		return connectedBricks;
	}

	public List<T> GetConnected<T>() where T : Brick
	{
		return (from brick in GetConnectedBricks()
				where brick is T
				select brick).ToList().Cast<T>().ToList();
	}
}