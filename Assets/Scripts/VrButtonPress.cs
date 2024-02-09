using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonPress : MonoBehaviour
{
	public GameObject ballPrefab; // Assign in the inspector
	public Transform spawnPoint; // Assign a spawn point
	private TextMesh generalText;
	private Interactable interactable;
	private void Awake()
	{
		// Debug.Log("Awake");
		var debugMesh = GetComponentInChildren<TextMesh>();
		if (debugMesh != null)
		{
			generalText = debugMesh;
			generalText.text = "No Hand Hovering";
		}
		else
		{
			Debug.Log("No TextMesh found");
		}
		interactable = this.GetComponent<Interactable>();

	}

	private void OnHandHoverBegin(Hand hand)
	{
		// Debug.Log("Hovering hand: " + hand.name);
		generalText.text = "Hovering hand: " + hand.name;
	}

	private void OnHandHoverEnd(Hand hand)
	{
		generalText.text = "No Hand Hovering";
	}

	private void HandHoverUpdate(Hand hand)
	{
		GrabTypes startingGrabType = hand.GetGrabStarting();
		bool isGrabEnding = hand.IsGrabEnding(this.gameObject);
		if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
		{
			// Instantiate the new object and position it at the hand's attachment point
			GameObject spawnedObject = Instantiate(ballPrefab, hand.transform.position, hand.transform.rotation) as GameObject;

			// Random material for object
			spawnedObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();

			// Make the balloon be deflated on initial spawn in by scaling it down
			spawnedObject.transform.localScale = new Vector3(0.1f, 0.3f, 0.3f);

			// Attach the spawned object to the hand
			hand.AttachObject(spawnedObject, startingGrabType);
		}
		else if (isGrabEnding)
		{

		}



	}

}
