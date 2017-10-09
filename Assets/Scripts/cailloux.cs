using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cailloux : MonoBehaviour {

	float timesincestart = 0;
	public float speed;
	public	float lifetime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timesincestart += Time.deltaTime;
		if (timesincestart > lifetime)
		{
			GameObject.Destroy(this.gameObject);
		}
		transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
	}
}
