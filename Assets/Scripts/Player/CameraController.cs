using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Rect		worldLimit;
	public Rect		deadZone;
	public float	deadZoneSize = 3;

	public float 	cameraDist = -10;
	Transform		playerTransform;
	Vector3 currentVelocity = new Vector3(0, 0, 0);
	public float smoothTime = 0.5f;
	public float smoothTimeWorld = 0.5f;
	// public float maxSpeed = 50f;

	bool	followPlayer = false;
	float camHalf;
	Vector3 camCorrection;


	void Start()
	{	
		// StartCoroutine(LateStart(1));
		playerTransform = Object.FindObjectOfType< PlayerController >().transform;
		camHalf = GetComponent<Camera>().orthographicSize;
	}
	
	// IEnumerator LateStart(float waitTime)
	// {
	// 	yield return new WaitForSeconds(waitTime);
	// 	if (gameObject.GetComponent<PlayerController>().transform != null)
	// 		playerTransform = GameObject.Find("player").GetComponent<PlayerController>().transform;
	// }


	bool PlayerOutOfDeadzone()
	{
		if (!deadZone.Contains(playerTransform.position))
		// if ((playerTransform.position.x < deadZone.xMin) ||
		// ((playerTransform.position.x > deadZone.xMax)) ||
		// ((playerTransform.position.y < deadZone.yMin)) ||
		// ((playerTransform.position.y > deadZone.yMax)))
			return (true);
		return (false);
	}

	bool CheckFollowCondition()
	{
		if ((playerTransform.position.x < transform.position.x + 0.5) &&
		(playerTransform.position.x > transform.position.x - 0.5) &&
		(playerTransform.position.y < transform.position.y + 0.5) &&
		(playerTransform.position.y > transform.position.y - 0.5))
			return (true);
		return (false);
	}

	void RectUpdate()
	{
		deadZone.xMax = transform.position.x + deadZoneSize;
		deadZone.xMin = transform.position.x - deadZoneSize;
		deadZone.yMax = transform.position.y + deadZoneSize;
		deadZone.yMin = transform.position.y - deadZoneSize;
	}

	// void CameraInBound()
	// {
	// 	Vector3 moveTo = Vector3.zero;
	// 	float camHalf = GetComponent<Camera>().orthographicSize;
	// 	Vector3 camUp = transform.position + Vector3.up * camHalf;
	// 	Vector3 camDown = transform.position - Vector3.up * camHalf;
	// 	Vector3 camRight = transform.position + Vector3.right * camHalf / 9 * 16;
	// 	Vector3 camLeft = transform.position - Vector3.right * camHalf / 9 * 16;
	// 	if (!worldLimit.Contains(camUp))

	// 	if (!worldLimit.Contains(camDown))

	// 	if (!worldLimit.Contains(camRight))

	// 	if (!worldLimit.Contains(camLeft))
	// }

	
	void LateUpdate () 
	{
		// camHalf = GetComponent<Camera>().orthographicSize;
// Vector3 moveTo;	
// 		moveTo = playerTransform.position;
// 		moveTo.z = -10;
// 		transform.position = moveTo;
		RectUpdate();
		if (PlayerOutOfDeadzone())
		{
			followPlayer = true;
		}
		
		if (followPlayer)
		{
			Vector3 moveTo;
			// Vector3 camUp = transform.position + Vector3.up * camHalf;
		//	Debug.DrawLine(transform.position, camUp, Color.red, 1f);
			// Vector3 camDown = transform.position - Vector3.up * camHalf;
			// Vector3 camRight = transform.position + Vector3.right * camHalf / 9 * 16;
			// Vector3 camLeft = transform.position - Vector3.right * camHalf / 9 * 16;
		// //	Debug.DrawLine(transform.position, camUp, Color.red, 1f);
		// 	// Debug.DrawLine(transform.position, camLeft, Color.red, 1f);
		// 	// Debug.DrawLine(transform.position, camRight , Color.blue, 1f);
			
			// camCorrection = Vector3.zero;
			// if (!worldLimit.Contains(camRight))
			// {
			// 	Debug.Log("nocamright");
			// 	camCorrection.x = camLeft.x - worldLimit.xMax;
			// 	// moveTo.x = (transform.position.x - moveTo.x > 0f)? moveTo.x - 2 * (transform.position.x - moveTo.x): moveTo.x;
			// }
			// else if (!worldLimit.Contains(camLeft))
			// {
			// 	Debug.Log("nocamleft");
			// 	camCorrection.x = camRight.x - worldLimit.xMin;
			// 	// moveTo.x = (transform.position.x - moveTo.x < 0f)? moveTo.x - 2 * (transform.position.x - moveTo.x): moveTo.x;
			// }
			// if (!worldLimit.Contains(camUp))
			// {
			// 	Debug.Log("nocamup");
			// 	camCorrection.y = camDown.y - worldLimit.yMax;
			// }
			// else if (!worldLimit.Contains(camDown))
			// {
			// 	Debug.Log("nocamdown");
			// 	camCorrection.y = camUp.y - worldLimit.yMin;
			// }
		// 	Debug.Log(camCorrection);
		//	 Debug.Log(currentVelocity.magnitude);
			moveTo = Vector3.SmoothDamp(transform.position, playerTransform.position , ref(currentVelocity), smoothTime);
			// Debug.Log(moveTo);

			moveTo.x = ((moveTo - (Vector3.right * camHalf / 9 * 16)).x - worldLimit.xMin < 0.001)? transform.position.x : moveTo.x;
			moveTo.x = (worldLimit.xMax - (moveTo + (Vector3.right * camHalf / 9 * 16)).x < 0.001)? transform.position.x : moveTo.x;
		
			moveTo.y = ((moveTo - (Vector3.up * camHalf)).y - worldLimit.yMin < 0.001)? transform.position.y : moveTo.y;
			moveTo.y = (worldLimit.yMax - (moveTo + (Vector3.up * camHalf)).y < 0.001)? transform.position.y : moveTo.y;
			
			moveTo.z = cameraDist;
		//	Debug.Log(moveTo);
			transform.position = moveTo;
			// Debug.DrawLine(transform.position, playerTransform.position + camCorrection, Color.green, 1f);
			if (CheckFollowCondition())
			{
				currentVelocity = Vector3.zero;
				followPlayer = false;
			}
		}

	}

}
