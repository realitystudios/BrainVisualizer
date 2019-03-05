using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectManipulator : MonoBehaviour {

    VRTK_ControllerEvents m_Controller;

    private Quaternion m_TargetRotation;
    private float m_RotationSmoothing = 1.5f;

    // Use this for initialization
    void Start () {
        m_Controller = GetComponent<VRTK_ControllerEvents>();
        m_TargetRotation = ModelManager.Instance.GetCurrentModel().transform.rotation;
	}

    // Update is called once per frame
    void Update()
    {
        if (m_Controller.GetTouchpadAxis().x < 0)
        {
            m_TargetRotation *= Quaternion.AngleAxis(-20, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime);
            Debug.Log("rotated");
        }
        else if (m_Controller.GetTouchpadAxis().x > 0)
        {
            m_TargetRotation *= Quaternion.AngleAxis(20, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime);
            Debug.Log("rotated");
        }
    }
}
