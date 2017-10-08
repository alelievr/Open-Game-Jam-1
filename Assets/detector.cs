using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour {

	public wolf w;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			w.SetCible(other.GetComponent<PlayerController>());
		}
	}
}
