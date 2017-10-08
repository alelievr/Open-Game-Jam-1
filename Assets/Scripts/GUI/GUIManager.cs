using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public GUIPointActive[]	points;

	public GameObject		deadScreen;
	public GameObject		helpScreen;

	bool					helpScreenActive = false;

	void OnEnable()
	{
		//attach events from game manager:
		GameManager.instance.OnPointsModified += OnPointsModifiedCallback;
		GameManager.instance.OnPlayerDeath += OnPlayerDeathCallback;
		// GameManager.instance.OnPlayerRestart += OnPlayerRestartCallback;
	}

	void OnDisable()
	{
		//detach events from game manager:
		GameManager.instance.OnPointsModified -= OnPointsModifiedCallback;
		GameManager.instance.OnPlayerDeath -= OnPlayerDeathCallback;
		// GameManager.instance.OnPlayerRestart -= OnPlayerRestartCallback;
	}

	void OnPlayerDeathCallback()
	{
		deadScreen.SetActive(true);
	}

	// void OnPlayerRestartCallback()
	// {
	// 	deadScreen.SetActive(false);
	// }

	void OnPointsModifiedCallback(int newPointCount)
	{
		points[newPointCount - 1].ActivePoint();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			helpScreenActive = !helpScreenActive;
			helpScreen.SetActive(helpScreenActive);
		}
	}
}
