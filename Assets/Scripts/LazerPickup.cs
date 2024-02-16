using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LazerPickup : MonoBehaviour
{
	public SteamVR_Action_Boolean lazerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "GrabGrip");
	private Hand hand;

	// Add references for the two materials
	public Material defaultMaterial;
	public Material interactMaterial;

	private MeshRenderer meshRenderer;

	private void Start()
	{
		hand = gameObject.GetComponentInParent<Hand>();
		meshRenderer = GetComponent<MeshRenderer>();

		// Initialize with the default material
		if (meshRenderer != null && defaultMaterial != null)
		{
			meshRenderer.material = defaultMaterial;
		}
	}

	private void OnTriggerEnter(Collider other)
	{

		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable == null)
		{
			interactable = other.GetComponentInParent<Interactable>();
		}

		if (interactable != null)
		{
			meshRenderer.material = interactMaterial;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable == null)
		{
			interactable = other.GetComponentInParent<Interactable>();
		}
		if (interactable != null)
		{
			meshRenderer.material = defaultMaterial;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable == null)
		{
			// If no Interactable component is found on the object, try finding it on the parent
			interactable = other.GetComponentInParent<Interactable>();
		}
		if (interactable != null && lazerAction[hand.handType].stateDown)
		{
			// If the interactable object is not already grabbed
			if (!interactable.attachedToHand)
			{
				// Attach the object to the hand
				hand.AttachObject(interactable.gameObject, GrabTypes.Grip);
			}
		}
	}


	void Update()
	{

	}
}
