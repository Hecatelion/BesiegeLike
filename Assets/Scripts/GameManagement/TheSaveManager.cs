using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class TheSaveManager : MonoBehaviour
{
	private static TheSaveManager instance;

	string dir;
	Dictionary<string, VehicleData> savedVehiclesDatas;

	private void Start()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

			dir = Application.streamingAssetsPath + "/";

			savedVehiclesDatas = new Dictionary<string, VehicleData>();

			// Load all data
			ReadAllDataFiles();
		}
		else
		{
			Destroy(this);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			WriteAllDataFiles();
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			ReadAllDataFiles();
		}
	}

	public static void SaveVehicle(Vehicle _vehicle, string _name)
	{
		instance.savedVehiclesDatas[_name] = _vehicle.GetData(_name);

		// save on data file
		WriteDataFile(_name);
	}

	public static VehicleData LoadVehicle(string _name)
	{
		return instance.savedVehiclesDatas[_name];
	}
	
	private static void ReadDataFile(string _fileName)
	{
		StreamReader reader = new StreamReader(instance.dir + _fileName);
		string str = reader.ReadToEnd();
		reader.Close();

		string vehicleName = _fileName.Split('.')[0];
		var vehicleData = VehicleData.FromJson(str);
		instance.savedVehiclesDatas[vehicleName] = vehicleData;
	}

	private static void WriteDataFile(string _vehicleName)
	{
		StreamWriter writer = new StreamWriter(instance.dir + _vehicleName + ".json");
		writer.Write(instance.savedVehiclesDatas[_vehicleName].ToJson());
		writer.Close();
	}

	// read all vehicle data files
	public static void WriteAllDataFiles()
	{
		foreach (string vehicleName in instance.savedVehiclesDatas.Keys)
		{
			WriteDataFile(vehicleName);
		}
	}

	// write all vehicle data files
	public static void ReadAllDataFiles()
	{
		string[] filesInDir = Directory.GetFiles(instance.dir);

		string[] dataFilesInDir = (from file in filesInDir
								  where file.Contains(".json") && !file.Contains(".meta")
								  select file).ToArray();

		foreach (string fileName in dataFilesInDir)
		{
			string trimedFileName = fileName.Remove(0, instance.dir.Length);
			ReadDataFile(trimedFileName);
		}
	}
}
