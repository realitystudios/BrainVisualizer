using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private OvrAvatar m_OVRAvatar;

    [SerializeField]
    private GameObject m_ControllerTooltip;

    [SerializeField]
    private VRUICheckbox m_ControllerCheckbox;
    
    [SerializeField]
    private VRUICheckbox m_DarkModeCheckbox;

    public OvrAvatar OvrAvatar { get { return m_OVRAvatar; } set { m_OVRAvatar = value; } }

    private void Start()
    {
        m_OVRAvatar = FindObjectOfType<OvrAvatar>();
        
        m_DarkModeCheckbox.onValueChanged.AddListener((state) => { ToggleDarkMode(state); }) ;
        StartCoroutine(SetDarkMode());
        m_ControllerCheckbox.onValueChanged.AddListener((state) => { ToggleControllers(state); });
        StartCoroutine(SetControllerState());
    }

    private IEnumerator SetDarkMode()
    {
        yield return new WaitUntil(() => VRUIColorPalette.Instance != null);
        m_DarkModeCheckbox.isOn = VRUIColorPalette.Instance.isDarkTheme;
    }

    private IEnumerator SetControllerState()
    {
        yield return new WaitUntil(() => m_OVRAvatar != null);
        m_ControllerCheckbox.isOn = m_OVRAvatar.StartWithControllers;
    }

    public void ToggleDarkMode(bool state)
    {
        if (VRUIColorPalette.Instance)
        {
            VRUIColorPalette.Instance.isDarkTheme = state;
            VRUIColorPalette.Instance.UpdateColors();
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
