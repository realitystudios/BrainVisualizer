using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerTrigger : MonoBehaviour
{
    [SerializeField, Range(0, 4)]
    private float m_ButtonCooldown = 1f;

    private Button m_Button;
    private BoxCollider m_Collider;
    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Button = GetComponent<VRUIControlBarButton>();
        if (!m_Button)
        {
            m_Button = GetComponent<Button>();
        }

        m_Collider = gameObject.AddComponent<BoxCollider>();
        m_Collider.isTrigger = true;
        m_Collider.size = new Vector3(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height, 2);
        
        m_Rigidbody = gameObject.AddComponent<Rigidbody>();
        m_Rigidbody.useGravity = false;
        m_Rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_Button.IsInteractable()) { 
            if (other.name.Contains("hands:b_l_index_ignore") || other.name.Contains("hands:b_r_index_ignore"))
            {
                m_Button.interactable = false;
                StartCoroutine(ButtonCooldown());
                m_Button.onClick.Invoke();
            }
        }
    }

    private IEnumerator ButtonCooldown()
    {
        yield return new WaitForSeconds(m_ButtonCooldown);

        m_Button.interactable = true;
    }
}
