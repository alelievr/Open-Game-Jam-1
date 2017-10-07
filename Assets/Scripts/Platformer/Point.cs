using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

	public ParticleSystem	pp1;
	public ParticleSystem	pp2;
	public SpriteRenderer	mark;
	public SpriteRenderer	point;

	bool		taken = false;

	IEnumerator		FadeOut()
	{
		float	t;
		float	time = Time.time;
		Color	markColor = mark.color;
		Color	pointColor = point.color;

		do
		{
			t = (Time.time - time) / .4f;
			point.color = new Color(pointColor.r, pointColor.g, pointColor.b, 1 - t);
			mark.color = new Color(markColor.r, markColor.g, markColor.b, 1 - t);
			yield return null;
		} while(t < 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (taken)
			return ;
		
		taken = true;
		GameManager.instance.points++;

		var pp1Main = pp1.main;
		pp1Main.simulationSpeed = 7;

		pp1.Stop();
		pp2.Stop();

		StartCoroutine(FadeOut());

		Destroy(gameObject, 12f);
	}

}
