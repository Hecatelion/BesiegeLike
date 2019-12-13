﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
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
			rb.AddForce(transform.forward * 10);
		}
    }
}
