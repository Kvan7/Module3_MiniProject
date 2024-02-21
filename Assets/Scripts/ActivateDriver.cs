using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDriver : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject ventDriver;
	public GameObject shelfDriver;
	void Start()
	{
		ventDriver.SetActive(false);
	}

	public void ShowDriver()
	{
		Debug.Log("ShowDriver");
		if (!shelfDriver.activeSelf)
		{
			ventDriver.SetActive(true);
		}
	}
}
