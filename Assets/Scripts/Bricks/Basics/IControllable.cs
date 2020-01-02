using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
	// key binding
	void TestBind(Event curEvent);
	void Bind(KeyCode _key);
	KeyCode GetBoundKey();

	// brick behavior
	void Use();
}
