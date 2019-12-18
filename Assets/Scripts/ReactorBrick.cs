using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBrick : MonoBehaviour
{
	Rigidbody rbVehicule;

	void Start()
	{ }
	
    void Update()
    { }

	// direction in which the ship will go
	public void SetDirection(Vector3 _dir)
	{
		transform.forward = _dir;
	}

	public void SetVehicule(Rigidbody _rb)
	{
		rbVehicule = _rb;
	}
}
