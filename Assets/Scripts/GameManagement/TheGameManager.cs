using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton
public class TheGameManager : MonoBehaviour
{
	private static TheGameManager instance;

	[SerializeField] private e_GameMode gameMode;
	public static e_GameMode  GameMode
	{
		get => instance.gameMode;
		set => instance.gameMode = value;
	}

    // Start is called before the first frame update
    void Start()
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

	// Update is called once per frame
	void Update()
	{ }
}
