using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public GUIPointActive[]	points;

	void OnEnable()
	{
		//attach events from game manager:
		GameManager.instance.OnPointsModified += OnPointsModifiedCallback;
	}

	void OnDisable()
	{
		//detach events from game manager:
		GameManager.instance.OnPointsModified -= OnPointsModifiedCallback;
	}

	void OnPointsModifiedCallback(int newPointCount)
	{
		points[newPointCount - 1].ActivePoint();
	}
	
	void Update ()
	{
	}
}
