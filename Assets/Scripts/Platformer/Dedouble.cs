using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dedouble : MonoBehaviour {
	[HideInInspector] bool isdedoubled = false;
	// Use this for initialization
	void Start () {
		
	}
	
	void Dedoublation()
	{
		if (isdedoubled == true)
			return;
		GameObject tmp = GameObject.Instantiate (this.gameObject);
//		Destroy(tmp.GetComponent("Dedouble"));
		tmp.GetComponent<Stopmoving> ().cannotmove = true;
		tmp.GetComponent<Dedouble> ().isdedoubled = true ;
		SpriteRenderer tmp3 = tmp.GetComponent<SpriteRenderer> () ;
		if (tmp3)
			tmp3.color = new Color(0.9f, 0.9f, 0.9f);
	}

	// Update is called once per frame
	void Update () {
		if (isdedoubled == true)
			return;
		if (Input.GetKey("up"))
		{
			Dedoublation();
		}
	}
}
