using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor {

	CameraController		cameraController;

	void OnEnable()
	{
		cameraController = target as CameraController;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}

	public void OnSceneGUI()
	{
		Rect r = cameraController.deadZone;
		Handles.color = Color.red;
		Handles.DrawAAPolyLine(r.min, new Vector2(r.xMin, r.yMax), r.max, new Vector2(r.xMax, r.yMin), r.min);

		cameraController.deadZone.min = Handles.FreeMoveHandle(r.min, Quaternion.identity, 0.1f, Vector3.zero, Handles.DotHandleCap);
		cameraController.deadZone.max = Handles.FreeMoveHandle(r.max, Quaternion.identity, 0.1f, Vector3.zero, Handles.DotHandleCap);


		Rect wr = cameraController.worldLimit;
		Handles.color = Color.blue;

		Handles.DrawAAPolyLine(wr.min, new Vector2(wr.xMin, wr.yMax), wr.max, new Vector2(wr.xMax, wr.yMin), wr.min);

		cameraController.worldLimit.min = Handles.FreeMoveHandle(wr.min, Quaternion.identity, 0.1f, Vector3.zero, Handles.DotHandleCap);
		cameraController.worldLimit.max = Handles.FreeMoveHandle(wr.max, Quaternion.identity, 0.1f, Vector3.zero, Handles.DotHandleCap);
	}
}
