using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActivateLazer : MonoBehaviour
{
	public SteamVR_Action_Boolean lazerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "GrabLazer");
	private Hand hand;
	private GameObject lazer;

	private void Start()
	{
		hand = GetComponent<Hand>();
		// Get the child GameObject with the name "Lazer"
		lazer = gameObject.GetNamedChild("Lazer");
		lazer.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (lazerAction[hand.handType].stateDown)
		{
			lazer.SetActive(true);
		}
		if (lazerAction[hand.handType].stateUp)
		{
			lazer.SetActive(false);
		}
	}
}
