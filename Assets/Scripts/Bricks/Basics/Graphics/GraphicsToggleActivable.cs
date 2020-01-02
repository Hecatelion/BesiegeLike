using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsToggleActivable : MonoBehaviour
{
	[SerializeField] Material matUnusable;
	Text[] worldSpaceTexts;

	IActivable activable;

	// Start is called before the first frame update
	void Start()
    {
		activable = GetComponent<IActivable>();
		worldSpaceTexts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    { }

	public void SetUnusable()
	{
		GetComponent<MeshRenderer>().material = matUnusable;
	}

	public void SetBindingMode(bool _isWaitingForBinding = true)
	{
		// graphic feedback when brick is waiting for key binding
		Color col = (_isWaitingForBinding) ? Color.cyan : Color.white;
		SetTextsColor(col);
	}

	public void SetTexts(string _str)
	{
		foreach (var t in worldSpaceTexts)
		{
			t.text = _str;
		}
	}

	public void SetTextsColor(Color _col)
	{
		foreach (var t in worldSpaceTexts)
		{
			t.color = _col;
		}
	}
}
