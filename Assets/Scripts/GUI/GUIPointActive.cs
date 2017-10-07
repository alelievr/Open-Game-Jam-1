using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPointActive : MonoBehaviour {

	public ParticleSystem	pp1;
	public ParticleSystem	pp2;

	public Color			markEnabledColor;
	public Color			pointEnabledColor;
	public Color			markDisabledColor;
	public Color			pointDisabledColor;
	public Image			pointImage;
	public Image			markImage;

	// Use this for initialization
	void Start () {
		StopPoint();
	}

	public void StopPoint()
	{
		pp1.Stop();
		pp2.Stop();

		markImage.color = markDisabledColor;
		pointImage.color = pointDisabledColor;
	}

	IEnumerator ImageFadeIn()
	{
		float t;
		float time = Time.time;

		do
		{
			t = (Time.time - time) / .4f;
			pointImage.color = Color.Lerp(pointDisabledColor, pointEnabledColor, t);
			markImage.color = Color.Lerp(markDisabledColor, markEnabledColor, t);
			yield return null;
		} while(t < 1);
	}

	public void ActivePoint()
	{
		pp1.Play();
		pp2.Play();

		StartCoroutine(ImageFadeIn());
	}
	
}
