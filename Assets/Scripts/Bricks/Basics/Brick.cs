using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Brick : MonoBehaviour
{
	protected e_BrickType type;
	public e_BrickType Type { get => type; }

	private static float detachingForce = 400;
	[SerializeField] public GameObject conductiveWiresGO;
	[HideInInspector] public Wires wires;

	virtual protected void Start()
	{
		// Instantiate Conductive Wires Object and store its script
		GameObject temp = Instantiate(conductiveWiresGO, transform);
		wires = temp.GetComponent<Wires>();

		type = e_BrickType.Neutral;
	}

	virtual public void Delete(Vehicle _parentVehicle)
	{
		Destroy(gameObject);

		if (_parentVehicle)
		{
			// clear brick not linked to vehicle anymore because of this brick deletion
			_parentVehicle.AskForClear();

			// remove it from vehicle control
			if (this is IControllable)
			{
				_parentVehicle.controllables.Remove(this as IControllable);
			}
		}

	}

	virtual public void Detach()
	{
		// in play mode -> detach brick physically
		if (TheGameManager.GameMode == e_GameMode.Play)
		{
			transform.SetParent(null);

			Rigidbody tempRb = transform.GetComponent<Rigidbody>();
			tempRb.useGravity = true;
			tempRb.isKinematic = false;

			// bump it away in random direction
			Vector3 randDir = Vector3.up * Random.Range(-1f, 1f)
							+ Vector3.forward * Random.Range(-1f, 1f)
							+ Vector3.right * Random.Range(-1f, 1f);
			tempRb.AddForce(randDir * detachingForce);
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