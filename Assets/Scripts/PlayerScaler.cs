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
	public TeleportArea[] smallTeleportZones;
	public TeleportArea[] bigTeleportZones;
	public TeleportArea[] movableTeleportZones;
	private void Start()
	{
		bigLocation = vrRig.position;
		vrRig.position = new Vector3(-5.67f, 0.1f, -4.54f);
		ChangeSize(smallScale);

		// GameObject[] teleportSmallObjects = GameObject.FindGameObjectsWithTag("SmallTeleportZone");
		// GameObject[] teleportBigObjects = GameObject.FindGameObjectsWithTag("BigTeleportZone");
		// smallTeleportZones = teleportSmallObjects.Select(x => x.GetComponentInChildren<TeleportArea>()).ToArray();
		// bigTeleportZones = teleportBigObjects.Select(x => x.GetComponentInChildren<TeleportArea>()).ToArray();

		ChangeTeleportZones(isSmall);
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
			zone.SetLocked(!isSmall);
		}
		foreach (TeleportArea zone in bigTeleportZones)
		{
			zone.SetLocked(isSmall);
		}
	}

	void SwapLocations(ref Vector3 location1, ref Vector3 location2)
	{
		Vector3 temp = location1;
		location1 = vrRig.position;
		location2 = temp;
	}

	void ToggleSize()
	{
		if (isSmall)
		{
			SwapLocations(ref smallLocation, ref bigLocation);
			ChangeSize(largeScale);
		}
		else
		{
			SwapLocations(ref bigLocation, ref smallLocation);
			ChangeSize(smallScale);
		}

		ChangeTeleportZones(isSmall);

		vrRig.position = isSmall ? smallLocation : bigLocation;
		isSmall = !isSmall;
	}
}
