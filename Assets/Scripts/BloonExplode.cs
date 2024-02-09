using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BloonExplode : MonoBehaviour
{
	public GameObject explosionEffect;
	public float activationDelay = 1.0f; // Delay in seconds before the ball can explode
	public float filledAmount = 0.0f;
	private bool isActive = false; // Indicates whether the ball can explode
	private Interactable interactable;

	private void Awake()
	{
		// StartCoroutine(ActivateAfterDelay(activationDelay));
		interactable = GetComponent<Interactable>();
	}

	private IEnumerator ActivateAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		isActive = true; // Now the ball can explode upon collision
	}

	public void Fill()
	{
		// NOTE: original bloon was scaled down by Vector3(0.1f, 0.3f, 0.3f);
		// Fill the ball with water, and activate it after a delay
		if (filledAmount < 1.0f)
		{
			filledAmount += 0.01f;
			// Scale the ball slowly back to Vector3(1.0f, 1.0f, 1.0f); to represent the filled amount
			transform.localScale += Vector3.one * 0.01f;
			if (filledAmount >= 1.0f)
			{
				StartCoroutine(ActivateAfterDelay(activationDelay));
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (isActive && collision.collider.CompareTag("Terrain"))
		{
			// Instantiate the explosion effect at the ball's position and rotation
			Instantiate(explosionEffect, transform.position, transform.rotation);

			// Optionally, destroy the ball upon collision
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isActive && other.CompareTag("PickUp"))
		{
			// Explosion logic when hitting a pickup
			Instantiate(explosionEffect, transform.position, transform.rotation);
			ScoreManager.Instance.AddScore(1);
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}