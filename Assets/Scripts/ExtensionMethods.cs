using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void RemoveNullItems<T>(this List<T> list) where T : Object
	{
		list.RemoveAll(item => item == null);
	}
}
