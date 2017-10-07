using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public List< Vector2 >	list = new List< Vector2 >();
	public float			speed = 1f;
	private int				index;

	public bool		canMove = true;

	public	bool	showPath = false;
	// Use this for initialization
	void Start () {
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!canMove)
			return ;
		
		if ((list [index] - (Vector2)transform.position).magnitude > 0.01)
			transform.position = Vector3.MoveTowards (transform.position, list [index], speed * Time.deltaTime);
		else if (index < list.Count - 1)
			index++;
		else
			index = 0;
	}
}
