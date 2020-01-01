using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsActivable : MonoBehaviour
{
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
		GetComponent<MeshRenderer>().material = matON;
	}

	public void SetOFF()
	{
		GetComponent<MeshRenderer>().material = matOFF;
	}
}
