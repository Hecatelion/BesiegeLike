using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
	[SerializeField] GameObject vehicule;
	[SerializeField] GameObject brick;

    void Start()
    {
        
    }

    void Update()
    {
		if (!Input.GetKey(KeyCode.LeftAlt))
		{
			if (Input.GetMouseButtonDown(0))
			{
				AddBrick();
			}

			if (Input.GetMouseButtonDown(1))
			{
				InteractWithBrick();
			}
		}
    }

	void AddBrick()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(ray, out hit);

		if (hit.transform)
		{
			GameObject tempBrick = Instantiate(brick, vehicule.transform);
			tempBrick.transform.position = hit.transform.position + hit.normal * 0.5f;
		}
	}


	void InteractWithBrick()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(ray, out hit);

		if (hit.transform)
		{
			hit.transform.GetComponent<ReactorBrick>().SetDirection(-hit.normal); 
		}
	}
}
