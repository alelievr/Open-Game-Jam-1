using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_particule : MonoBehaviour {

	ParticleSystem particlesystem;
	// Use this for initialization
	void Start () {
		ParticleSystem particlesystem = GetComponent<ParticleSystem>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate ()
	{
		if (!particlesystem)
			particlesystem = GetComponent<ParticleSystem>();		
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particlesystem.particleCount];
		int count = particlesystem.GetParticles(particles);
		for(int i = 0; i < count; i++)
		{
			Vector3 v = transform.position - particles[i].position;
			// float xVel = (particles[i].remainingLifetime / particles[i].startLifetime) * distance;
			particles[i].velocity += v * Time.deltaTime * 3;
			// particlesystem.main.simulationSpace = ParticleSystemSimulationSpace.Local;
		}
	
		particlesystem.SetParticles(particles, count);
	}
}
