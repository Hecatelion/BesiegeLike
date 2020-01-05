using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*public class SavedVehicleData
{
	public SavedVehicleData(string _name, string _jsonData)
	{
		name = _name;
		jsonData = _jsonData;
	}

	public string name;
	public string jsonData;
}*/

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

	public static void Save(GameObject _vehicleGO, string _name)
	{
		instance.savedVehiclesDatas[_name] = JsonUtility.ToJson(_vehicleGO);
	}

	public static GameObject Load(string _name)
	{
		return JsonUtility.FromJson<GameObject>(instance.savedVehiclesDatas[_name]);
	}
}
