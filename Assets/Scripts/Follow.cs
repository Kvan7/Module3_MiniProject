using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;

	// Update is called once per frame
	void Update()
	{
		// Have this game object look at player
		transform.LookAt(target);
		// add offset rotation
		transform.Rotate(offset);
	}
}
