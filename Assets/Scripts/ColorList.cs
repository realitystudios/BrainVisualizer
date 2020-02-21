using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorList : MonoBehaviour
{
    [System.Serializable]
    private struct AccentColour
    {
        public string name;
        public Color colour;
    }

    [SerializeField]
    private AccentColour[] m_AccentColours;


    void Start()
    {
        List<string> colourNames = new List<string>();

        foreach (AccentColour accentColour in m_AccentColours)
        {
            colourNames.Add(accentColour.name);
        }

        GetComponent<VRUIDropdown>().AddOptions(colourNames);

        GetComponent<VRUIDropdown>().onValueChanged.AddListener((value) => {
            VRUIColorPalette.Instance.accentColor = m_AccentColours[value].colour;
            VRUIColorPalette.Instance.UpdateColors();
        });        
    }
}
