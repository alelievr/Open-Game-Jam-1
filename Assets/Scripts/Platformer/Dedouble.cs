using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dedouble : MonoBehaviour {
	[HideInInspector] bool isdedoubled = false;
	[HideInInspector] bool havededoubled = false;
	private Dedouble dedouble;

	// Use this for initialization
	void Start () {
		
	}

	void colorbluator(GameObject go)
	{
		ParticleSystem[] ps = go.gameObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem tmp in ps)
		{
			if (tmp.isPaused)
				tmp.Play();
			else
				tmp.Pause();
			ParticleSystem.ColorOverLifetimeModule colorModule = tmp.colorOverLifetime;
			// colorModule.color = new ParticleSystem.MinMaxGradient(Color.green, Color.red);
			GradientColorKey[] tmp5 = colorModule.color.gradient.colorKeys;
			int i = -1;
			while (++i < tmp5.Length)
			{
				tmp5[i].color = new Color(1 - tmp5[i].color.r, 1 - tmp5[i].color.g, 1 - tmp5[i].color.b);
			}
			Gradient ourGradient = new Gradient();
			ourGradient.SetKeys(
				tmp5,
				new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(0.5f, 1.0f) }
            );
			colorModule.color = ourGradient;
			Invoke("ModifyGradient", 0.2f);
			// colorModule.color = new Color(1 - colorModule.color.color.r, 1 - colorModule.color.color.g, 1 - colorModule.color.color.b);
			ParticleSystem.MainModule settings = tmp.main;
			settings.startColor = new Color(1 - settings.startColor.color.r, 1 - settings.startColor.color.g, 1 - settings.startColor.color.b);
			// Color tmp2 = tmp.GetComponent<Renderer>().material.color;
			// tmp.GetComponent<Renderer>().material.color = new Color(1 - tmp2.r, 1 - tmp2.g, 1 - tmp2.b);
		}
		SpriteRenderer[] sr = go.gameObject.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer tmp2 in sr)
		{
			tmp2.color = new Color(0, 1, 0);
		}
	}
	
	void Dedoublation()
	{
		if (isdedoubled == true || havededoubled == true)
			return;
		GameObject tmp = GameObject.Instantiate (this.gameObject);
//		Destroy(tmp.GetComponent("Dedouble"));
		this.GetComponent<Stopmoving> ().cannotmove = true;
		this.GetComponent<Dedouble> ().isdedoubled = true;
		this.GetComponent<Rigidbody2D> ().isKinematic = true;
		this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		this.GetComponent< Collider2D > ().enabled = false;
		PlayerController tmp4 = this.GetComponent<PlayerController>();
		if (tmp4 && !tmp4.facingRight)
			tmp.GetComponent<PlayerController>().facingRight = false;
		// this.GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
		tmp.GetComponent<Dedouble>().havededoubled = true;
		tmp.GetComponent<Dedouble>().dedouble = this;
		colorbluator(this.gameObject);
		SpriteRenderer tmp3 = this.GetComponent<SpriteRenderer> () ;
		if (tmp3)
			tmp3.color = new Color(0.9f, 0.9f, 0.9f);
	}

	void redoublation()
	{
		dedouble.GetComponent<Stopmoving> ().cannotmove = false;
		dedouble.GetComponent<Dedouble> ().isdedoubled = false;
		dedouble.GetComponent<Rigidbody2D> ().isKinematic = false;
		// dedouble.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		dedouble.GetComponent< Collider2D > ().enabled = true;
		colorbluator(dedouble.gameObject);
		SpriteRenderer[] sr = dedouble.gameObject.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer tmp2 in sr)
		{
			tmp2.color = new Color(0, 0, 0);
		}
		GameObject.Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (isdedoubled == true)
			return;
		if (havededoubled == true && Input.GetKey("down"))
			redoublation();
		if (Input.GetKey("up"))
		{
			Dedoublation();
		}
	}
}
