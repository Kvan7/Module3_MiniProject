using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDriver : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject ventDriver;
	public GameObject shelfDriver;
	private bool playerIsBig;
	void Start()
	{
		ventDriver.SetActive(false);
		playerIsBig = GameObject.Find("Player").GetComponent<PlayerScaler>().isBig;
	}

	public void ShowDriver()
	{
		if (!shelfDriver.activeSelf && (!playerIsBig || Application.isEditor))
		{
			ventDriver.SetActive(true);
		}
	}
}
