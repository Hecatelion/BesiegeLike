using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : MonoBehaviour
{
	public delegate void TriggerDelegate(Collider _other);
	public TriggerDelegate ProcessTriggering = (Collider col) => { };

	BoxCollider[] wiresColliders;
	public List<Collider> connectedColliders;
	int layerMask;

	// Start is called before the first frame update
	void Start()
	{
		wiresColliders = GetComponents<BoxCollider>();
		connectedColliders = new List<Collider>();

		// "Bricks" physical layer
		layerMask = 1 << 8;
	}

	// Update is called once per frame
	void Update()
	{
		FindConnectedColliders();
	}

	public void FindConnectedColliders()
	{
		connectedColliders.Clear();

		// for each wire (the 3 box colliders)
		foreach (var wCol in wiresColliders)
		{
			// get every object collided and add them to otherColliders list
			foreach (var c in Physics.OverlapBox(transform.position, wCol.size / 2.0f * wCol.transform.lossyScale.x, Quaternion.identity, layerMask))
			{
				// ignore parent brick's collider
				if (c != transform.parent.GetComponent<Collider>())
				{
					connectedColliders.Add(c);
				}
			}
		}
	}

	void ONDrawGizmos()
	{
		// for each wire (the 3 box colliders)
		foreach (var wCol in wiresColliders)
		{
			Gizmos.DrawWireCube(transform.position, wCol.size * wCol.transform.lossyScale.x);
		}
	}
}
