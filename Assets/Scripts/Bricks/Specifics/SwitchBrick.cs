using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// controllable electrical toggle switch
// -> allows controller to cut or link conductor
// -> bound to a key, controlled by Controller (c.f. Controller.cs, Action.cs, and Vehicule.cs)
public class SwitchBrick : Conductor, IControllable
{
	GraphicsToggleActivable graphicsToggle;

	private int boundKey = 0; // should become KeyCode

	// Start is called before the first frame update
	override protected void Start()
    {
		base.Start();

		graphicsToggle = GetComponent<GraphicsToggleActivable>();

		SetUsable(false);
    }

    // Update is called once per frame
    override protected void Update()
    {
		base.Update();
    }

	public void SetUsable(bool _isUsable = true)
	{
		isUsable = _isUsable;

		// if an IActivable Brick was ON and becomes unusable, then switch it OFF
		if(!isUsable)
		{
			SetOFF();
			graphicsToggle.SetUnusable();
		}
	}

	// IControllable interface implementation
	public int GetBoundKey()
	{
		return boundKey;
	}

	public void Use()
	{
		// switch state on use as a classical toggle switch
		SetUsable(!isUsable);
	}
}
