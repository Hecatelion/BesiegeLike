using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBrick : Receiver, IInteractible
{
	Rigidbody rbVehicle;
	[SerializeField] float power = 10.0f;

	public Vector3 Direction
	{
		get => transform.forward;
		set => transform.forward = value;
	}

	protected override void Start()
	{
		base.Start();
		type = e_BrickType.Reactor;
	}

	protected override void Update()
    {
		base.Update();

		if (isReceivingPower)
		{
			Use();
		}
	}

	public void SetVehicle(Rigidbody _rb)
	{
		rbVehicle = _rb;
	}

	public void Use()
	{
		rbVehicle.AddForceAtPosition(transform.forward * power, transform.position);
	}

	// IInteraactible interface implementation
	public void Interact(RaycastHit _hit)
	{
		Direction = -_hit.normal;
	}
}
