using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Vehicle vehicle;


	// Start is called before the first frame update
	void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

	private void OnGUI()
	{
		// on any key press, perform action bound to this key, if none, discard
		Event e = Event.current;

		if (e.isKey && Input.anyKeyDown)
		{
			vehicle.PerformAction(e.keyCode);
		}
	}
}
