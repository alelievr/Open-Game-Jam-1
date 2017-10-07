using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ronde : Stopmoving {
	public List<Vector2> list;
	public float speed;
	private int index;

	public	bool	showPath = false;
	// Use this for initialization
	void Start () {
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (base.Stop ())
			return;
		if ((list [index] - (Vector2)transform.position).magnitude > 0.01)
			this.transform.position = Vector3.MoveTowards (transform.position, list [index], speed * Time.deltaTime);
		else if (index < list.Count - 1)
			index++;
		else
			index = 0;
	}
}
