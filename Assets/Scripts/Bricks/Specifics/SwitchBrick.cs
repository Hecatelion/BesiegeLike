using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// controllable electrical toggle switch
// -> allows controller to cut or link conductor
// -> bound to a key, controlled by Controller (c.f. Controller.cs, Action.cs, and Vehicule.cs)
public class SwitchBrick : Conductor, IControllable, IInteractible
{
	GraphicsToggleActivable graphicsToggle;

	//private int boundKey = 0; // should become KeyCode
	private KeyCode boundKey = KeyCode.None; // should become KeyCode
	public bool isWaitingForKeyToBind = false;
	public Vehicle linkedVehicle = null;

	bool graphicsReady = false;
	bool needGraphicsUpdate = false;

	// Start is called before the first frame update
	override protected void Start()
    {
		base.Start();

		graphicsToggle = GetComponent<GraphicsToggleActivable>();

		SetUsable(false);

		graphicsReady = true;
    }

    // Update is called once per frame
    override protected void Update()
    {
		base.Update();

		if (needGraphicsUpdate)
		{
			// rebind to ut
			Bind(boundKey);
		}
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

	// wait for player to press a key
	// if it's valid, then bind this IControllable to this Key
	public void TestBind(Event curEvent) // out arg key -> move Bind() into IControllable definition
	{
		// if player press an autorized key, then bind key and stop
		if (curEvent.isKey)
		{
			if (!TheControlsData.IsForbidden(curEvent.keyCode))
			{
				Bind(curEvent.keyCode);
			}

			SetKeyBindingMode(false);
		}

		// if player click somewhere, then stop
		else if (curEvent.isMouse && curEvent.type == EventType.MouseDown)
		{
			SetKeyBindingMode(false);
		}
	}

	// IControllable interface implementation
	public void Bind(KeyCode _key)
	{
		boundKey = _key;

		if (graphicsReady)
		{
			// graphics
			graphicsToggle.SetTexts(boundKey.ToString());
			needGraphicsUpdate = false;
		}
		else
		{
			needGraphicsUpdate = true;
		}
	}

	public KeyCode GetBoundKey()
	{
		return boundKey;
	}

	public void Use()
	{
		// switch state on use as a classical toggle switch
		SetUsable(!isUsable);
	}

	// IInteraactible interface implementation
	public void Interact(RaycastHit _hit = default)
	{
		SetKeyBindingMode();
	}

	public void SetKeyBindingMode(bool _b = true)
	{
		isWaitingForKeyToBind = _b;
		graphicsToggle.SetBindingMode(_b);
	}

	private void OnGUI()
	{
		if (isWaitingForKeyToBind)
		{
			// get current event, only in OnGUI()
			Event curEvent = Event.current;

			TestBind(curEvent);
		}
	}
}
