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
    private VRUICheckbox m_ControllerCheckbox;
    [SerializeField]
    private VRUICheckbox m_DarkModeCheckbox;

    private void Start()
    {
        m_DarkModeCheckbox.onValueChanged.AddListener((state) => { ToggleDarkMode(state); }) ;
        m_DarkModeCheckbox.isOn = m_Pallete.isDarkTheme;

        m_ControllerCheckbox.onValueChanged.AddListener((state) => { ToggleControllers(state); });
        m_ControllerCheckbox.isOn = m_OVRAvatar.StartWithControllers;
    }

    public void ToggleDarkMode(bool state)
    {
        m_Pallete.isDarkTheme = state;
        m_Pallete.UpdateColors();
    }

    public void ToggleControllers(bool state)
    {
        if (m_OVRAvatar)
        {
            m_OVRAvatar.ShowControllers(state);
        }

        if (m_ControllerTooltip)
        {
            m_ControllerTooltip.SetActive(state);
        }
    }
}
