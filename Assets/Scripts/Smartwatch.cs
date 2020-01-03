using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Smartwatch : MonoBehaviour
{
    [Header("DateTime Elements")]
    [SerializeField]
    private TextMeshProUGUI time;

    private Coroutine m_TimeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        time.richText = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        m_TimeCoroutine = StartCoroutine(StartWatch());
    }

    private IEnumerator StartWatch()
    {
        for(; ; )
        {
            time.text = DateTime.Now.ToString("HH:mm") + "<br> <size=0.5>" + DateTime.Now.ToString("ddd d MMM") + "</size>";

            yield return new WaitForSecondsRealtime(60);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(m_TimeCoroutine);
    }
}
