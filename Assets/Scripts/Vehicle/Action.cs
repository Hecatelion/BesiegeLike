using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// is performed by Vehicle and Controller
// manage what should be done when one controlle key is pressed during game mode
// e.g. when press key 1 -> actions[1].Perform();		c.f. Vehicle.cs and Controller.cs
public class Action
{
	internal int keyBound = 0; // should become real 
	private List<IControllable> controllables;

	public Action(int _keyBound = 0, List<IControllable> _controllables = null)
	{
		keyBound = _keyBound;
		controllables = _controllables ?? new List<IControllable>();
	}

	public void SetControllables(List<IControllable> _controllables)
	{
		controllables = _controllables;
	}

	public void Perform()
	{
		foreach (var controllable in controllables)
		{
			controllable.Use();
		}
	}
}