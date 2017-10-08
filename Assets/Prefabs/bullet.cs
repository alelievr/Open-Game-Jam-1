using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public bool follow;
	public float vitesse;
	Vector3 dir;
	Vector3 cible;
	float lifetime;
	// Use this for initialization
	void Start () {
		cible = Camera.main.transform.position;
		dir = Vector3.Normalize (Camera.main.transform.position - transform.position);
		lifetime = 0;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		GameObject.Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		lifetime += Time.deltaTime;

		if (lifetime > 20)
			GameObject.Destroy(gameObject);
			
		if (follow)
			dir = Vector3.Normalize (Camera.main.transform.position - transform.position);
		transform.localPosition += dir * vitesse;
		if (transform.localPosition.y < -120 || transform.localPosition.x < -120 || transform.localPosition.y > 120 || transform.localPosition.x > 120)
			GameObject.Destroy(gameObject);
	}
}
