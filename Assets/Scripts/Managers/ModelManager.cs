using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ModelManager : MonoBehaviour {

    [System.Serializable]
    public struct ModelData{
        public string Name;
        public Texture Icon;
        public GameObject Model;
    }

    [SerializeField]
    private List<ModelData> m_ModelPrefabs;

    private GameObject m_CurrentModel;
    private int m_CurrentModelIndex = 0;

    public List<ModelData> Models { get { return m_ModelPrefabs; } set { m_ModelPrefabs = value; } }

    public static ModelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if (m_ModelPrefabs == null)
        {
            m_ModelPrefabs = new List<ModelData>();
        }

        if (m_ModelPrefabs != null && m_ModelPrefabs.Count > 0)
        {
            LoadModel(0);
        }
    }

    public GameObject GetCurrentModel()
    {
        return m_CurrentModel;
    }

    public void ResetCurrentModel()
    {
        DestroyImmediate(m_CurrentModel);   
        m_CurrentModel = Instantiate(m_ModelPrefabs[m_CurrentModelIndex].Model, transform);
    }

    public void LoadModel(string modelName){
        LoadModel(m_ModelPrefabs.IndexOf(
            m_ModelPrefabs.FirstOrDefault((model) => model.Name.Equals(modelName))
        ));
    }

    public void LoadModel(int modelId)
    {
        if (m_CurrentModel != null)
        {
            Destroy(m_CurrentModel);
        }

        m_CurrentModelIndex = modelId;
        m_CurrentModel = Instantiate(m_ModelPrefabs[m_CurrentModelIndex].Model, transform);
        m_CurrentModel.name = m_ModelPrefabs[m_CurrentModelIndex].Model.name;
    }
}
