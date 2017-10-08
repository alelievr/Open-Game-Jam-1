using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Player")
		{
			//TODO: player death
		}
	}

}
