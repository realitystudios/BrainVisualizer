using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchMenu : MonoBehaviour
{
    [SerializeField]
    private VRUIColorPalette m_Palette;
    [SerializeField]
    private GameObject m_ModelMenu;
    [SerializeField]
    private GameObject m_SettingsMenu;
    [SerializeField]
    private GameObject m_EnvironmentMenu;

    private bool m_ModelMenuActive;
    private bool m_SettingsMenuActive;
    private bool m_EnvironmentMenuActive;

    private void Start()
    {
        m_ModelMenuActive = m_ModelMenu.activeSelf;
        m_SettingsMenuActive = m_SettingsMenu.activeSelf;
        m_EnvironmentMenuActive = m_EnvironmentMenu.activeSelf;
    }

    public void ToggleModelMenu()
    {
        if (m_EnvironmentMenuActive)
            ToggleEnvironmentMenu();
        if (m_SettingsMenuActive)
            ToggleSettingsMenu();

        m_ModelMenuActive = !m_ModelMenuActive;
        m_ModelMenu.SetActive(m_ModelMenuActive);

        m_Palette.UpdateColors();
    }

    public void ToggleEnvironmentMenu()
    {
        if (m_ModelMenuActive)
            ToggleModelMenu();
        if (m_SettingsMenuActive)
            ToggleSettingsMenu();

        m_EnvironmentMenuActive = !m_EnvironmentMenuActive;
        m_EnvironmentMenu.SetActive(m_EnvironmentMenuActive);

        m_Palette.UpdateColors();
    }

    public void ToggleSettingsMenu()
    {
        if (m_ModelMenuActive)
            ToggleModelMenu();
        if (m_EnvironmentMenuActive)
            ToggleEnvironmentMenu();

        m_SettingsMenuActive = !m_SettingsMenuActive;
        m_SettingsMenu.SetActive(m_SettingsMenuActive);

        m_Palette.UpdateColors();
    }

    public void ExitApplication()
    {
        Application.Quit(0);
    }
}
