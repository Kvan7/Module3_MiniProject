using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerScaler : MonoBehaviour
{
	public Transform vrRig; // Assign your VR Rig's parent object here in the inspector
	public Transform[] otherToScale; // Assign any other objects you want to scale here in the inspector
	public float smallScale = 0.1f; // The scale for being "really small"
	public float largeScale = 2f; // The scale for being "really big"
	public Transform smallStartLocation;
	public Transform bigStartLocation;
	private bool isBig = false; // Initial size state
	private Vector3 bigLocation;
	private Vector3 smallLocation;
	private TeleportArea[] smallTeleportZones;
	private TeleportArea[] bigTeleportZones;
	private TeleportArea[] movableTeleportZones;
	private void Start()
	{
		bigLocation = bigStartLocation.position;
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
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode.Space to your desired input
		{
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
		Debug.Log("isBig:" + isBig);
		foreach (ActuallyThrowable throwable in FindObjectsOfType<ActuallyThrowable>())
		{
			if (!throwable.gameObject.CompareTag("SmallInteract"))
			{
				throwable.enabled = isBig;
				throwable.gameObject.GetComponent<Interactable>().highlightOnHover = isBig;
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
