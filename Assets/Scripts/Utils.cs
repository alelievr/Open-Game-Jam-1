using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utils {

	public static IEnumerator	FadeOut(Image i, Color startColor, Color endColor, float fadeOutDuration)
	{
		float startTime = Time.time;
		float t;

		do
		{
			t = (Time.time - startTime) / fadeOutDuration;
			i.color = Color.Lerp(startColor, endColor, t);
			yield return null;
		} while (t < 1f);
	}
	
	public static IEnumerator	FadeOut(SpriteRenderer i, Color startColor, Color endColor, float fadeOutDuration)
	{
		float startTime = Time.time;
		float t;

		do
		{
			t = (Time.time - startTime) / fadeOutDuration;
			i.color = Color.Lerp(startColor, endColor, t);
			yield return null;
		} while (t < 1f);
	}
	
	public static IEnumerator	FadeOut(SpriteRenderer i, Color startColor, Color endColor, float fadeOutDuration, AnimationCurve fadeCurve)
	{
		float startTime = Time.time;
		float t;

		do
		{
			t = (Time.time - startTime) / fadeOutDuration;
			i.color = Color.Lerp(startColor, endColor, fadeCurve.Evaluate(t));
			yield return null;
		} while (t < 1f);
	}

}
