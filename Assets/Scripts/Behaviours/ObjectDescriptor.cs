using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

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

    private ObjectTooltip m_CurrentTooltip;
    private Vector3 m_TooltipLineEnd;

    [SerializeField]
    private AudioSource m_AudioSource;

    protected void OnEnable()
    {
        if (GetComponent<VRTK_InteractableObject>() != null)
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += InteractableObjectUsed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUnused += InteractableObjectUnused;
        }

        m_TooltipLineEnd = GetComponent<Renderer>().bounds.center;

        m_AudioSource = GetComponent<AudioSource>();
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
            m_CurrentTooltip.alwaysFaceHeadset = true;

            m_CurrentTooltip.UpdateText(m_ObjectName, m_ObjectDescription);

            m_CurrentTooltip.Button.onClick.AddListener(() => {
                if (m_AudioSource.isPlaying)
                {
                    m_AudioSource.Stop();
                    m_CurrentTooltip.Button.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
                } 
                else
                {
                    PlayAudio();
                    m_CurrentTooltip.Button.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
                }
            });
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
