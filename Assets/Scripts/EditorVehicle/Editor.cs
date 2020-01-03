using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Editor : MonoBehaviour
{
	[SerializeField] Vehicle vehicle;

	[Header("Bricks Prefabs")]
	[SerializeField] GameObject neutralBrickGO;
	[SerializeField] GameObject reactorBrickGO;
	[SerializeField] GameObject circuitBrickGO;
	[SerializeField] GameObject switchBrickGO;

	public BrickType currentBrickType;
	int layerBrick = 0;

	[Header("PlayMenu")]
	[SerializeField] GameObject playButtonGO;

    void Start()
    {
		currentBrickType = BrickType.Neutral;
		layerBrick = LayerMask.GetMask("Bricks");

		if (TheGameManager.NextLevel == "None")
		{
			playButtonGO.SetActive(false);
		}
	}

    void Update()
    {
		if (!Input.GetKey(KeyCode.LeftAlt))
		{
			// left click -> brick instantiation
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;

				if (LookForBrick(out hit))
				{
					AddBrick(hit);
				}
			}

			// right click -> brick interaction
			if (Input.GetMouseButtonDown(1))
			{
				RaycastHit hit;

				if (LookForBrick(out hit))
				{
					InteractWithBrick(hit);
				}
			}

			// mouse wheel click -> brick deletion
			if (Input.GetMouseButtonDown(2))
			{
				RaycastHit hit;

				if (LookForBrick(out hit))
				{
					DeleteBrick(hit);
				}
			}
		}
    }

	// raycasting to find a brick and return if succeed
	bool LookForBrick(out RaycastHit _hit)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out _hit, float.MaxValue, layerBrick); // using layerBrick to ignore wires' colliders

		//Brick brickHit = _hit.transform.GetComponent<Brick>() ?? null;

		return _hit.transform && _hit.transform.GetComponent<Brick>() != null && vehicle.bricks.Contains(_hit.transform.GetComponent<Brick>());
	}

	// ------------------------------------------
	//			bricks instantiations
	// ------------------------------------------

	void AddBrick(RaycastHit _hit)
	{
		if (vehicle.bricks.Contains(_hit.transform.GetComponent<Brick>()))
		{
			switch (currentBrickType)
			{
				case BrickType.Neutral: AddNeutralBrick(_hit); break;
				case BrickType.Reactor: AddReactorBrick(_hit); break;
				case BrickType.Circuit: AddCircuitBrick(_hit); break;
				case BrickType.Switch:	AddSwitchBrick(_hit); break;
			}
		}
	}

	void AddNeutralBrick(RaycastHit _hit)
	{
		// Instantiate a neutral brick, ignoring the returned variable
		_ = InstantiateBrick(neutralBrickGO, vehicle, _hit);
	}

	void AddReactorBrick(RaycastHit _hit)
	{
		// Instantiate a reactor brick
		GameObject tempBrick = InstantiateBrick(reactorBrickGO, vehicle, _hit);

		ReactorBrick reactorBrick = tempBrick.GetComponent<ReactorBrick>();

		// make reactor's muzzle point at the opposite of the brick it is built on
		reactorBrick.SetDirection(-_hit.normal);

		// link reactors to vehicle, in order to make reactors move vehicle
		reactorBrick.SetVehicle(vehicle.GetComponent<Rigidbody>());
	}

	void AddCircuitBrick(RaycastHit _hit)
	{
		_ = InstantiateBrick(circuitBrickGO, vehicle, _hit);
	}

	void AddSwitchBrick(RaycastHit _hit)
	{
		// Instantiate a switch brick and link it to vehicle
		IControllable controllable = InstantiateBrick(switchBrickGO, vehicle, _hit).GetComponent<IControllable>();
		vehicle.GetComponent<Vehicle>().controllables.Add(controllable);
	}

	GameObject InstantiateBrick(GameObject _objectToInstanciate, Vehicle _vehicle, RaycastHit _hit)
	{
		// scene instantiation settings
		GameObject tempBrick = Instantiate(_objectToInstanciate, _vehicle.transform);
		tempBrick.transform.position = _hit.transform.position + _hit.normal * 0.5f;

		// link vehicle and brick
		Brick createdBrick = tempBrick.GetComponent<Brick>();
		_vehicle.bricks.Add(createdBrick);

		return tempBrick;
	}

	// ------------------------------------------
	// bricks interactions
	// ------------------------------------------

	void InteractWithBrick(RaycastHit _hit)
	{
		IInteractible interactible = _hit.transform.GetComponent<IInteractible>();
		
		if (interactible != null)
		{
			interactible.Interact(_hit);
		}
	}

	void DeleteBrick(RaycastHit _hit)
	{
		Brick brickHit = _hit.transform.GetComponent<Brick>();
		if (brickHit)
		{
			brickHit.Delete(vehicle);
		}
	}

	// ------------------------------------------
	// UI Brick type selection
	// ------------------------------------------

	public void UI_SelectThisBrickType(UIButtonBrickType _script)
	{
		currentBrickType = _script.brickType;
	}

	public void UI_Back()
	{
		Destroy(vehicle.gameObject);

		string nextLevel = TheGameManager.NextLevel;
		
		// if editor phase before levels, go back to LevelSelection
		if (nextLevel.Contains("Level"))
		{
			TheCustomSceneManager.LoadScene_SetNextLevel("LevelSelection");
		}
		// if classical editor, go back to main menu
		else
		{
			TheCustomSceneManager.LoadScene_SetNextLevel("Menu");
		}
	}

	public void UI_Play()
	{
		vehicle.BindControllables();
		TheCustomSceneManager.LoadScene_SetNextLevel(TheGameManager.NextLevel);
	}
}
