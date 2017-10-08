using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	//singleton
	public static GameManager instance { get; private set; }

	//number of gathered points in this level
	int				_points = 0;
	GUIManager		guiManager;
	LevelFadeOut	levelFade;
	public int		points { get { return _points; } set {
		_points = value;
		if (OnPointsModified != null)
			OnPointsModified(_points);
	} }

	public event Action< int >	OnPointsModified;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		levelFade = FindObjectOfType< LevelFadeOut >();
	}

	public void LoadNextLevel()
	{
		//will swhitch the scene at the end of the fade
		StartCoroutine(levelFade.FadeIn());
	}
}
