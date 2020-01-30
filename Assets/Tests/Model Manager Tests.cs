using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ModelManagerTests
    {
        ModelManager m_ModelManager;

        [SetUp]
        public void Setup()
        {
            m_ModelManager = new GameObject().AddComponent<ModelManager>();

            GameObject brainModel = Resources.Load<GameObject>("Prefabs/Brain");
            GameObject ventricleModel = Resources.Load<GameObject>("Prefabs/Ventricle");

            m_ModelManager.Models = new List<GameObject>()
            {
                brainModel,
                ventricleModel
            };
        }

        [Test]
        public void ModelManager_LoadObject()
        {
            m_ModelManager.LoadModel(0);

            Assert.That(m_ModelManager.GetCurrentModel().name, Does.Contain(m_ModelManager.Models[0].name));
        }

        [Test]
        public void ModelManager_ResetObject()
        {
            m_ModelManager.ResetCurrentModel();

            Assert.That(m_ModelManager.GetCurrentModel().name, Does.Contain(m_ModelManager.Models[0].name));
        }

        [TearDown]
        public void ModelManager_TearDown()
        {
            Object.Destroy(m_ModelManager.gameObject);
        }
    }
}
