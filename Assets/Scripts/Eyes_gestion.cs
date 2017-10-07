using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes_gestion : MonoBehaviour {

	public GameObject eyesright;
	public GameObject eyesleft;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start(){
		rigidBody = GetComponent<Rigidbody2D>();
		eyesright.SetActive(true);
		eyesleft.SetActive(false);
	}
	
	// Update is called once per frame
	void Update()
	{
		Debug.Log(rigidBody.velocity.x);
		if (rigidBody.velocity.x > .0f && eyesright.activeInHierarchy == false)
		{
			eyesright.SetActive(true);
			eyesleft.SetActive(false);
		}
		else if (rigidBody.velocity.x == .0f);
		else if (eyesleft.activeInHierarchy == false)
		{
			eyesright.SetActive(false);
			eyesleft.SetActive(true);
		}
	}
}
