using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotOVR : MonoBehaviour
{
	public float smooth = 1.5f;

	private Quaternion targetRotation;

	void Start()
	{
		targetRotation = transform.rotation;
	}

    private void Update()
    {
        Debug.Log("compared");
		if (OVRInput.Get(OVRInput.Button.Right))
        {
            targetRotation *= Quaternion.AngleAxis(-20, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 25 * smooth * Time.deltaTime);
            Debug.Log("rotated");
        }
		else if (OVRInput.Get(OVRInput.Button.Left))
		{
			targetRotation *= Quaternion.AngleAxis(20, Vector3.up);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 25 * smooth * Time.deltaTime);
			Debug.Log("rotated");
		}
    }
}
