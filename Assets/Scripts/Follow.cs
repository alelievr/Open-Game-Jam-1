using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject who;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = who.transform.position + new Vector3(0f, 0f, -5f);		
	}
}
