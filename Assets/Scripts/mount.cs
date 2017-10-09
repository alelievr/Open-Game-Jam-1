using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mount : MonoBehaviour {

	// Use this for initialization
	public float speed;
	float time = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 2f)
		{
			transform.position += new Vector3(0, speed * Time.deltaTime, 0);
		}
	}
}
