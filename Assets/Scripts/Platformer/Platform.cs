using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

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

	public bool		blind { get { return blindTime > 0;} }
	[Space]
	public float	blindTime = 0.0f;
	public bool		blindPlayer = false;
	public float	blindTransitionTime = .4f;

	ParticleSystem	ps;
	SpriteRenderer	sr;
	Color			color;
	new Camera		camera;
	PostProcessingProfile	ppp;
	new Collider2D	collider;
	float			vignetteIntensity;
	Transform		playerTransform;

	// Use this for initialization
	void Start () {
		ps = GetComponent< ParticleSystem >();
		sr = GetComponent< SpriteRenderer >();
		ppp = Camera.main.GetComponent< PostProcessingBehaviour >().profile;
		camera = Camera.main;
		collider = GetComponent< Collider2D >();

		color = sr.color;

	}
	public void	Init(float timeBeforeDispawn, float blinkTime = 0f)
	{
		this.timeBeforeDispawn = timeBeforeDispawn;
		this.blinkTime = blinkTime;
		if (dispawn)
			StartCoroutine(Dispawn());
		if (blink)
			StartCoroutine(Blink());
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag != "Player")
			return ;
		
		StopCoroutine("ResetBlindness");

		if (blind)
		{
			playerTransform = other.transform;
			blindPlayer = true;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.collider.tag != "Player")
			return ;
		
		StartCoroutine("ResetBlindness");
	}

	IEnumerator ResetBlindness()
	{
		yield return new WaitForSeconds(blindTime);

		while (vignetteIntensity > 0)
		{
			blindPlayer = false;
			var vignetting = ppp.vignette.settings;
			vignetting.intensity = vignetteIntensity;
			ppp.vignette.settings = vignetting;
			vignetteIntensity = Mathf.Clamp01(vignetteIntensity - 0.06f);
			yield return null;
		}
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
		ParticleSystem	ps2 = this.gameObject.GetComponent< ParticleSystem >();
		ps2.Play();
		yield return new WaitForSeconds(0.3f);
		Destroy(this.transform.parent.gameObject);
		Destroy(gameObject, 10f);
	}

	void Update()
	{
		if (blindPlayer)
		{
			Vector3 playerPos = playerTransform.position;
			Vector2 screenPoint = camera.WorldToScreenPoint(playerPos);

			screenPoint.x /= Screen.width;
			screenPoint.y /= Screen.height;
			Vector2 vignettingPos = screenPoint;


			var vignetting = ppp.vignette.settings;
			vignetting.intensity = Mathf.Clamp01(vignetteIntensity - .1f);
			vignetting.center = vignettingPos;
			ppp.vignette.settings = vignetting;

			vignetteIntensity = Mathf.Clamp01(vignetteIntensity + 0.06f);
		}
	}

	void OnDestroy()
	{
		//reset vignetting to zero
		if (ppp != null)
		{
			var vignetting = ppp.vignette.settings;
			vignetting.intensity = 0.0f;
			vignetting.center = Vector2.one / 2f;
			ppp.vignette.settings = vignetting;
		}
	}
}
