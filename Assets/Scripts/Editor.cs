using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
	[SerializeField] GameObject vehiculeGO;
	[SerializeField] GameObject neutralBrickGO;
	[SerializeField] GameObject reactorBrickGO;
	[SerializeField] GameObject circuitBrickGO;

	public BrickTypes currentBrickType;

    void Start()
    {
		currentBrickType = BrickTypes.Neutral;
    }

    void Update()
    {
		if (!Input.GetKey(KeyCode.LeftAlt))
		{
			if (Input.GetMouseButtonDown(0))
			{
				AddBrick(); 
			}

			if (Input.GetMouseButtonDown(1))
			{
				InteractWithBrick();
			}
		}
    }

	void AddBrick()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		int layer = 1 << 8;
		Physics.Raycast(ray, out hit, Mathf.Infinity, layer);

		//Debug.Log(hit.transform.gameObject.name);

		if (hit.transform && hit.transform.tag == "Brick")
		{

			switch (currentBrickType)
			{
				case BrickTypes.Neutral:	AddNeutralBrick(hit);	break;
				case BrickTypes.Reactor:    AddReactorBrick(hit);	break;
				case BrickTypes.Circuit:    AddCircuitBrick(hit);	break;
			}
		}
		else
		{
			Debug.Log("no brick found");
		}
	}

	void AddNeutralBrick(RaycastHit _hit)
	{
		// Instantiate a neutral brick, ignoring variable the returned
		_ = InstantiateBrick(neutralBrickGO, vehiculeGO.transform, _hit);
	}

	void AddReactorBrick(RaycastHit _hit)
	{
		// Instantiate a reactor brick
		GameObject tempBrick = InstantiateBrick(reactorBrickGO, vehiculeGO.transform, _hit);

		ReactorBrick reactorBrick = tempBrick.GetComponent<ReactorBrick>();

		// make reactor's muzzle point at the opposite of the brick it is built on
		reactorBrick.SetDirection(-_hit.normal);

		// link reactors to vehicle, in order to make reactors move vehicle
		reactorBrick.SetVehicle(vehiculeGO.GetComponent<Rigidbody>());
		vehiculeGO.GetComponent<Vehicle>().reactors.Add(reactorBrick);
	}

	void AddCircuitBrick(RaycastHit _hit)
	{
		_ = InstantiateBrick(circuitBrickGO, vehiculeGO.transform, _hit);
	}

	GameObject InstantiateBrick(GameObject _objectToInstanciate, Transform _parent, RaycastHit _hit)
	{
		GameObject tempBrick = Instantiate(_objectToInstanciate, _parent);
		tempBrick.transform.position = _hit.transform.position + _hit.normal * 0.5f;

		return tempBrick;
 }

	void InteractWithBrick()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(ray, out hit);

		if (hit.transform)
		{
			hit.transform.GetComponent<ReactorBrick>().SetDirection(-hit.normal); 
		}
	}

	// UI Brick type selection
	public void SelectNeutralBrick()
	{
		currentBrickType = BrickTypes.Neutral;
	}
	public void SelectReactorBrick()
	{
		currentBrickType = BrickTypes.Reactor;
	}
	public void SelectCircuitBrick()
	{
		currentBrickType = BrickTypes.Circuit;
	}
}
