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


	public void OpenTheVent()
	{
		// Change the objects
		Debug.Log("OpenTheVent");
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
