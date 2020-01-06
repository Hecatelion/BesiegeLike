using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

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

		// temporary
		WriteDataFile("Assets/savedVehicles.json");
	}

	public static JsonableVehicle Load(string _name)
	{
		// temporary
		LoadDataFromFile("Assets/savedVehicles.json");

		// from name reads data to get vehicle data
		return CustomJsonableData.FromJsonString<JsonableVehicle>(instance.savedVehiclesDatas[_name]);
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
					jsonVehicle.jsonBricks.Add(jsonableReactor);
				}
				else if (brick.Type == e_BrickType.Switch)
				{
					JsonableSwitchBrick jsonableSwitch = new JsonableSwitchBrick(jsonableBrick);
					jsonableSwitch.keyBound = (brick as SwitchBrick).GetBoundKey();
					jsonVehicle.jsonBricks.Add(jsonableSwitch);
				}
				else // all other brick time
				{
					jsonVehicle.jsonBricks.Add(jsonableBrick);
				}
			}
		}

		return jsonVehicle.ToJson();
	}

	public static void WriteDataFile(string _path)
	{
		StreamWriter writer = new StreamWriter(_path, true);
		string content = "";

		foreach (string vehicleData in instance.savedVehiclesDatas.Values.ToArray())
		{
			content += vehicleData;
		}
		writer.Write(content);

		writer.Close();
	}

	public static void LoadDataFromFile(string _path)
	{
		// read file
		StreamReader reader = new StreamReader(_path);
		string fileContent = reader.ReadToEnd();

		// get json data
		LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(fileContent.Trim());
		
		// fill dictionary from data
		foreach (LitJson.JsonData vehicleJsonData in jsonData)
		{
			string vehicleName = (string)vehicleJsonData["name"];
			string vehicleContent = (string)vehicleJsonData;
			instance.savedVehiclesDatas[vehicleName] = vehicleContent;
		}

		_ = 0;
	}
}
