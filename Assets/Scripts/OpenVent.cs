using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class OpenVent : MonoBehaviour
{

	// Objects to change on trigger
	public GameObject screw;
	public GameObject screw2;
	public GameObject screw3;
	public GameObject screw4;
	public GameObject ventCover;
	public GameObject teleportArea;
	public GameObject teleportArea2;
	private Interactable interactable;
	private bool isVentOpen = false;
	// private ActuallyThrowable throwable;
	// Start is called before the first frame update
	void Start()
	{
		interactable = gameObject.GetComponent<Interactable>();
		// throwable = gameObject.GetComponent<ActuallyThrowable>();
	}

	private void HandHoverUpdate(Hand hand)
	{
		GrabTypes startingGrabType = hand.GetGrabStarting();
		Debug.Log("HandHoverUpdate");
		if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None && !isVentOpen)
		{
			OpenTheVent();
			isVentOpen = true;
		}
	}

	public void OpenTheVent()
	{
		// Change the objects
		screw.AddComponent<BoxCollider>();
		screw.AddComponent<Rigidbody>();
		screw2.AddComponent<BoxCollider>();
		screw2.AddComponent<Rigidbody>();
		screw3.AddComponent<BoxCollider>();
		screw3.AddComponent<Rigidbody>();
		screw4.AddComponent<BoxCollider>();
		screw4.AddComponent<Rigidbody>();
		ventCover.AddComponent<BoxCollider>();
		ventCover.AddComponent<Rigidbody>();
		teleportArea.GetComponent<TeleportArea>().locked = false;
		teleportArea2.GetComponent<TeleportArea>().locked = false;
	}
}
