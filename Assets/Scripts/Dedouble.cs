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
			ParticleSystem.ColorOverLifetimeModule colorModule = tmp.colorOverLifetime;
			// colorModule.color = new ParticleSystem.MinMaxGradient(Color.green, Color.red);
			colorModule.color = Color.blue;
			ParticleSystem.MainModule settings = tmp.main;
			settings.startColor = Color.blue;
			tmp.GetComponent<Renderer>().material.color = Color.blue;
		}
		SpriteRenderer[] sr = go.gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	
	void Dedoublation()
	{
		if (isdedoubled == true || havededoubled == true)
			return;
		GameObject tmp = GameObject.Instantiate (this.gameObject);
//		Destroy(tmp.GetComponent("Dedouble"));
		tmp.GetComponent<Stopmoving> ().cannotmove = true;
		tmp.GetComponent<Dedouble> ().isdedoubled = true;
		tmp.GetComponent<Rigidbody2D> ().isKinematic = true;
		tmp.GetComponent< Collider2D > ().enabled = false;
		// tmp.GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
		havededoubled = true;
		dedouble = tmp.GetComponent<Dedouble>();
		colorbluator(tmp);
		SpriteRenderer tmp3 = tmp.GetComponent<SpriteRenderer> () ;
		if (tmp3)
			tmp3.color = new Color(0.9f, 0.9f, 0.9f);
	}

	// Update is called once per frame
	void Update () {
		if (isdedoubled == true)
			return;
		if (Input.GetKey("up"))
		{
			Dedoublation();
		}
	}
}
