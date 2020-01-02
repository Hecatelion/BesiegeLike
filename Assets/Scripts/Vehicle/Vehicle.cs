﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// formed of bricks
// physics is applyed on it through movement bricks (e.g. ReactorBrick(s))
public class Vehicle : MonoBehaviour
{
	Rigidbody rb;

	// vehicle bricks
	[SerializeField] CoreBrick core;
	public List<Brick> bricks;
	public List<IControllable> controllables;
	List<Action> actions;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		bricks = new List<Brick>();
		bricks.Add(core);

		controllables = new List<IControllable>();
		actions = new List<Action>();
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			BindControllables(); // should be done when exiting editor mode / starting game mode
		}
		else if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			ClearNotLinkedBricks(true);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			ClearNotLinkedBricks(false);
		}
	}

	void BindControllables()
	{
		// get all bound key
		List<KeyCode> boundKeys = (from controllable in controllables select controllable.GetBoundKey()).Distinct().ToList();

		// create one action for each bound key, and bind corresponding controllables
		foreach (var key in boundKeys)
		{
			actions.Add(new Action(key, (from controllable in controllables where controllable.GetBoundKey() == key select controllable).ToList()));
		}

		Debug.Log("Bind succeeded");
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

	public void ClearNotLinkedBricks(bool _playingMode)
	{
		// remove destroyed bricks from vehicle.bricks
		for (int i = 0; i < bricks.Count; ++i)
		{
			if (!bricks[i])
			{
				bricks.RemoveAt(i);

				// decremente to test next index (which has changed because one item has been removed)
				--i;
			}
		}

		// from core, finds all linked brick, and take others out of vehicle

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
			btc.Detach(_playingMode);
		}

		bricks = linkedBricks;
	}
}
