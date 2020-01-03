using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void UI_GoToLevel(int _level)
	{
		TheCustomSceneManager.LoadScene_SetNextLevel("Editor", 0.0f, "Level" + _level.ToString());
	}

	public void UI_GoToMenu()
	{
		TheCustomSceneManager.LoadScene_SetNextLevel("Menu");
	}
}
