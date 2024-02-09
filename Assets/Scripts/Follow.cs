using UnityEngine;

public class Follow : MonoBehaviour
{
	// player's position
	public Transform playerFallback;
	// player's rotation
	public Transform playerRotationFallback;

	public Transform player;
	// Fixed height above the player
	public float heightAbovePlayer = 2.0f; // Set this to the desired height above the player
										   // Distance in front of the player
	public float distanceInFrontOfPlayer = 0.5f;
	// Rotation offset for the UI object
	public float rotationOffset = 0.0f;
	private bool useFallback = true;
	private void Start()
	{
		// Check if a VR headset is detected and active.
		if (UnityEngine.XR.XRSettings.isDeviceActive)
		{
			// A VR headset is detected, so we don't use the fallback mode.
			useFallback = false;
		}
		else
		{
			// No VR headset is detected, so we use the fallback mode.
			useFallback = true;
		}
	}


	void Update()
	{
		Transform positionUse = useFallback ? playerFallback : player;
		Transform rotationUse = useFallback ? playerRotationFallback : player;
		// Calculate the offset in front of the player based on the player's rotation
		Vector3 forwardOffset = rotationUse.forward * distanceInFrontOfPlayer;

		// Update the position to be directly above the player at a fixed height and offset in front
		// It will now follow the player's y position changes
		transform.position = new Vector3(
			positionUse.position.x + forwardOffset.x,
			positionUse.position.y + heightAbovePlayer,
			positionUse.position.z + forwardOffset.z
		);

		// Extract only the y-component of the player's rotation for the UI object's rotation
		float yRotation = rotationUse.eulerAngles.y;

		// Set the y-rotation to follow the player's y-axis rotation, and add the rotation offset to the x-axis
		transform.rotation = Quaternion.Euler(rotationOffset, yRotation, 0);
	}
}
