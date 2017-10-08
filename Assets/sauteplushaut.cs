using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sauteplushaut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			other.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 100);
	}
}
