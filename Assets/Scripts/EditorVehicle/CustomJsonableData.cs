using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CustomJsonableData
{
	public string ToJson()
	{
		return JsonUtility.ToJson(this);
	}
}

public class JsonableBrick : CustomJsonableData
{
	public e_BrickType type;
	public Vector3 position;

	public JsonableBrick() { }
	public JsonableBrick(JsonableBrick _jsonableBrick)
	{
		type = _jsonableBrick.type;
		position = _jsonableBrick.position;
	}
}

public class JsonableReactorBrick : JsonableBrick
{
	public Vector3 direction;

	public JsonableReactorBrick() { }
	public JsonableReactorBrick(JsonableBrick _jsonableBrick) : base(_jsonableBrick) { }
}

public class JsonableSwitchBrick : JsonableBrick
{
	public KeyCode keyBound;

	public JsonableSwitchBrick() { }
	public JsonableSwitchBrick(JsonableBrick _jsonableBrick) : base(_jsonableBrick) { }
}

public class JsonableVehicle : CustomJsonableData
{
	public string name;
	public List<JsonableBrick> jsonBricks;

	public JsonableVehicle(string _name)
	{
		name = _name;
		jsonBricks = new List<JsonableBrick>();
	}
}