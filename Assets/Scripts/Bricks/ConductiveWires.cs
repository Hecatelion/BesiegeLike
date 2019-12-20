using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductiveWires : MonoBehaviour
{
	public delegate void TriggerDelegate(Collider _other);
	public TriggerDelegate ProcessOnTriggerEnter = (Collider col) => { };

	BoxCollider[] wiresColliders;
	List<Collider> otherColliders;
	int layerMask;

	// Start is called before the first frame update
	void Start()
    {
		wiresColliders = GetComponents<BoxCollider>();
		otherColliders = new List<Collider>();
		layerMask = 1 << 8;
	}

    // Update is called once per frame
    void Update()
    {
		
		// for each wire (the 3 box colliders)
		foreach (var wCol in wiresColliders)
		{
			// get every object collided and add them to otherColliders list
			foreach (var c in Physics.OverlapBox(wCol.center, wCol.size / 2.0f, Quaternion.identity, layerMask))
			{
				otherColliders.Add(c);
			} 
		}

		if (otherColliders.Count > 0)
		{
			foreach (var c in otherColliders)
			{
				// test connectivity
				ProcessOnTriggerEnter(c);

			}

			// clear list for next tick
			otherColliders.Clear();
		}
    }

	/*private void OnTriggerEnter(Collider _other)
	{
		ProcessOnTriggerEnter(_other);
	}*/
}
