using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectManipulator : MonoBehaviour {

    VRTK_ControllerEvents m_Controller;

    float m_XPrev, m_YPrev;

	// Use this for initialization
	void Start () {
        m_Controller = GetComponent<VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {
        //ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.back, Mathf.Lerp(m_Controller.GetTouchpadAxis().x, m_XPrev, Time.deltaTime));
        //ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.up, Mathf.Lerp(m_Controller.GetTouchpadAxis().y, m_YPrev, Time.deltaTime));

        Debug.Log(m_Controller.GetTouchpadAxis().x - m_XPrev);

        m_XPrev = m_Controller.GetTouchpadAxis().x;
        m_YPrev = m_Controller.GetTouchpadAxis().y;
	}
}
