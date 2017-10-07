using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	//is blincking ?
	public bool		blink { get { return blinkTime > 0; } }
	//time where the platform stay visible
	public float	blinkTime = .0f;
	//time where the platform is invisible
	public float	blinkInterval = 1f;

	public bool		dispawn { get { return timeBeforeDispawn > 0; } }
	[Space]
	public float	timeBeforeDispawn = 0f;

	ParticleSystem	ps;
	SpriteRenderer	sr;
	Color			color;
	new Collider2D	collider;

	// Use this for initialization
	void Start () {
		ps = GetComponent< ParticleSystem >();
		sr = GetComponent< SpriteRenderer >();
		collider = GetComponent< Collider2D >();

		color = sr.color;

		if (dispawn)
			StartCoroutine(Dispawn());
		if (blink)
			StartCoroutine(Blink());
	}

	IEnumerator	Blink()
	{
		while (true)
		{
			yield return new WaitForSeconds(blinkTime);
			sr.color = new Color(0, 0, 0, 0);
			collider.enabled = false;
			ps.Stop();
			yield return new WaitForSeconds(blinkInterval);
			sr.color = color;
			ps.Play();
			collider.enabled = true;
		}
	}

	IEnumerator	Dispawn()
	{
		yield return new WaitForSeconds(timeBeforeDispawn);
		ps.Stop();
		Color disabledColor = color;
		disabledColor.a = 0;
		StartCoroutine(Utils.FadeOut(sr, color, disabledColor, .4f));
		collider.enabled = false;
		Destroy(gameObject, 10f);
	}
}
