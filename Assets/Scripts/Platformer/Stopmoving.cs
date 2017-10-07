using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopmoving : MonoBehaviour {

	public bool cannotmove = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool Stop()
	{
		return cannotmove;
	}
}
