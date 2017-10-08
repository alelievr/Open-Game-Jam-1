using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markator : MonoBehaviour {

	public ParticleSystem			splatParticles;

	new ParticleSystem				particleSystem;
	List< ParticleCollisionEvent >	collisionEvents = new List< ParticleCollisionEvent >();

	void Start () {

		particleSystem = GetComponent< ParticleSystem >();
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
