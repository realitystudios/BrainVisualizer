using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private VRUIColorPalette m_Pallete;
    [SerializeField]
    private OvrAvatar m_OVRAvatar;
    [SerializeField]
    private GameObject m_ControllerTooltip;

    [SerializeField]
    private GameObject m_DarkThemeIcon;
    [SerializeField]
    private GameObject m_ControllerActiveIcon;

    private bool m_DarkThemeActive = false;
    private bool m_ControllersActive = true;

    private void Start()
    {
        m_DarkThemeActive = m_Pallete.isDarkTheme;
        m_DarkThemeIcon.SetActive(m_DarkThemeActive);

        m_ControllersActive = m_OVRAvatar.StartWithControllers;
        m_OVRAvatar.ShowControllers(m_ControllersActive);
        m_ControllerActiveIcon.SetActive(m_ControllersActive);
    }

    public void ToggleDarkMode()
    {
        m_DarkThemeActive = !m_DarkThemeActive;
        m_Pallete.isDarkTheme = m_DarkThemeActive;
        m_Pallete.UpdateColors();

        if (m_DarkThemeIcon) 
        { 
            m_DarkThemeIcon.SetActive(m_DarkThemeActive);
        } 
        else
        {
            Debug.LogWarning("Dark Theme Icon not set");
        }
    }

    public void ToggleControllers()
    {
        m_ControllersActive = !m_ControllersActive;

        if (m_OVRAvatar)
        {
            m_OVRAvatar.ShowControllers(m_ControllersActive);
        }

        if (m_ControllerTooltip)
        {
            m_ControllerTooltip.SetActive(m_ControllersActive);
        }

        if (m_ControllerActiveIcon)
        {
            m_ControllerActiveIcon.SetActive(m_ControllersActive);
        } 
        else
        {
            Debug.LogWarning("Controller not set");
        }
    }
}
