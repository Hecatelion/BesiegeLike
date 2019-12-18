using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Vehicule vehicule;
	Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Space))
		{
			foreach (ReactorBrick reactor in vehicule.reactors)
			{
				// add force on point
			}
			rb.AddForce(transform.forward * 10);
		}
    }
}
