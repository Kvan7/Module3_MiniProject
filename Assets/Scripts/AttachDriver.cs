using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AttachDriver : MonoBehaviour
{
	public GameObject driverEquipped;
	// Start is called before the first frame update
	void Start()
	{
		driverEquipped.SetActive(false);
	}

	public void AttachDriverToPlayer()
	{
		driverEquipped.SetActive(true);
		gameObject.GetComponent<Interactable>().attachedToHand.DetachObject(gameObject);
		gameObject.SetActive(false);
	}
}
