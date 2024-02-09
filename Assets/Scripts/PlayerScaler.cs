using UnityEngine;

public class PlayerScaler : MonoBehaviour
{
	public Transform vrRig; // Assign your VR Rig's parent object here in the inspector
	public Transform[] otherToScale; // Assign any other objects you want to scale here in the inspector
	public float smallScale = 0.1f; // The scale for being "really small"
	public float largeScale = 2f; // The scale for being "really big"
	private bool isSmall = true; // Initial size state
	private Vector3 bigLocation;
	private Vector3 smallLocation;
	private void Start()
	{
		ToggleSize();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode.Space to your desired input
		{
			ToggleSize();
		}
	}

	void ToggleSize()
	{
		if (isSmall)
		{
			// Save small location before changing size
			smallLocation = vrRig.position;

			// Change to large size
			vrRig.localScale = Vector3.one * largeScale;
			foreach (Transform t in otherToScale)
			{
				t.localScale = Vector3.one * largeScale;
			}

			// Restore to big location after changing size
			vrRig.position = bigLocation;
			isSmall = false;
		}
		else
		{
			// Save big location before changing size
			bigLocation = vrRig.position;

			// Change to small size
			vrRig.localScale = Vector3.one * smallScale;
			foreach (Transform t in otherToScale)
			{
				t.localScale = Vector3.one * smallScale;
			}

			// Restore to small location after changing size
			vrRig.position = smallLocation;
			isSmall = true;
		}
	}
}
