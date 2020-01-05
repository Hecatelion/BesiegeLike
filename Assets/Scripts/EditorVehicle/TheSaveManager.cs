using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TheSaveManager : MonoBehaviour
{
	private static TheSaveManager instance;

	Dictionary<string, string> savedVehiclesDatas;

	private void Start()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

			savedVehiclesDatas = new Dictionary<string, string>();
		}
		else
		{
			Destroy(this);
		}
	}

	public static void Save(Vehicle _vehicle, string _name)
	{
		instance.savedVehiclesDatas[_name] = VehicleToJson(_vehicle, _name);
	}

	public static JsonableVehicle Load(string _name)
	{
		// from name reads data to get vehicle data
		return new JsonableVehicle(_name);
	}

	private static string VehicleToJson(Vehicle vehicle, string _name)
	{
		JsonableVehicle jsonVehicle = new JsonableVehicle(_name);

		foreach (var brick in vehicle.bricks)
		{
			if (brick.Type == e_BrickType.None)
			{
				Debug.LogError("brick type = None");
			}
			else
			{
				JsonableBrick jsonableBrick = new JsonableBrick();
				jsonableBrick.type = brick.Type;
				jsonableBrick.position = brick.transform.position;

				if (brick.Type == e_BrickType.Reactor)
				{
					JsonableReactorBrick jsonableReactor = new JsonableReactorBrick(jsonableBrick);
					jsonableReactor.direction = (brick as ReactorBrick).Direction;
				}
				else if (brick.Type == e_BrickType.Switch)
				{
					JsonableSwitchBrick jsonableReactor = new JsonableSwitchBrick(jsonableBrick);
					jsonableReactor.keyBound = (brick as SwitchBrick).GetBoundKey();
				}
				else // all other brick time
				{
					jsonVehicle.jsonBricks.Add(jsonableBrick);
				}
			}
		}

		return jsonVehicle.ToJson();
	}

	private static string Quoted(string _str)
	{
		return "\"" + _str + "\"";
	}
}
