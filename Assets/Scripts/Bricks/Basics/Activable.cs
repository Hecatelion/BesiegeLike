using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
	public bool isON = false;
	[SerializeField] Material matOFF;
	[SerializeField] Material matON;

	// Start is called before the first frame update
	void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

	public void SetON()
	{
		isON = true;
		GetComponent<MeshRenderer>().material = matON;
	}

	public void SetOFF()
	{
		isON = false;
		GetComponent<MeshRenderer>().material = matOFF;
	}
}
