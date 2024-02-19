using UnityEngine;
using Valve.VR.InteractionSystem;

public class DynamicTeleportArea : MonoBehaviour
{
	// Assuming each side of the cube has a child object with a TeleportArea component
	public TeleportArea[] sides; // Assign in inspector

	void Update()
	{
		UpdateTeleportableSide();
	}

	void UpdateTeleportableSide()
	{
		int topSideIndex = GetTopSideIndex();
		Debug.Log("Top side index: " + topSideIndex);
		for (int i = 0; i < sides.Length; i++)
		{
			sides[i].locked = i != topSideIndex;
		}
	}

	int GetTopSideIndex()
	{
		float maxDot = -Mathf.Infinity;
		int index = -1;
		for (int i = 0; i < sides.Length; i++)
		{
			float dot = Vector3.Dot(sides[i].transform.up, Vector3.up);
			if (dot > maxDot)
			{
				maxDot = dot;
				index = i;
			}
		}
		return index;
	}
}
