using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAroundPoint : MonoBehaviour
{
	[SerializeField] Transform target;
	public float sensitivity = 100.0f;
	float distance = 5.0f;

    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		Vector3 v = target.position - transform.position;
		if (v.magnitude > distance)
		{
			transform.position += v.normalized * (v.magnitude - distance);
		}

		// orbital camera
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
		{
			transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity);
			transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity);
		}
	}
}
