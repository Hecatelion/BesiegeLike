using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton
public class TheCustomSceneManager : MonoBehaviour
{
	private static TheCustomSceneManager instance;

	private void Start()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	public static void LoadScene_SetNextLevel(string _sceneToLoad, float _delay = 0.0f, string _levelName = "None")
	{
		instance.StartCoroutine(DelayedLoadScene(_sceneToLoad, _levelName, _delay));
	}

	private static IEnumerator DelayedLoadScene(string _sceneToLoad, string _levelName, float _delay)
	{
		yield return new WaitForSeconds(_delay);

		TheGameManager.NextLevel = _levelName;

		if (_sceneToLoad == "Menu" || _sceneToLoad == "LevelSelection")
		{
			TheGameManager.GameMode = e_GameMode.Menu;
		}
		else if (_sceneToLoad == "Editor")
		{
			TheGameManager.GameMode = e_GameMode.Editor;
		}
		else if (_sceneToLoad.Contains("Level"))
		{
			TheGameManager.GameMode = e_GameMode.Play;
		}
		else
		{
			Debug.LogError("no GameMode matching with loaded scene : " + _sceneToLoad);
		}

		SceneManager.LoadScene(_sceneToLoad);
		yield return null;
	}
	
	static private void TransitionTo()
	{
		// fade out transition ?
	}

	static public void UnlockNextLevel()
	{
		string nextLevelName = SceneManager.GetActiveScene().name;
		int cur = nextLevelName[nextLevelName.Length - 1];
		nextLevelName = "Scene" + cur.ToString();

		UnlockLevel(nextLevelName);
	}

	static public void UnlockLevel(string _levelToUnlock)
	{
		// code 
	}
}
