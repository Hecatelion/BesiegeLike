using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBrick : Receiver
{
	Rigidbody rbVehicle;
	[SerializeField] float power = 10.0f;

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
    {
		base.Update();

		if (isReceivingPower)
		{
			Use();
		}
	}

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
