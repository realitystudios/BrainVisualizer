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
        float xVector = m_Controller.GetTouchpadAxis().x - m_XPrev;
        float yVector = m_Controller.GetTouchpadAxis().y - m_YPrev;

        /*if (xVector < 0)
        {
            ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.back, -0.1f);
        } else if (xVector > 0)
        {
            ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.back, 1.0f);
        }

        if (yVector < 0)
        {
            ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.back, -0.1f);
        }
        else if (yVector > 0)
        {
            ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.back, 1.0f);
        }*/

        if (m_Controller.GetTouchpadAxis().x - m_XPrev != 0) Debug.Log(m_Controller.GetTouchpadAxis().x - m_XPrev);

        m_XPrev = m_Controller.GetTouchpadAxis().x;
        m_YPrev = m_Controller.GetTouchpadAxis().y;
	}
}
