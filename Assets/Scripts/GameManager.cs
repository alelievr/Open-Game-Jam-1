using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//singleton
	public static GameManager instance { get; private set; }

	//number of gathered points in this level
	int					_points = 0;
	GUIManager			guiManager;
	LevelFadeOut		levelFade;
	PlayerController	player;
	public int		points { get { return _points; } set {
		_points = value;
		if (OnPointsModified != null)
			OnPointsModified(_points);
	} }

	bool						playerDead = false;

	public event Action< int >	OnPointsModified;
	public event Action			OnPlayerDeath;
	// public event Action			OnPlayerRestart;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		levelFade = FindObjectOfType< LevelFadeOut >();
		player = FindObjectOfType< PlayerController >();
	}

	void Update()
	{
		if (player.life <= 0 && !playerDead)
		{
			OnPlayerDeath();
			playerDead = true;
		}
		if (playerDead)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadNextLevel()
	{
		//will swhitch the scene at the end of the fade
		StartCoroutine(levelFade.FadeIn());
	}
}
