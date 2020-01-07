using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum e_Type
{
	Classic,
	Specific
}

public interface IJsonable
{
	string ToJson();
}

[System.Serializable]
public class BrickData
{
	public e_BrickType type;
	public Vector3 pos;

	public BrickData(e_BrickType _type, Vector3 _pos)
	{
		type = _type;
		pos = _pos;
	}
}

[System.Serializable]
public class ClassicBrickData : BrickData
{
	public ClassicBrickData(e_BrickType _type, Vector3 _pos) 
		: base(_type, _pos) { }
}

[System.Serializable]
public class ReactorBrickData : BrickData
{
	public Vector3 dir;

	public ReactorBrickData(Vector3 _pos, Vector3 _dir) 
		: base(e_BrickType.Reactor, _pos)
	{
		dir = _dir;
	}
}

[System.Serializable]
public class VehicleData : IJsonable
{
	public string name;
	public List<ClassicBrickData> classicbricksDatas;
	public List<ReactorBrickData> reactorbricksDatas;

	public VehicleData(string _name = "<no_name>")
	{
		name = _name;

		classicbricksDatas = new List<ClassicBrickData>();

		reactorbricksDatas = new List<ReactorBrickData>();
	}

	public void Add(BrickData _brickData)
	{
		switch (_brickData.type)
		{
			case e_BrickType.Core:
			case e_BrickType.Neutral:
			case e_BrickType.Circuit:
				classicbricksDatas.Add(_brickData as ClassicBrickData);
				break;
			case e_BrickType.Switch:
				//classicbricksDatas.Add(_brickData as ClassicBrickData);
				break;
			case e_BrickType.Reactor:
				reactorbricksDatas.Add(_brickData as ReactorBrickData);
				break;
			case e_BrickType.None:
			default:
				break;
		}
	}

	public string ToJson()
	{
		return JsonUtility.ToJson(this, true);
	}

	// static methods
	public static VehicleData FromJson(string _json)
	{
		return JsonUtility.FromJson<VehicleData>(_json);
	}
}
