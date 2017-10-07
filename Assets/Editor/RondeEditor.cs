using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Ronde))]
public class RondeEditor : Editor {

	Ronde	r;

	void OnEnable()
	{
		r = target as Ronde;
	}

	public override void OnInspectorGUI()
	{
		base.DrawDefaultInspector();
	}

	public void OnSceneGUI()
	{
		if (r.list.Count == 0)
			r.list.Add(Vector2.zero);

		Handles.color = Color.green;
		for (int i = 0; i < r.list.Count; i++)
			r.list[i] = Handles.FreeMoveHandle(r.list[i], Quaternion.identity, .1f, Vector3.zero, Handles.DotHandleCap);
		
		if (!EditorApplication.isPlaying)	
			r.gameObject.transform.position = r.list[0];

		if (r.showPath)
		{
			Vector3[] arr = new Vector3[r.list.Count + 1];		
			for (int i = 0; i < r.list.Count; i++)
				arr[i] = r.list[i];
			arr[r.list.Count] = r.list[0];
			Handles.DrawAAPolyLine(arr);
		}
	}
}
