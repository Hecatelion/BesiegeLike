using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBrick : MonoBehaviour
{
	Rigidbody rbVehicle;
	[SerializeField] float power = 10.0f;

	void Start()
	{ }
	
    void Update()
    { }

	// direction in which the ship will go
	public void SetDirection(Vector3 _dir)
	{
		transform.forward = _dir;
	}

	public void SetVehicle(Rigidbody _rb)
	{
		rbVehicle = _rb;
	}

	public void Use()
	{
		rbVehicle.AddForceAtPosition(transform.forward * power, transform.position);
	}
}
