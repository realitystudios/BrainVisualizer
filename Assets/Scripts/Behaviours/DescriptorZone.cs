using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptorZone : MonoBehaviour
{
    [SerializeField]
    private ObjectTooltip m_Tooltip;

    private void OnTriggerEnter(Collider other)
    {
        ObjectDescriptor descriptor = other.GetComponent<ObjectDescriptor>();
        if (descriptor != null)
        {
            if (m_Tooltip != null)
            {
                m_Tooltip.UpdateText(
                    descriptor.ObjectName, descriptor.ObjectDescription
                );

                m_Tooltip.Button.onClick.AddListener(() => {
                    if (descriptor.GetComponent<AudioSource>().isPlaying)
                    {
                        descriptor.StopAudio();
                    } else {
                        descriptor.PlayAudio(); 
                    }
                });
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectDescriptor descriptor = other.GetComponent<ObjectDescriptor>();
        if (descriptor != null)
        {
            if (m_Tooltip != null)
            {
                m_Tooltip.UpdateText("", "");
                m_Tooltip.Button.onClick.RemoveAllListeners();
            }
        }
    }
}
