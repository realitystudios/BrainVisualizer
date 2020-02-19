using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    [SerializeField]
    private Smartwatch m_Watch;

    [SerializeField, Range(0, 1)]
    private float m_Cooldown = 0.5f;

    private bool IsInteractable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (IsInteractable)
        {
            if (other.name.Contains("hands:b_l_index_ignore") || other.name.Contains("hands:b_r_index_ignore"))
            {
                IsInteractable = false;
                m_Watch.ToggleMenu();
                StartCoroutine(ButtonCooldown());

            }
        }
    }

    private IEnumerator ButtonCooldown()
    {
        yield return new WaitForSeconds(m_Cooldown);

        IsInteractable = true;
    }
}
