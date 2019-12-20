using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductiveBrick : MonoBehaviour
{
	[SerializeField] GameObject conductiveWiresGO;
	ConductiveWires wires;
	public bool isConducting;

    void Start()
    {
		// Instantiate Conductive Wires Object and store its script
		GameObject temp = Instantiate(conductiveWiresGO, transform);
		//temp.transform.position;
		wires = temp.GetComponent<ConductiveWires>();

		// test connectivity when Conductive Wires are colliding with another brick
		wires.ProcessOnTriggerEnter += TestConductivity;
    }

    void Update()
    {
        
    }

	public void TestConductivity(Collider _other)
	{
		ConductiveBrick otherConductive = _other.GetComponent<ConductiveBrick>();

		// if brick is colliding with another conductive brick which is electrically charged, then conducts electricity
		if (otherConductive && otherConductive.isConducting)
		{
			isConducting = true;
		}
	}
}
