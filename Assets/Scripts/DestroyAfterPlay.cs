using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
	private ParticleSystem ps;

	// Start is called before the first frame update
	void Start()
	{
		ps = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		// Check if the particle system has stopped playing
		if (!ps.IsAlive())
		{
			// Destroy the game object this script is attached to
			Destroy(gameObject);
		}
	}
}
