using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartToPlay : MonoBehaviour {

	public float	blinkTime;
	public Text		toBlink;

	bool 			started = false;
	LevelFadeOut	fadeOut;

	// Use this for initialization
	void Start () {
		fadeOut = GetComponent< LevelFadeOut >();
		StartCoroutine(Blink());
	}

	IEnumerator Blink()
	{
		while (true)
		{
			yield return new WaitForSeconds(blinkTime);
			toBlink.enabled = false;
			yield return new WaitForSeconds(blinkTime);
			toBlink.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) && !started)
		{
			started = true;
			StartCoroutine(fadeOut.FadeIn());
		}
	}
}
