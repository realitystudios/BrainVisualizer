using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectManipulator : MonoBehaviour {

    VRTK_ControllerEvents m_Controller;

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
            float m_TmpVector;

            if (m_Controller.GetTouchpadAxis().x < -0.01 || m_Controller.GetTouchpadAxis().x > 0.01)
            {
                m_TmpVector = m_Controller.GetTouchpadAxis().x - m_PrevVector.x;
            } else
            {
                m_TmpVector = m_PrevVector.x;
            }

            if (m_TmpVector < -0.01)
            {
                m_XVector -= Mathf.Abs(m_TmpVector);
                Debug.Log("Vector (Negative): " + m_XVector);
                m_TargetRotation *= Quaternion.AngleAxis(m_XVector * 5, Vector3.back);
                ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                    ModelManager.Instance.GetCurrentModel().transform.rotation,
                    m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime
                );
                Debug.Log("rotated");
            }
            else if (m_TmpVector > 0.01)
            {
                m_XVector += Mathf.Abs(m_TmpVector);
                Debug.Log("Vector (Positive): " + m_XVector);
                m_TargetRotation *= Quaternion.AngleAxis(m_XVector * 5, Vector3.back);
                ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                    ModelManager.Instance.GetCurrentModel().transform.rotation,
                    m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime
                );
                Debug.Log("rotated");
            }
            
            m_TmpVector = m_Controller.GetTouchpadAxis().y - m_PrevVector.y;

            /*if (m_TmpVector < -0.01)
            {
                m_YVector -= Mathf.Abs(m_TmpVector);
                Debug.Log("Vector (Negative): " + m_XVector);
                m_TargetRotation *= Quaternion.AngleAxis(m_YVector, Vector3.up);
                ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                    ModelManager.Instance.GetCurrentModel().transform.rotation,
                    m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime
                );
                Debug.Log("rotated");
            }
            else if (m_TmpVector > 0.01)
            {
                m_YVector += Mathf.Abs(m_TmpVector);
                Debug.Log("Vector (Positive): " + m_YVector);
                m_TargetRotation *= Quaternion.AngleAxis(m_YVector, Vector3.up);
                ModelManager.Instance.GetCurrentModel().transform.rotation = Quaternion.Lerp(
                    ModelManager.Instance.GetCurrentModel().transform.rotation,
                    m_TargetRotation, 25 * m_RotationSmoothing * Time.deltaTime
                );
                Debug.Log("rotated");
            }*/

            m_PrevVector = m_Controller.GetTouchpadAxis();
        }
    }
}
