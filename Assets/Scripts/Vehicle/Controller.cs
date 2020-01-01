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
    {
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			vehicle.PerformAction(0);
		}
    }
}
