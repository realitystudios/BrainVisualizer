using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private VRUIColorPalette m_Palette;
    [SerializeField]
    private OvrAvatar m_OVRAvatar;
    [SerializeField]
    private GameObject m_ControllerTooltip;

    [SerializeField]
    private VRUICheckbox m_ControllerCheckbox;
    [SerializeField]
    private VRUICheckbox m_DarkModeCheckbox;

    public VRUIColorPalette ColourPalette { get { return m_Palette; } set { m_Palette = value; } }
    public OvrAvatar OvrAvatar { get { return m_OVRAvatar; } set { m_OVRAvatar = value; } }

    private void Start()
    {
        Debug.Log(m_Palette);
        Debug.Log(m_OVRAvatar);

        m_DarkModeCheckbox.onValueChanged.AddListener((state) => { ToggleDarkMode(state); }) ;
        m_DarkModeCheckbox.isOn = m_Palette.isDarkTheme;

        m_ControllerCheckbox.onValueChanged.AddListener((state) => { ToggleControllers(state); });
        m_ControllerCheckbox.isOn = m_OVRAvatar.StartWithControllers;
    }

    public void ToggleDarkMode(bool state)
    {
        if (m_Palette)
        {
            m_Palette.isDarkTheme = state;
            m_Palette.UpdateColors();
        }
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
