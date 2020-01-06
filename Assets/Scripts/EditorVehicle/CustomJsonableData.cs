using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class CustomJsonableData
{
	public abstract string ToJson();

	static public T FromJsonString<T>(string _jsonContent)
	{
		return JsonUtility.FromJson<T>(_jsonContent);
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

	public override string ToJson()
	{
		return JsonUtility.ToJson(this);
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

	public override string ToJson()
	{
		string jsonContent = string.Format("{{\n\t {0}", CustomJsonTool.JsonProperty("name", name)); // use reflection ?

		jsonContent += string.Format("\t\t {0} : [\n", CustomJsonTool.Quoted("bricks"));
		foreach (JsonableBrick jsonBrick in jsonBricks)
		{
			jsonContent += jsonBrick.ToJson();
		}

		jsonContent += "]\n}";

		return jsonContent;
	}
}

public static class CustomJsonTool
{
	internal static string JsonProperty(string _name, string value)
	{
		return string.Format("{0} : {1}, \n", Quoted(_name), Quoted(value));
	}

	internal static string Quoted(string _str)
	{
		return string.Format("\"{0}\"", _str);
	} }