using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// formed of bricks
// physics is applyed on it through movement bricks (e.g. ReactorBrick(s))
public class Vehicle : MonoBehaviour
{

	Rigidbody rb;

	// has references on controllable bricks
	public List<IControllable> controllables;
	List<Action> actions;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		controllables = new List<IControllable>();
		actions = new List<Action>();
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Bind!");
			BindControllables(); // should be done when exiting editor mode / starting game mode
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
	}

	public void PerformAction(KeyCode _keyBound)
	{
		// on any key press, perform action bound to this key, if none, discard
		Action actionBound = actions.FirstOrDefault(action => action.keyBound == _keyBound);
		
		if(actionBound != default && actionBound != null)
		{
			actionBound.Perform();
		}
	}
}
