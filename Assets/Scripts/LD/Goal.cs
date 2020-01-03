using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour, IWinnable
{
	[SerializeField] public GameObject hudGO;
	[SerializeField] public GameObject winningPopUpGO;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

	private void OnTriggerEnter(Collider _other)
	{
		if (_other.transform.parent &&  _other.transform.parent.GetComponent<Vehicle>())
		{
			Win(_other.transform.parent.GetComponent<Vehicle>());
		}
	}

	public void Win(Vehicle _winningVechicle)
	{ 
		TheCustomSceneManager.UnlockNextLevel();

		Instantiate(winningPopUpGO, hudGO.transform);

		StartCoroutine(WinningScenario(_winningVechicle));
	}

	private IEnumerator WinningScenario(Vehicle _winningVechicle)
	{
		yield return new WaitForSeconds(1f);
		SceneManager.MoveGameObjectToScene(_winningVechicle.gameObject, SceneManager.GetActiveScene());
		_winningVechicle.Explode();

		TheCustomSceneManager.LoadScene_SetNextLevel("LevelSelection", 1.5f);
		yield return null;
	}
}
