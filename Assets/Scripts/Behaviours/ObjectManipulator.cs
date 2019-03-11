using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectManipulator : MonoBehaviour {

    private VRTK_ControllerEvents m_Controller;

    private Vector2 m_PrevVector;
    private float m_XVector, m_YVector;

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
        if (m_Controller.touchpadTouched)
        {

            ModelManager.Instance.GetCurrentModel().transform.Rotate(Vector3.back, m_Controller.GetTouchpadAxis().x * 40 * Time.deltaTime);

            /*Vector2 m_TmpVector;

            Debug.Log(m_XVector);

            if (m_Controller.GetTouchpadAxis().x < -0.01 || m_Controller.GetTouchpadAxis().x > 0.01)
            {
                m_TmpVector.x = m_Controller.GetTouchpadAxis().x - m_PrevVector.x;
            }
            else
            {
                m_TmpVector.x = m_PrevVector.x;
            }

            Mathf.Clamp(m_TmpVector.x, -1, 1);

            if (m_TmpVector.x < -0.01)
            {
                m_XVector -= Mathf.Abs(m_TmpVector.x * 5);
                if (m_XVector < 0)
                {
                    m_XVector += 360;
                }
                Debug.Log("Vector (Negative): " + m_XVector);
                m_TargetRotation *= Quaternion.AngleAxis(m_XVector * 5, Vector3.back);
                ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                    ModelManager.Instance.GetCurrentModel().transform.rotation,
                    m_TargetRotation, 5 * m_RotationSmoothing * Time.deltaTime
                );
            }
            else if (m_TmpVector.x > 0.01)
            {
                m_XVector += Mathf.Abs(m_TmpVector.x * 5);
                if (m_XVector > 360)
                {
                    m_XVector -= 360;
                }
                Debug.Log("Vector (Positive): " + m_XVector);
                m_TargetRotation *= Quaternion.AngleAxis(m_XVector * 5, Vector3.back);
                ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                    ModelManager.Instance.GetCurrentModel().transform.rotation,
                    m_TargetRotation, 5 * m_RotationSmoothing * Time.deltaTime
                );
            }
            //}
            
            /*if (m_Controller.GetTouchpadAxis().x < 0.1 && m_Controller.GetTouchpadAxis().x > -0.1)
            {
                if (m_Controller.GetTouchpadAxis().y < -0.01 || m_Controller.GetTouchpadAxis().y > 0.01)
                {
                    m_TmpVector.y = m_Controller.GetTouchpadAxis().y - m_PrevVector.y;
                }
                else
                {
                    m_TmpVector.y = m_PrevVector.y;
                }

                if (m_TmpVector.y < -0.01)
                {
                    if (m_YVector < -359)
                    {
                        m_YVector = 360;
                    }
                    m_YVector -= Mathf.Abs(m_TmpVector.y);
                    Debug.Log("Vector (Negative): " + m_XVector);
                    m_TargetRotation *= Quaternion.AngleAxis(m_YVector * 5, Vector3.up);
                    ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                        ModelManager.Instance.GetCurrentModel().transform.rotation,
                        m_TargetRotation, 5 * m_RotationSmoothing * Time.deltaTime
                    );
                }
                else if (m_TmpVector.y > 0.01)
                {
                    if (m_YVector > 359)
                    {
                        m_YVector = -360;
                    }
                    m_YVector += Mathf.Abs(m_TmpVector.y);
                    Debug.Log("Vector (Positive): " + m_XVector);
                    m_TargetRotation *= Quaternion.AngleAxis(m_YVector * 5, Vector3.up);
                    ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                        ModelManager.Instance.GetCurrentModel().transform.rotation,
                        m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime
                    );
                }
            }*/

            //m_PrevVector = m_Controller.GetTouchpadAxis();
        }
    }
}
