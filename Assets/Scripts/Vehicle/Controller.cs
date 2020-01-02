using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Vehicle vehicle;

	// forbidden keys
	[SerializeField]
	public static List<KeyCode> forbiddenKeys; // go back to array (init is aweful)

	// Start is called before the first frame update
	void Start()
    {
		forbiddenKeys = new List<KeyCode>();
		forbiddenKeys.Add(KeyCode.Escape);
		forbiddenKeys.Add(KeyCode.LeftAlt);
	}

    // Update is called once per frame
    void Update()
    {

	}

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
