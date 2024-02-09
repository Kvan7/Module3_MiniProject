using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBloon : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Bloon"))
		{
			other.gameObject.GetComponent<BloonExplode>().Fill();
		}
	}

}
