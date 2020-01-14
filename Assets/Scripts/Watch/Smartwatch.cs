using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Smartwatch : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField]
    private GameObject m_Menu;

    [Header("Time Display")]
    [SerializeField]
    private TextMeshProUGUI time;

    private Coroutine m_TimeCoroutine;

    private bool m_MenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleMenu()
    {
        m_Menu.SetActive(m_MenuActive);

        m_MenuActive = !m_MenuActive;
    }

    private void OnEnable()
    {
        if (m_TimeCoroutine == null)
        {
            m_TimeCoroutine = StartCoroutine(StartWatch());
        }
    }

    private IEnumerator StartWatch()
    {
        for(; ; )
        {
            time.text = DateTime.Now.ToString("HH:mm") + "<br> <size=0.5>" + DateTime.Now.ToString("ddd d MMM") + "</size>";

            yield return new WaitForSecondsRealtime(15);
        }
    }

    private void OnDisable()
    {
        if (m_TimeCoroutine != null)
        {
            StopCoroutine(m_TimeCoroutine);
            m_TimeCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        if (m_TimeCoroutine != null)
        {
            StopCoroutine(m_TimeCoroutine);
            m_TimeCoroutine = null;
        }
    }
}
