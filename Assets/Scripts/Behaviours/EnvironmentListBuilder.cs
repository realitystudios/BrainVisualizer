using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentListBuilder : MonoBehaviour
{
    [SerializeField]
    private ToggleGroup m_ListContainer;

    [SerializeField]
    private GameObject m_ListItemPrefab;

    private void Awake()
    {
        m_ListContainer = GetComponentInChildren<ToggleGroup>(true);
        if (m_ListContainer != null)
        {
            StartCoroutine(GenerateEnvironmentList());
        }
        else
        {
            Debug.LogWarning("Cannot Find Toggle Group");
        }
    }

    private IEnumerator GenerateEnvironmentList()
    {
        yield return new WaitUntil(() => EnvironmentManager.Instance != null);

        foreach(EnvironmentManager.Skybox skybox in EnvironmentManager.Instance.Skyboxes)
        {
            VRUIRadio listItem = Instantiate(m_ListItemPrefab, m_ListContainer.transform).GetComponent<VRUIRadio>();
            listItem.isOn = (RenderSettings.skybox.name.Equals(skybox.skybox.name));
            listItem.group = m_ListContainer.GetComponent<ToggleGroup>();
            listItem.onValueChanged.AddListener((isOn) => {
                if (isOn)
                {
                    EnvironmentManager.Instance.ChangeSkybox(skybox.name);
                }
            });
            listItem.GetComponentInChildren<Text>().text = skybox.name;
        }
    }
}
