using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton
public class TheDLC : MonoBehaviour
{
	private static TheDLC instance;

	Scene level2;

	private void Start()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

			ReadBundleContent();
		}
		else
		{
			Destroy(this);
		}
	}

	void ReadBundleContent()
	{
		//if ( Application.persistentDataPath)
		{

		}
	}
}