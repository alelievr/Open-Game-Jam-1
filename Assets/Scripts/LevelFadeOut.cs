using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFadeOut : MonoBehaviour {

	public string			nextLevelName;
	public Image			panel;
	[Space]
	public float			fadeTime = .5f;
	public AnimationCurve	fadeCurve;

	AudioSource				playerAudioSource;

	float			alpha;

	void Start()
	{
		playerAudioSource = Camera.main.GetComponent< AudioSource >();

		StartCoroutine(FadeOut());
	}

	public IEnumerator FadeOut()
	{
		float	startTime = Time.time;
		
		do
		{
			alpha = 1 - ((Time.time - startTime) / fadeTime);
			alpha = fadeCurve.Evaluate(alpha);
			playerAudioSource.volume = 1 - alpha;
			panel.color = new Color(0, 0, 0, alpha);
			yield return null;
		} while (alpha > 0f);
	}
	
	public IEnumerator FadeIn()
	{
		float	startTime = Time.time;
		
		do
		{
			alpha = (Time.time - startTime) / fadeTime;
			alpha = fadeCurve.Evaluate(alpha);
			playerAudioSource.volume = 1 - alpha;
			panel.color = new Color(0, 0, 0, alpha);
			yield return null;
		} while (alpha < 1f);

		if (!string.IsNullOrEmpty(nextLevelName))
			SceneManager.LoadScene(nextLevelName);
	}

	public void StartFadeIn(string levelName = null)
	{
		if (levelName != null)
			nextLevelName = levelName;
		StartCoroutine(FadeIn());
	}
}
