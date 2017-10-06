using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour {

	public bool		canRepeat;
	public bool		triggerOnEnter = true;
	public bool		triggerOnExit = false;
	public bool		once = true;

	[Space]
	public float	delayTrigger = 0f;
	public float	flashDuration = .2f;
	public float	fadeOutDuration = .4f;

	int				flashCount = 0;
	SpriteRenderer	sp;

	// Use this for initialization
	void Start () {
		sp = GetComponent< SpriteRenderer >();
		sp.enabled = false;
	}
	
	IEnumerator	FadeOut()
	{
		float startTime = Time.time;
		float t = (Time.time - startTime) / fadeOutDuration;
		while (t < 1f)
		{
			t = (Time.time - startTime) / fadeOutDuration;
			sp.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1f, 0f, t));
			yield return null;
		}
		sp.enabled = false;
	}

	IEnumerator FlashCoroutine()
	{
		if (once && flashCount > 0)
			yield break;
		
		flashCount++;
		if (delayTrigger > 0)
			yield return new WaitForSeconds(delayTrigger);

		sp.enabled = true;
		yield return new WaitForSeconds(flashDuration);

		if (fadeOutDuration > 0)
			StartCoroutine(FadeOut());
		else
			sp.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player")
			return ;
		if (triggerOnEnter)
			StartCoroutine(FlashCoroutine());

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "Player")
			return ;
		
		if (triggerOnExit)
			StartCoroutine(FlashCoroutine());
	}
}
