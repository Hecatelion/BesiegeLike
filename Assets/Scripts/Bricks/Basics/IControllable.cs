using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
	// key binding (logic)
	void TestBind(Event curEvent);
	void Bind(KeyCode _key);
	KeyCode GetBoundKey();

	// key binding (graphics)
	void SetKeyBindingMode(bool _b);

	// brick behavior
	void Use();
}
