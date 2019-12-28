using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductiveWires : MonoBehaviour
{
	public delegate void TriggerDelegate(Collider _other);
	public TriggerDelegate ProcessTriggering = (Collider col) => { };

	BoxCollider[] wiresColliders;
	List<Collider> otherColliders;
	int layerMask;

	// Start is called before the first frame update
	void Start()
	{
		wiresColliders = GetComponents<BoxCollider>();
		otherColliders = new List<Collider>();

		// "Bricks" physical layer
		layerMask = 1 << 8;
	}

	// Update is called once per frame
	void Update()
	{
		// for each wire (the 3 box colliders)
		foreach (var wCol in wiresColliders)
		{
			// get every object collided and add them to otherColliders list
			foreach (var c in Physics.OverlapBox(transform.position, wCol.size / 2.0f * wCol.transform.lossyScale.x, Quaternion.identity, layerMask))
			{
				otherColliders.Add(c);
			}
		}

		if (otherColliders.Count > 0)
		{
			foreach (var c in otherColliders)
			{
				// if collide with something else than parent
				if (c.transform != transform.parent)
				{
					// call trigger delegate
					ProcessTriggering(c);
				}
			}

			// clear list for next tick
			otherColliders.Clear();
		}
		else
		{

		}
	}

	void OnDrawGizmos()
	{
		// for each wire (the 3 box colliders)
		foreach (var wCol in wiresColliders)
		{
			Gizmos.DrawWireCube(transform.position, wCol.size * wCol.transform.lossyScale.x);
		}
	}
}
