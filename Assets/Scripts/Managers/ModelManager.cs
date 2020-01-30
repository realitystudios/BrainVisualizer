﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_ModelPrefabs;

    private GameObject m_CurrentModel;
    private int m_CurrentModelIndex = 0;

    public List<GameObject> Models { get { return m_ModelPrefabs; } set { m_ModelPrefabs = value; } }

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
            m_ModelPrefabs = new List<GameObject>();
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
        m_CurrentModel = Instantiate(m_ModelPrefabs[m_CurrentModelIndex], transform);
    }

    public void LoadModel(int modelId)
    {
        if (m_CurrentModel != null)
        {
            Destroy(m_CurrentModel);
        }

        m_CurrentModelIndex = modelId;
        m_CurrentModel = Instantiate(m_ModelPrefabs[m_CurrentModelIndex], transform);
        m_CurrentModel.name = m_ModelPrefabs[m_CurrentModelIndex].name;
    }
}
