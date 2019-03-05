using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public Teleporting_OculusGO teleporter;

	void Update () {

		if (OVRInput.GetDown(OVRInput.Button.One))
		{
			teleporter.ToggleDisplay(true);
		}

		if(OVRInput.GetUp(OVRInput.Button.One))
		{
			teleporter.Teleport();
			teleporter.ToggleDisplay(false);
		}
	}
}
