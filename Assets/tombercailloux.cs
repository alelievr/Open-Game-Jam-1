using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tombercailloux : MonoBehaviour {

	// Use this for initialization
	ParticleSystem ps;
	float timesincelast = 0;
	float delai = 0;
	public cailloux go;

	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timesincelast == 0)
			delai = Random.Range(6, 12);
		timesincelast += Time.deltaTime;
		if (timesincelast > delai)
			ps.Play();
		if (timesincelast > delai + 1f)
		{
			ps.Stop();
			timesincelast = 0;
			Debug.Log("fd");
			GameObject.Instantiate (go, transform.position, Quaternion.identity);
		}

	}
}
