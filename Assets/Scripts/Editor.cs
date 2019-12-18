using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
	[SerializeField] GameObject vehicule;
	[SerializeField] GameObject neutralBrick;
	[SerializeField] GameObject reactorBrick;

	public BrickTypes currentBrickType;

    void Start()
    {
		currentBrickType = BrickTypes.Neutral;
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

		if (hit.transform && hit.transform.tag == "Brick")
		{
			Debug.Log(hit.transform.gameObject.name);

			switch (currentBrickType)
			{
				case BrickTypes.Neutral:	AddNeutralBrick(hit);	break;
				case BrickTypes.Reactor:    AddReactorBrick(hit);	break;
			}
		}
	}

	void AddNeutralBrick(RaycastHit _hit)
	{
		GameObject tempBrick = Instantiate(neutralBrick, vehicule.transform);
		tempBrick.transform.position = _hit.transform.position + _hit.normal * 0.5f;
	}

	void AddReactorBrick(RaycastHit _hit)
	{
		GameObject tempBrick = Instantiate(reactorBrick, vehicule.transform);
		tempBrick.transform.position = _hit.transform.position + _hit.normal * 0.5f;

		tempBrick.GetComponent<ReactorBrick>().SetDirection(-_hit.normal);
		tempBrick.GetComponent<ReactorBrick>().SetVehicule(vehicule.GetComponent<Rigidbody>());
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

	// UI Brick type selection
	public void SelectNeutralBrick()
	{
		currentBrickType = BrickTypes.Neutral;
	}
	public void SelectReactorBrick()
	{
		currentBrickType = BrickTypes.Reactor;
	}
}
