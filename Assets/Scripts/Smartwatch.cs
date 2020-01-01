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
    [SerializeField]
    private TextMeshProUGUI date;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time.SetText(
            DateTime.Now.ToString("HH:mm")
        );

        date.SetText(
            DateTime.Now.ToString("ddd d MMM")
        );
    }
}
