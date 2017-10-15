using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParticle : MonoBehaviour {

	public float			speed = 1f;
	public GameObject		destroyOnTrigger;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player")
			return ;
		
		if (destroyOnTrigger != null)
		{
			StartCoroutine(MoveTo(destroyOnTrigger.transform.position));
		}
	}

	IEnumerator MoveTo(Vector3 to)
	{
		while ((transform.position - to).magnitude > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, to, speed * Time.deltaTime);
			yield return null;
		}
		Destroy(destroyOnTrigger);
		Destroy(gameObject);
	}

}
