using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(AudioSource))]
public class ObjectDescriptor : MonoBehaviour {


    [Header("Object Information")]
    [SerializeField]
    private string m_ObjectName;
    [SerializeField, TextArea(5,10)]
    private string m_ObjectDescription;

    [Header("Tooltip Options")]
    [SerializeField]
    private GameObject m_Tooltip;
    [SerializeField, Tooltip("Tooltips offset from the object")]
    private Vector3 m_TooltipOffset;
    [SerializeField, Tooltip("Size of the tooltip")]
    private Vector2 m_TooltipSize;
    [SerializeField]
    private int m_TooltipFontSize;

    private ObjectTooltip m_CurrentTooltip;

    protected void OnEnable()
    {
        if (GetComponent<VRTK_InteractableObject>() != null)
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += InteractableObjectUsed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUnused += InteractableObjectUnused;
        }
    }

    protected virtual void OnDisable()
    {
        if (GetComponent<VRTK_InteractableObject>() != null)
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectUsed -= InteractableObjectUsed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUnused -= InteractableObjectUnused;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (m_CurrentTooltip == null)
        {
            DisplayDescription();
        }
        else
        {
            HideDescription();
        }
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        HideDescription();
    }

    public void DisplayDescription()
    {
        if (m_Tooltip != null)
        {
            foreach(ObjectDescriptor obj in transform.parent.GetComponentsInChildren<ObjectDescriptor>())
            {
                obj.HideDescription();
            }

            m_CurrentTooltip = Instantiate(m_Tooltip, transform).GetComponent<ObjectTooltip>();

            m_CurrentTooltip.transform.localPosition = m_TooltipOffset;
            m_CurrentTooltip.containerSize = m_TooltipSize;
            m_CurrentTooltip.fontSize = m_TooltipFontSize;
            m_CurrentTooltip.alwaysFaceHeadset = true;
            m_CurrentTooltip.drawLineFrom = m_CurrentTooltip.transform;
            m_CurrentTooltip.drawLineTo = transform;

            m_CurrentTooltip.UpdateText(m_ObjectName + '\n' + m_ObjectDescription);

            if (GetComponent<AudioSource>().clip != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            Debug.LogError("The Tooltip has not been set");
        }
    }

    public void HideDescription()
    {
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Stop();

        if (m_CurrentTooltip != null)
            Destroy(m_CurrentTooltip.gameObject);
    }

    public void PlayAudio()
    {
        if (GetComponent<AudioSource>().clip != null)
            GetComponent<AudioSource>().Play();   
    }
}
