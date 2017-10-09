using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScroller : MonoBehaviour {

	RectTransform		rt;

	public float scrollSpeed = 1.2f;

	LevelFadeOut	fadeOut;
	bool			end = false;

	void Start()
	{
		rt = GetComponent< RectTransform >();
		fadeOut = FindObjectOfType< LevelFadeOut >();
	}
	
	// Update is called once per frame
	void Update () {
		var s = rt.offsetMax;
		s.y += scrollSpeed;
		rt.offsetMax = s;

		if (s.y > 3300 && !end)
		{
			StartCoroutine(fadeOut.FadeIn());
			end = true;
		}
	}
}
