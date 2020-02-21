using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRUIDropdown : Dropdown
{
    private Color m_AccentColor = Color.white;
    
    [SerializeField]
    private Image m_Outline;
    [SerializeField]
    private Image m_Arrow;

    protected override void Awake()
    {
        base.Awake();
    }

    public void setColors(Color a)
    {
        m_AccentColor = a;

        SetDropdownColor();
    }

    void SetDropdownColor()
    {
        if (m_Outline == null)
        {
            m_Outline = transform.Find("Background").GetComponent<Image>();
        }

        if (m_Arrow == null)
        {
            m_Arrow = transform.Find("Background/Arrow").GetComponent<Image>();
        }

        m_Outline.CrossFadeColor(m_AccentColor, 0f, true, true);

        m_Arrow.CrossFadeColor(new Color(1f, 1f, 1f, 0), 0f, true, true);
    }
}
