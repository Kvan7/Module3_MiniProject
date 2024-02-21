using System;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerScaler : MonoBehaviour
{
	public Transform vrRig; // Assign your VR Rig's parent object here in the inspector
	public Transform[] otherToScale; // Assign any other objects you want to scale here in the inspector
	public float smallScale = 0.1f; // The scale for being "really small"
	public float largeScale = 2f; // The scale for being "really big"
	public Transform smallStartLocation;
	public Transform bigStartLocation;
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
	public GameObject? smallPositionMarker;
	public GameObject? bigPositionMarker;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
	public bool isBig { get; private set; } = false; // Initial size state
	private Vector3 bigLocation;
	private Vector3 smallLocation;
	private TeleportArea[] smallTeleportZones;
	private TeleportArea[] bigTeleportZones;
	private TeleportArea[] movableTeleportZones;
	public SteamVR_Action_Boolean changeSize = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "SizeChange");

	private void Start()
	{
		bigLocation = bigStartLocation.position;
		SetMarkers(isBig);
		vrRig.position = smallStartLocation.position;
		ChangeSize(smallScale);

		smallTeleportZones = GameObject.FindGameObjectsWithTag("SmallTeleportZone")
			.SelectMany(obj => obj.GetComponentsInChildren<TeleportArea>(true))
			.ToArray();

		bigTeleportZones = GameObject.FindGameObjectsWithTag("BigTeleportZone")
			.SelectMany(obj => obj.GetComponentsInChildren<TeleportArea>(true))
			.ToArray();

		movableTeleportZones = GameObject.FindGameObjectsWithTag("MovableTeleport")
			.SelectMany(obj => obj.GetComponentsInChildren<TeleportArea>(true))
			.ToArray();
		ChangeTeleportZones(isBig);
		ToggleInteractable(isBig);
		// changeSize.onStateDown += ToggleSize;

	}

	// private void ToggleSize(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	// {
	// 	Debug.Log("Toggling size from action");
	// 	ToggleSize();
	// }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode.Space to your desired input
		{
			ToggleSize();
		}
		if (changeSize.GetStateDown(SteamVR_Input_Sources.Any))
		{
			// Debug.Log("Toggling size from input source");
			ToggleSize();
		}
	}

	private void ChangeSize(float scale)
	{
		vrRig.localScale = Vector3.one * scale;
		foreach (Transform t in otherToScale)
		{
			t.localScale = Vector3.one * scale;
		}
		// Also change gravity
		Physics.gravity = new Vector3(0, -9.81f * scale, 0);
	}

	private void ToggleInteractable(bool isBig)
	{
		foreach (ActuallyThrowable throwable in FindObjectsOfType<ActuallyThrowable>())
		{
			if (!throwable.gameObject.CompareTag("SmallInteract"))
			{
				throwable.enabled = isBig;
				throwable.gameObject.GetComponent<Interactable>().highlightOnHover = isBig;
			}
		}
	}

	private void SetMarkers(bool isBig)
	{
		if (isBig)
		{
			bigPositionMarker?.SetActive(false);
			smallPositionMarker?.SetActive(true);
			// Put marker where player was
			if (smallPositionMarker != null)
			{
				smallPositionMarker.transform.position = vrRig.position;
			}
		}
		else
		{
			bigPositionMarker?.SetActive(true);
			smallPositionMarker?.SetActive(false);
			// Put marker where player was
			if (bigPositionMarker != null)
			{
				bigPositionMarker.transform.position = vrRig.position;
			}
		}

	}

	private void ChangeTeleportZones(bool isBig)
	{
		foreach (TeleportArea zone in smallTeleportZones)
		{
			zone.SetLocked(isBig);
		}
		foreach (TeleportArea zone in bigTeleportZones)
		{
			zone.SetLocked(!isBig);
		}
		foreach (TeleportArea zone in movableTeleportZones)
		{
			zone.SetLocked(isBig);
		}
	}

	void ToggleSize()
	{
		isBig = !isBig;
		ChangeTeleportZones(isBig);
		ToggleInteractable(isBig);
		SetMarkers(isBig);
		if (isBig)
		{
			smallLocation = vrRig.position;
			ChangeSize(largeScale);

			vrRig.position = bigLocation;
		}
		else
		{
			bigLocation = vrRig.position;
			ChangeSize(smallScale);

			vrRig.position = smallLocation;
		}
	}
}
