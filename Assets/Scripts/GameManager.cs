using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	//singleton
	public static GameManager instance { get; private set; }

	//number of gathered points in this level
	int				_points = 0;
	GUIManager		guiManager;
	public int		points { get { return _points; } set {
		_points = value;
		if (OnPointsModified != null)
			OnPointsModified(_points);
	} }

	public event Action< int >	OnPointsModified;

	void Awake()
	{
		instance = this;
	}
}
