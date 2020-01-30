using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WatchTests
    {
        Smartwatch m_Watch;
        WatchMenu m_WatchMenu;

        [SetUp]
        [UnitySetUp]
        public void Setup()
        {
            Camera camera = new GameObject().AddComponent<Camera>();

            m_Watch = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Watch")).GetComponent<Smartwatch>();

            m_WatchMenu = m_Watch.GetComponentInChildren<WatchMenu>();
            
            foreach (var canvas in m_WatchMenu.GetComponentsInChildren<Canvas>())
            {
                canvas.worldCamera = camera;
            }
        }

        [Test]
        public void Watch_ToggleMenu()
        {
            m_Watch.ToggleMenu();

            Assert.That(m_WatchMenu.gameObject.activeSelf == false);

            m_Watch.ToggleMenu();

            Assert.That(m_WatchMenu.gameObject.activeSelf == true);
        }

        [Test]
        public void Watch_ToggleModelMenu()
        {
            GameObject modelMenu = m_WatchMenu.transform.Find("VRUI-GridMenu").gameObject;

            m_WatchMenu.ToggleModelMenu();

            Assert.That(modelMenu.activeSelf == false);

            m_WatchMenu.ToggleModelMenu();

            Assert.That(modelMenu.activeSelf == true);
        }

        [TearDown]
        [UnityTearDown]
        public void TearDown()
        {
            Object.Destroy(m_Watch);
        }
    }
}
