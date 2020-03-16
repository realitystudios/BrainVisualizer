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
        VRUIColorPalette m_Palette;
        OvrAvatar m_OvrAvatar;

        [SetUp]
        [UnitySetUp]
        public void Setup()
        {
            Camera camera = new GameObject().AddComponent<Camera>();
            m_Palette = new GameObject().AddComponent<VRUIColorPalette>();
            m_OvrAvatar = new GameObject().AddComponent<OvrAvatar>();

            m_Watch = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Watch")).GetComponent<Smartwatch>();
            m_Watch.GetComponentInChildren<SettingsManager>(true).OvrAvatar = m_OvrAvatar;
            m_WatchMenu = m_Watch.GetComponentInChildren<WatchMenu>(true);
            
            foreach (var canvas in m_WatchMenu.GetComponentsInChildren<Canvas>(true))
            {
                canvas.worldCamera = camera;
            }
        }

        [Test]
        public void Watch_ToggleMenu()
        {
            bool active = m_Watch.gameObject.activeSelf;
            
            m_Watch.ToggleMenu();
            Assert.That(m_WatchMenu.gameObject.activeSelf == active);

            m_Watch.ToggleMenu();
            Assert.That(m_WatchMenu.gameObject.activeSelf == !active);
        }

        [Test]
        public void Watch_ToggleModelMenu()
        {
            Debug.Log(m_WatchMenu);
            GameObject modelMenu = m_WatchMenu.transform.Find("VRUI-GridMenu (Model)").gameObject;
            Debug.Log(modelMenu);
            bool active = modelMenu.activeSelf;

            m_WatchMenu.ToggleModelMenu();
            Assert.That(modelMenu.activeSelf == !active);

            m_WatchMenu.ToggleModelMenu();
            Assert.That(modelMenu.activeSelf == active);
        }

        [Test]
        public void Watch_ToggleEnvironmentsMenu()
        {
            GameObject modelMenu = m_WatchMenu.transform.Find("VRUI-RadioPanel (Environments)").gameObject;
            bool active = modelMenu.activeSelf;

            m_WatchMenu.ToggleEnvironmentMenu();
            Assert.That(modelMenu.activeSelf == !active);

            m_WatchMenu.ToggleEnvironmentMenu();
            Assert.That(modelMenu.activeSelf == active);
        }

        [Test]
        public void Watch_ToggleSettingsMenu()
        {
            GameObject modelMenu = m_WatchMenu.transform.Find("VRUI-CheckListPanel (Settings)").gameObject;
            bool active = modelMenu.activeSelf;

            m_WatchMenu.ToggleSettingsMenu();
            Assert.That(modelMenu.activeSelf == !active);

            m_WatchMenu.ToggleSettingsMenu();
            Assert.That(modelMenu.activeSelf == active);
        }

        [TearDown]
        [UnityTearDown]
        public void TearDown()
        {
            Object.Destroy(m_Watch);
            Object.Destroy(m_Palette);
            Object.Destroy(m_OvrAvatar);
        }
    }
}
