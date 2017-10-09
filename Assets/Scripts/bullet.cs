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
		dir = Vector3.Normalize(new Vector3(Camera.main.transform.position.x - transform.position.x, Camera.main.transform.position.y - transform.position.y));
		lifetime = 0;
	}

	// Update is called once per frame
	void Update () {
		lifetime += Time.deltaTime;

		if (lifetime > 20)
			GameObject.Destroy(gameObject);
		if (follow)
			dir = Vector3.Normalize(new Vector3(Camera.main.transform.position.x - transform.position.x, Camera.main.transform.position.y - transform.position.y));
		transform.localPosition += dir * vitesse;
		if (transform.localPosition.y < -1200 || transform.localPosition.x < -1200 || transform.localPosition.y > 1200 || transform.localPosition.x > 1200)
			GameObject.Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		GameObject.Destroy(this.gameObject);		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			GameObject.Destroy(this.gameObject);
	}
}
