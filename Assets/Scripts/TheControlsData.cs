using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TheControlsData
{
	// forbidden keys
	private static KeyCode[] forbiddenKeys =
	{
		KeyCode.LeftAlt,
		KeyCode.Escape
	};

	public static bool IsForbidden(KeyCode _key)
	{
		foreach (var forbiddenKey in forbiddenKeys)
		{
			if (_key == forbiddenKey)
			{
				return true;
			}
		}
		return false;
	}
}
