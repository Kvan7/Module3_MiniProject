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
			Debug.Log(zone);
		}
		foreach (TeleportArea zone in bigTeleportZones)
		{
			zone.SetLocked(isSmall);
		}
	}

	void ToggleSize()
	{
		if (isSmall)
		{
			smallLocation = vrRig.position;
			ChangeSize(largeScale);

			vrRig.position = bigLocation;
			isSmall = false;
		}
		else
		{
			bigLocation = vrRig.position;
			ChangeSize(smallScale);

			vrRig.position = smallLocation;
			isSmall = true;
		}
		ChangeTeleportZones(isSmall);
	}
}
