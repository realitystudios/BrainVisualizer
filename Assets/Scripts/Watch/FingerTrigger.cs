using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerTrigger : MonoBehaviour
{
    [SerializeField, Range(0, 4)]
    private float m_ButtonCooldown = 1f;

    private Selectable m_Selectable;
    private BoxCollider m_Collider;
    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Selectable = GetComponent<Selectable>();

        m_Collider = gameObject.AddComponent<BoxCollider>();
        m_Collider.isTrigger = true;
        StartCoroutine(SetColliderSize());

        m_Rigidbody = gameObject.AddComponent<Rigidbody>();
        m_Rigidbody.useGravity = false;
        m_Rigidbody.isKinematic = true;
    }

    private IEnumerator SetColliderSize()
    {
        yield return new WaitUntil(() => m_Collider != null);

        yield return new WaitUntil(() => GetComponent<RectTransform>().rect.width > 0);

        m_Collider.size = new Vector3(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_Selectable.IsInteractable()) {
            if (other.name.Contains("hands:b_l_index_ignore") || other.name.Contains("hands:b_r_index_ignore"))
            {
                Button button = m_Selectable.GetComponent<Button>();
                if (button)
                {
                    button.onClick.Invoke();
                }

                Toggle toggle = m_Selectable.GetComponent<Toggle>();
                if (toggle)
                {
                    toggle.isOn = !toggle.isOn;
                }

                Dropdown dropdown = m_Selectable.GetComponent<Dropdown>();
                if (dropdown)
                {
                    dropdown.Show();
                }

                m_Selectable.interactable = false;
                StartCoroutine(ButtonCooldown());
            }
        }
    }

    private IEnumerator ButtonCooldown()
    {
        yield return new WaitForSeconds(m_ButtonCooldown);

        m_Selectable.interactable = true;
    }
}
