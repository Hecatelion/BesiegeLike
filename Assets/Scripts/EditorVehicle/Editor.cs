using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
	[SerializeField] GameObject vehiculeGO;
	[SerializeField] GameObject neutralBrickGO;
	[SerializeField] GameObject reactorBrickGO;
	[SerializeField] GameObject circuitBrickGO;
	[SerializeField] GameObject switchBrickGO;

	public BrickType currentBrickType;
	int layerBrick = 0;

    void Start()
    {
		currentBrickType = BrickType.Neutral;
		layerBrick = LayerMask.GetMask("Bricks");
    }

    void Update()
    {
		if (!Input.GetKey(KeyCode.LeftAlt))
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;

				if (LookForBrick(out hit))
				{
					AddBrick(hit);
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				RaycastHit hit;

				if (LookForBrick(out hit))
				{
					InteractWithBrick(hit);
				}
			}
		}
    }

	// raycasting to find a brick and return if succeed
	bool LookForBrick(out RaycastHit _hit)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out _hit, float.MaxValue, layerBrick); // using layerBrick to ignore wires' colliders

		return _hit.transform && _hit.transform.GetComponent<Brick>() != null;
	}

	// bricks instantiations
	void AddBrick(RaycastHit _hit)
	{
		switch (currentBrickType)
		{
			case BrickType.Neutral:		AddNeutralBrick(_hit);		break;
			case BrickType.Reactor:		AddReactorBrick(_hit);		break;
			case BrickType.Circuit:		AddCircuitBrick(_hit);		break;
			case BrickType.Switch:		AddSwitchBrick(_hit);		break;
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
	}

	void AddCircuitBrick(RaycastHit _hit)
	{
		_ = InstantiateBrick(circuitBrickGO, vehiculeGO.transform, _hit);
	}

	void AddSwitchBrick(RaycastHit _hit)
	{
		// Instantiate a switch brick and link it to vehicle
		IControllable controllable = InstantiateBrick(switchBrickGO, vehiculeGO.transform, _hit).GetComponent<IControllable>();
		vehiculeGO.GetComponent<Vehicle>().controllables.Add(controllable);
	}

	GameObject InstantiateBrick(GameObject _objectToInstanciate, Transform _parent, RaycastHit _hit)
	{
		GameObject tempBrick = Instantiate(_objectToInstanciate, _parent);
		tempBrick.transform.position = _hit.transform.position + _hit.normal * 0.5f;

		return tempBrick;
	}

	// bricks interactions
	void InteractWithBrick(RaycastHit _hit)
	{
		IInteractible interactible = _hit.transform.GetComponent<IInteractible>();
		
		if (interactible != null)
		{
			interactible.Interact(_hit);
		}
	}

	// UI Brick type selection
	public void UI_SelectThisBrickType(UIButtonBrickType _script)
	{
		currentBrickType = _script.brickType;
	}
}
