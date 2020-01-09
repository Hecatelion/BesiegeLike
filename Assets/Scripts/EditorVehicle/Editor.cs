using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
	[SerializeField] Camera cameraEditor;
	Camera usedCamera;

	[Header("Vehicle")]
	[SerializeField] InputField nameField;
	[SerializeField] Vehicle vehicle;
	[SerializeField] GameObject emptyVehicleGO;

	[Header("Bricks Prefabs")]
	[SerializeField] GameObject neutralBrickGO;
	[SerializeField] GameObject reactorBrickGO;
	[SerializeField] GameObject circuitBrickGO;
	[SerializeField] GameObject switchBrickGO;

	public e_BrickType currentBrickType;
	int layerBrick = 0;

	[Header("PlayMenu")]
	[SerializeField] GameObject playButtonGO;

    void Start()
    {
		currentBrickType = e_BrickType.Neutral;
		layerBrick = LayerMask.GetMask("Bricks");
		usedCamera = cameraEditor;

		// while there isn't any vehicle, player can't play the leve
		playButtonGO.SetActive(false);
	}

    void Update()
    {
		if (vehicle && !Input.GetKey(KeyCode.LeftAlt))
		{
			// left click -> brick instantiation
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;

				if (LookForBrick(out hit))
				{
					AddBrickOnHit(hit);
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

	// ------------------------------------------
	//					vehicle 
	// ------------------------------------------

	void CreateNewVehicle()
	{
		// destroy current vehicle
		if (vehicle) Destroy(vehicle.gameObject);

		// create the new one
		GameObject vehicleToCreate = emptyVehicleGO;
		vehicle = Instantiate(vehicleToCreate).GetComponent<Vehicle>();

		// use created vehicle camera
		SwitchCamera(vehicle.transform.GetComponentInChildren<Camera>());

		// allow player to play level
		if (TheGameManager.NextLevel != "None")
		{
			playButtonGO.SetActive(true);
		}
	}

	// from JsonableVehicle recreate vehicle (using brick instantiation)
	IEnumerator AddBricksFromData(VehicleData _vehicleData)
	{
		yield return new WaitForSeconds(0.1f);

		foreach (var brickData in _vehicleData.classicBricksDatas)
		{
			switch (brickData.type)
			{
				case e_BrickType.Core:
					// nothing 
					break;
				case e_BrickType.Neutral:
					AddNeutralBrick(brickData.pos);
					break;
				case e_BrickType.Circuit:
					AddCircuitBrick(brickData.pos);
					break;

				case e_BrickType.None:
				default:
					break;
			}

			yield return new WaitForSeconds(0.05f);
		}

		foreach (ReactorBrickData brickData in _vehicleData.reactorBricksDatas)
		{
			AddReactorBrick(brickData.pos, brickData.dir);

			yield return new WaitForSeconds(0.05f);
		}

		foreach (SwitchBrickData brickData in _vehicleData.switchBricksDatas)
		{
			AddSwitchBrick(brickData.pos, brickData.keyBound);

			yield return new WaitForSeconds(0.05f);
		}

		yield return null;
	}
	void CreateNewVehicle(VehicleData _vehicleData)
	{
		CreateNewVehicle();

		// need to delay a bit brick instantiation to let empty vehicle being instantiated for real at the end of the current frame
		StartCoroutine(AddBricksFromData(_vehicleData));
	}

	// ------------------------------------------
	//				brick detection
	// ------------------------------------------

	// raycasting to find a brick and return if succeed
	bool LookForBrick(out RaycastHit _hit)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out _hit, float.MaxValue, layerBrick); // using layerBrick to ignore wires' colliders

		return _hit.transform && _hit.transform.GetComponent<Brick>() != null 
			&& vehicle.bricks.Contains(_hit.transform.GetComponent<Brick>());
	}

	// ------------------------------------------
	//			bricks instantiations
	// ------------------------------------------

	GameObject InstantiateBrick(GameObject _objectToInstanciate, Vehicle _vehicle, Vector3 _pos)
	{
		// scene instantiation settings
		GameObject tempBrick = Instantiate(_objectToInstanciate, _vehicle.transform);
		tempBrick.transform.position = _pos;

		// link vehicle and brick
		Brick createdBrick = tempBrick.GetComponent<Brick>();
		_vehicle.bricks.Add(createdBrick);

		return tempBrick;
	}

	void AddBrickOnHit(RaycastHit _hit)
	{
		if (vehicle.bricks.Contains(_hit.transform.GetComponent<Brick>()))
		{
			Vector3 pos = _hit.transform.position + _hit.normal * 0.5f;

			switch (currentBrickType)
			{
				case e_BrickType.Neutral:	AddNeutralBrick(pos);					break;
				case e_BrickType.Reactor:	AddReactorBrick(pos, -_hit.normal);		break;
				case e_BrickType.Circuit:	AddCircuitBrick(pos);					break;
				case e_BrickType.Switch:	AddSwitchBrick(pos, KeyCode.Space);		break;
			}
		}
	}

	void AddNeutralBrick(Vector3 _pos)
	{
		// Instantiate a neutral brick, ignoring the returned variable
		_ = InstantiateBrick(neutralBrickGO, vehicle, _pos);
	}

	void AddReactorBrick(Vector3 _pos, Vector3 _dir)
	{
		// Instantiate a reactor brick
		GameObject tempBrick = InstantiateBrick(reactorBrickGO, vehicle, _pos);

		ReactorBrick reactorBrick = tempBrick.GetComponent<ReactorBrick>();

		// make reactor's muzzle point at the opposite of the brick it is built on
		reactorBrick.Direction = _dir;

		// link reactors to vehicle, in order to make reactors move vehicle
		reactorBrick.SetVehicle(vehicle.GetComponent<Rigidbody>());
	}

	void AddCircuitBrick(Vector3 _pos)
	{
		_ = InstantiateBrick(circuitBrickGO, vehicle, _pos);
	}

	void AddSwitchBrick(Vector3 _pos, KeyCode _keyBound)
	{
		// instantiate a switch brick
		GameObject createdSwitchBrickGO = InstantiateBrick(switchBrickGO, vehicle, _pos);

		// link it to vehicle and vice-verse
		vehicle.GetComponent<Vehicle>().controllables.Add(createdSwitchBrickGO.GetComponent<IControllable>());
		createdSwitchBrickGO.GetComponent<SwitchBrick>().linkedVehicle = vehicle;

		// init its bind
		createdSwitchBrickGO.GetComponent<SwitchBrick>().Bind(_keyBound);
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
	//					Camera
	// ------------------------------------------

	void SwitchCamera(Camera _newCameraUsed)
	{
		usedCamera.enabled = false;
		usedCamera = _newCameraUsed;
		usedCamera.enabled = true;
	}

	// ------------------------------------------
	//					UI 
	// ------------------------------------------

	public void UI_SelectThisBrickType(UIButtonBrickType _script)
	{
		currentBrickType = _script.brickType;
	}

	public void UI_Back()
	{
		// trash vehicle
		SwitchCamera(cameraEditor);
		if(vehicle) Destroy(vehicle.gameObject);

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

	public void UI_NewVehicle()
	{
		CreateNewVehicle();
	}

	public void UI_Save()
	{
		TheSaveManager.SaveVehicle(vehicle, nameField.text);
	}

	public void UI_Load()
	{
		CreateNewVehicle(TheSaveManager.LoadVehicle(nameField.text));
	}
}
