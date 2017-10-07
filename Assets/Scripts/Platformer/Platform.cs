using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public bool		blink { get { return blinkTime > 0; } }
	public float	blinkTime = .0f;

	public bool		dispawn { get { return timeBeforeDispawn > 0; } }
	[Space]
	public float	timeBeforeDispawn = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
