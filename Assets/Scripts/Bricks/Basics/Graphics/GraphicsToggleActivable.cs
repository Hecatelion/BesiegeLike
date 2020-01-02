using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsToggleActivable : MonoBehaviour
{
	[SerializeField] Material matUnusable;
	Text worldSpaceText;

	IActivable activable;

	// Start is called before the first frame update
	void Start()
    {
		activable = GetComponent<IActivable>();
		worldSpaceText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    { }

	public void SetUnusable()
	{
		//activable.SetOFF();
		GetComponent<MeshRenderer>().material = matUnusable;
	}

	public void SetText(string _str)
	{
		worldSpaceText.text = _str;
	}
}
