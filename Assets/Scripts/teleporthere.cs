using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporthere : MonoBehaviour {

	public PlayerController Pc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void tphere()
	{
		Pc.transform.position = transform.position;
	}
}
