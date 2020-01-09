using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton
public class TheDLC : MonoBehaviour
{
	private static TheDLC instance;

	string level2Path;
	public static string Level2Path
	{
		get => instance.level2Path;
	}

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
		var dlcBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/dlc");
		if (dlcBundle)
		{
			string[] scenePath = dlcBundle.GetAllScenePaths();
			foreach (string path in scenePath)
			{
				if (path.Contains("Level2"))
				{
					level2Path = path;
				}
			}
		}
	}
}