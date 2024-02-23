using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AttachDriver : MonoBehaviour
{
	public GameObject driverEquipped;
	private Interactable interactable;
	private ActuallyThrowable throwable;
	// Start is called before the first frame update
	void Start()
	{
		driverEquipped.SetActive(false);
		interactable = gameObject.GetComponent<Interactable>();
		throwable = gameObject.GetComponent<ActuallyThrowable>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!Application.isEditor)
		{
			bool playerIsBig = GameObject.Find("Player").GetComponent<PlayerScaler>().isBig;
			if (playerIsBig && interactable.enabled)
			{
				interactable.enabled = false;
				throwable.enabled = false;
			}
			else if (!playerIsBig && !interactable.enabled)
			{
				interactable.enabled = true;
				throwable.enabled = true;
			}
		}
	}

	public void AttachDriverToPlayer()
	{
		bool playerIsBig = GameObject.Find("Player").GetComponent<PlayerScaler>().isBig;
		if (!playerIsBig || Application.isEditor)
		{
			driverEquipped.SetActive(true);
			gameObject.GetComponent<Interactable>().attachedToHand.DetachObject(gameObject);
			gameObject.SetActive(false);
		}
	}
}
