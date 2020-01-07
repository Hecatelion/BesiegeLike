using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class TheSaveManager : MonoBehaviour
{
	private static TheSaveManager instance;

	string dir = "Assets/";
	Dictionary<string, VehicleData> savedVehiclesDatas;

	private void Start()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

			savedVehiclesDatas = new Dictionary<string, VehicleData>();
		}
		else
		{
			Destroy(this);
		}
	}

	public static void SaveVehicle(Vehicle _vehicle, string _name)
	{
		instance.savedVehiclesDatas[_name] = _vehicle.GetData(_name);

		// temporary
		WriteDataFile(_name);
	}

	public static VehicleData LoadVehicle(string _name)
	{
		// temporary
		ReadDataFile(_name);

		return instance.savedVehiclesDatas[_name];
	}
	
	private static void ReadDataFile(string _vehicleName)
	{
		StreamReader reader = new StreamReader(instance.dir + _vehicleName + ".json");
		string str = reader.ReadToEnd();
		reader.Close();

		var vDat = VehicleData.FromJson(str);
		instance.savedVehiclesDatas[_vehicleName] = vDat;
	}

	private static void WriteDataFile(string _vehicleName)
	{
		StreamWriter writer = new StreamWriter(instance.dir + _vehicleName + ".json");
		writer.Write(instance.savedVehiclesDatas[_vehicleName].ToJson());
		writer.Close();
	}

	// read all files

	// write all files
}
