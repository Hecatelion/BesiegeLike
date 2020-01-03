using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void UI_GoToLevelSelection()
	{
		TheCustomSceneManager.LoadScene_SetNextLevel("LevelSelection");
	}

	public void UI_GoToEditor()
	{
		TheCustomSceneManager.LoadScene_SetNextLevel("Editor");
	}

	public void UI_Quit()
	{
		Application.Quit();
	}
}
