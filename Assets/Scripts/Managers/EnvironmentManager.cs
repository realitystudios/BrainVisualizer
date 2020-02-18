using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance;
    
    [System.Serializable]
    public struct Skybox
    {
        public string name;
        public Material skybox;
    }

    [SerializeField]
    private List<Skybox> m_Skyboxes;

    public delegate void SkyboxUpdated(string name);
    public event SkyboxUpdated OnSkyboxUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        OnSkyboxUpdated?.Invoke(m_Skyboxes.FirstOrDefault(i => i.skybox == RenderSettings.skybox).name);
    }

    public void ChangeSkybox(string skyboxName)
    {
        Skybox selectedSkybox = m_Skyboxes.FirstOrDefault(i => i.name.Equals(skyboxName));

        if (RenderSettings.skybox != selectedSkybox.skybox)
        {
            RenderSettings.skybox = selectedSkybox.skybox;
            OnSkyboxUpdated?.Invoke(skyboxName);
        }
    }

}
