using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void UI_GoToLevel(int _level)
	{
		string levelName = "None";

		if (_level > 1)
		{
			levelName = TheDLC.Level2Path;
		}
		else
		{
			levelName = "Level" + _level.ToString();
		}

		TheCustomSceneManager.LoadScene_SetNextLevel("Editor", 0.0f, levelName);
	}

	public void UI_GoToMenu()
	{
		TheCustomSceneManager.LoadScene_SetNextLevel("Menu");
	}
}
