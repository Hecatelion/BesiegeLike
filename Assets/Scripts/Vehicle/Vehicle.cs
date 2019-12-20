using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	public List<ReactorBrick> reactors;
	Rigidbody rb;

	void Start()
    {
		reactors = new List<ReactorBrick>();
		rb = GetComponent<Rigidbody>();
	}

    void Update()
    { }

	public void GazOn()
	{
		foreach (var r in reactors)
		{
			r.Use();
		}
	}
}
