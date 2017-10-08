using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markator : MonoBehaviour {

	public ParticleSystem			splatParticles;

	new ParticleSystem				particleSystem;
	List< ParticleCollisionEvent >	collisionEvents = new List< ParticleCollisionEvent >();

	new Camera			camera;

	void Start () {

		camera = Camera.main;
		particleSystem = GetComponent< ParticleSystem >();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));

			Debug.DrawLine(transform.position, mousePos);
			particleSystem.transform.LookAt(mousePos, Vector3.forward);

			particleSystem.Emit(1);
		}
	}
	
	void OnParticleCollision(GameObject other)
	{
		if (other.tag == "Enemy")
			Destroy(other);
		else
		{
			ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, other, collisionEvents);

			for (int i = 0; i < collisionEvents.Count; i++)
				EmitAtLocation(collisionEvents[i]);
		}
	}

	void EmitAtLocation(ParticleCollisionEvent pce)
	{
		splatParticles.transform.position = pce.intersection;
		splatParticles.transform.rotation = Quaternion.identity;
		splatParticles.Emit(1);
	}
}
