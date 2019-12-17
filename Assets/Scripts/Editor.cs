using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
	[SerializeField] GameObject vehicule;
	[SerializeField] GameObject neutralBrick;
	[SerializeField] GameObject reactorBrick;

	public GameObject currentBrick;

    void Start()
    {
		currentBrick = neutralBrick;
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

		Physics.Raycast(ray, out hit, LayerMask.GetMask("Bricks"));

		if (hit.transform)
		{
			Debug.Log(hit.transform.gameObject.name);
			GameObject tempBrick = Instantiate(currentBrick, vehicule.transform);
			
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

	// UI functions
	public void SelectNeutralBrick()
	{
		currentBrick = neutralBrick;
	}
	public void SelectReactorBrick()
	{
		currentBrick = reactorBrick;
	}
}
