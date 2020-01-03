using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// formed of bricks
// physics is applyed on it through movement bricks (e.g. ReactorBrick(s))
public class Vehicle : MonoBehaviour
{
	Rigidbody rb;

	// will be possessed automatically by a controller if one is available
	[SerializeField] public bool shouldBeAutoPossessed = true;

	// vehicle bricks
	[Header("Vehicle Bricks")]
	[SerializeField] CoreBrick core;
	public List<Brick> bricks;
	int framesBeforeClearing = -1; // used to delay clear after brick deletion, objects being destroyed at the end of the frame

	// bind utils
	public List<IControllable> controllables;
	List<Action> actions;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		// init vehicle bricks
		bricks = new List<Brick>();
		bricks.Add(core);

		// init bind utils 
		controllables = new List<IControllable>();
		actions = new List<Action>();

		// temp solution -> should be replaced by save/load system
		// -> VehicleBank.Save(Editor.vehicle);
		// -> level.vehicle = VehicleBank.Load();
		DontDestroyOnLoad(gameObject);
	}

    void Update()
    {
		// brick clearing
		// if there are frames left, then decrement
		if (framesBeforeClearing > 0)
		{
			framesBeforeClearing--;
		}
		// if timer ends, then call clearing function and disactivate frames counter
		else if (framesBeforeClearing == 0)
		{
			ClearNotLinkedBricks();
			framesBeforeClearing = -1;
		}
	}

	public void BindControllables()
	{
		// remove deleted controllable bricks for controllables
		//controllables.RemoveNullItems(); -> dosent works because interface not castable in object :)))
		controllables.RemoveAll(item => item == null);

		// get all bound key
		List<KeyCode> boundKeys = (from controllable in controllables select controllable.GetBoundKey()).Distinct().ToList();

		// create one action for each bound key, and bind corresponding controllables
		foreach (var key in boundKeys)
		{
			actions.Add(new Action(key, (from controllable in controllables where controllable.GetBoundKey() == key select controllable).ToList()));
		}
	}

	public void PerformAction(KeyCode _keyBound)
	{
		// on any key press, perform action bound to this key, if none, discard
		Action actionBound = actions.FirstOrDefault(action => action.keyBound == _keyBound);
		
		if (actionBound != default && actionBound != null)
		{
			actionBound.Perform();
		}
	}

	// activate frames counter
	public void AskForClear()
	{
		framesBeforeClearing = 1;
	}

	// from core, finds all linked brick, and take others out of vehicle
	private void ClearNotLinkedBricks()
	{
		// remove destroyed bricks from vehicle.bricks
		bricks.RemoveNullItems();

		// get first connected bricks
		//List<Brick> linkedBricks = core.GetConnectedBricks();
		List<Brick> linkedBricks = new List<Brick>();
		linkedBricks.Add(core);
		Brick testedBrick = null;
		int j = 0;

		// foreach (var testedBrick in linkedBricks) 
		// (while(){} is used instead of classic foreach because it allows to add items to circuitsToTest dynamically where foreach would crash)
		while (j < linkedBricks.Count)
		{
			testedBrick = linkedBricks[j];

			// get bricks connected to the tested one and which aren't stored in linkedBricks yet 
			foreach (var connectedBrick in testedBrick.GetConnectedBricks())
			{
				if (!linkedBricks.Contains(connectedBrick))
				{
					linkedBricks.Add(connectedBrick);
				}
			}

			++j;
		}

		// find bricks which are in vehicle but not linked to core (those which need to be clear)
		List<Brick> bricksToClear = (from vehicleBrick in this.bricks
									where (!linkedBricks.Any(b => b == vehicleBrick)) // vehicle brick which is not contained in linkedBricks
									select vehicleBrick).ToList();

		foreach (var btc in bricksToClear)
		{
			btc.Detach();
		}

		bricks = linkedBricks;
	}

	public void Explode()
	{
		foreach (var brick in bricks)
		{
			brick.Detach();
		}

		rb.velocity = Vector3.zero;
		//Destroy(rb);
	}
}
