using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerScaler : MonoBehaviour
{
	public Transform vrRig; // Assign your VR Rig's parent object here in the inspector
	public Transform[] otherToScale; // Assign any other objects you want to scale here in the inspector
	public float smallScale = 0.1f; // The scale for being "really small"
	public float largeScale = 2f; // The scale for being "really big"
	private bool isSmall = true; // Initial size state
	private Vector3 bigLocation;
	private Vector3 smallLocation;
	private TeleportArea[] smallTeleportZones;
	private TeleportArea[] bigTeleportZones;
	private TeleportArea[] movableTeleportZones;
	private void Start()
	{
		bigLocation = vrRig.position;
		vrRig.position = new Vector3(-5.67f, 0.1f, -4.54f);
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
		ChangeTeleportZones(!isSmall);
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
	}



	private void ChangeTeleportZones(bool isSmall)
	{
		foreach (TeleportArea zone in smallTeleportZones)
		{
			zone.SetLocked(isSmall);
		}
		foreach (TeleportArea zone in bigTeleportZones)
		{
			zone.SetLocked(!isSmall);
		}
		foreach (TeleportArea zone in movableTeleportZones)
		{
			zone.SetLocked(isSmall);
		}
	}

	void ToggleSize()
	{
		ChangeTeleportZones(isSmall);
		if (isSmall)
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
		isSmall = !isSmall;
	}
}
