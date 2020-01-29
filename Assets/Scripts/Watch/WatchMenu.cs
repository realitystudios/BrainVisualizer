using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ModelMenu;

    private bool m_ModelMenuActive;

    private void Start()
    {
        m_ModelMenuActive = m_ModelMenu.activeSelf;
    }

    private void Update()
    {
        transform.LookAt(VRTK.VRTK_DeviceFinder.HeadsetTransform());
    }

    public void ToggleModelMenu()
    {
        m_ModelMenu.SetActive(m_ModelMenuActive);

        m_ModelMenuActive = !m_ModelMenuActive;
    }

    public void ExitApplication()
    {
        Application.Quit(0);
    }
}
