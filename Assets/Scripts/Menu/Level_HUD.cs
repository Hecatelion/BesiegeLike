using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_HUD : MonoBehaviour
{
	// UI function
	public void UI_Back()
	{
		Vehicle foundVehicle = GameObject.FindObjectOfType<Vehicle>();
		if (foundVehicle)
		{
			Destroy(foundVehicle.gameObject);
		}

		TheCustomSceneManager.LoadScene_SetNextLevel("LevelSelection");
	}
}
