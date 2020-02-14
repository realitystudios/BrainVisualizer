using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    [SerializeField]
    private Smartwatch m_Watch;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("hands:b_l_index_ignore") || other.name.Contains("hands:b_r_index_ignore"))
        {
            m_Watch.ToggleMenu();
        }
    }
}
