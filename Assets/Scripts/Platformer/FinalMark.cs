using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMark : MonoBehaviour {

	public SpriteRenderer	stage1;
	public SpriteRenderer	stage2;
	public SpriteRenderer	stage3;
	public SpriteRenderer	stage4;

	[Space]
	public Color			color = Color.blue;
	public float			fadeSpeed = .3f;
	public float			fadeTransitionTime = .1f;
	public AnimationCurve	fadeCurve;

	new ParticleSystem		particleSystem;

	void Start()
	{
		particleSystem = GetComponent< ParticleSystem >();

		particleSystem.Stop();
		
		// StartMarkAnimation();
	}

	public void StartMarkAnimation()
	{
		int level = GameManager.instance.points;

		particleSystem.Play();
		StartCoroutine(FadeInColorMark(level));
	}

	IEnumerator	FadeInColorStage(SpriteRenderer stage)
	{
		float	time = Time.time;
		float	t;
		Color	defaultStageColor = stage.color;

		do
		{
			t = (Time.time - time) / fadeSpeed;
			float e = fadeCurve.Evaluate(t);
			stage.color = Color.Lerp(defaultStageColor, color, e);
			yield return null;
		} while (t < 1);
	}

	IEnumerator	FadeInColorMark(int level)
	{
		if (level == 0)
			yield break ;
		
		yield return StartCoroutine(FadeInColorStage(stage1));
		if (level > 1)
			yield return StartCoroutine(FadeInColorStage(stage2));
		if (level > 2)
			yield return StartCoroutine(FadeInColorStage(stage3));
		if (level > 3)
			yield return StartCoroutine(FadeInColorStage(stage4));
	}
}
