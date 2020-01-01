using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsToggleActivable : MonoBehaviour
{
	[SerializeField] Material matUnusable;
	// UIText -> scale, recolor for example

	IActivable activable;

	// Start is called before the first frame update
	void Start()
    {
		activable = GetComponent<IActivable>();
    }

    // Update is called once per frame
    void Update()
    { }

	public void SetUnusable()
	{
		//activable.SetOFF();
		GetComponent<MeshRenderer>().material = matUnusable;
	}
}
