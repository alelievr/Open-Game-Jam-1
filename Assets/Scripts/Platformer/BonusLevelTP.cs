using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusLevelTP : MonoBehaviour {

	public string		bonusLevelName;
	LevelFadeOut		levelFade;

	void Start()
	{
		levelFade = FindObjectOfType< LevelFadeOut >();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		levelFade.nextLevelName = bonusLevelName;
		StartCoroutine(levelFade.FadeIn());
	}

}
