using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public Vehicle vehicle;

	// Start is called before the first frame update
	void Start()
    {
		AutoPossess();
	}

    // Update is called once per frame
    void Update()
    { }

	private void OnGUI()
	{
		if (vehicle)
		{
			// on any key press, perform action bound to this key, if none, discard
			Event e = Event.current;

			if (e.isKey && Input.anyKeyDown)
			{
				vehicle.PerformAction(e.keyCode);
			}
		}
	}

	// find a vehicle in scene that should be autopossessed (e.g. player vehicle)
	public void AutoPossess()
	{
		Vehicle[] vehicles = FindObjectsOfType<Vehicle>();

		foreach (var v in vehicles)
		{
			if (v.shouldBeAutoPossessed)
			{
				vehicle = v;
				return;
			}
		}
	}
}
