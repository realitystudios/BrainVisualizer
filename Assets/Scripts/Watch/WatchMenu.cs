using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchMenu : MonoBehaviour
{
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

        UpdateSkyboxMenu("Morning");
    }

    private void OnEnable()
    {
        StartCoroutine(SkyboxChangeListener());
    }

    private IEnumerator SkyboxChangeListener()
    {
        yield return new WaitUntil(() => EnvironmentManager.Instance != null);

        EnvironmentManager.Instance.OnSkyboxUpdated += UpdateSkyboxMenu;
    }

    private void UpdateSkyboxMenu(string name)
    {
        foreach (Button button in m_EnvironmentMenu.GetComponentsInChildren<Button>())
        {
            if (button.GetComponentInChildren<Text>().text.Equals(name))
            {
                button.transform.Find("Icon").gameObject.SetActive(true);
            }
            else
            {
                button.transform.Find("Icon").gameObject.SetActive(false);
            }
        }
    }

    public void ToggleModelMenu()
    {
        if (m_EnvironmentMenuActive)
            ToggleEnvironmentMenu();
        if (m_SettingsMenuActive)
            ToggleSettingsMenu();

        m_ModelMenuActive = !m_ModelMenuActive;
        m_ModelMenu.SetActive(m_ModelMenuActive);
    }

    public void ToggleEnvironmentMenu()
    {
        if (m_ModelMenuActive)
            ToggleModelMenu();
        if (m_SettingsMenuActive)
            ToggleSettingsMenu();

        m_EnvironmentMenuActive = !m_EnvironmentMenuActive;
        m_EnvironmentMenu.SetActive(m_EnvironmentMenuActive);
    }

    public void ToggleSettingsMenu()
    {
        if (m_ModelMenuActive)
            ToggleModelMenu();
        if (m_EnvironmentMenuActive)
            ToggleEnvironmentMenu();

        m_SettingsMenuActive = !m_SettingsMenuActive;
        m_SettingsMenu.SetActive(m_SettingsMenuActive);
    }

    public void ExitApplication()
    {
        Application.Quit(0);
    }
}
