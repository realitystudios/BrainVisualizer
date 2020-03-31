using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuPlacementManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_LeftWristPosition;
    [SerializeField]
    private Transform m_RightWristPosition;
    [SerializeField]
    private Transform m_MenuPosition;
    [SerializeField]
    private float m_MenuSize = 0.2f;

    [Space()]
    [Header("Prefabs")]
    [SerializeField]
    private GameObject m_WatchPrefab;
    [SerializeField]
    private GameObject m_MenuPrefab;

    private GameObject m_MenuObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetControllerType());
    }

    private IEnumerator GetControllerType()
    {
        yield return new WaitUntil(() => OVRPlugin.GetSystemHeadsetType() != OVRPlugin.SystemHeadset.None);
        yield return new WaitUntil(() => FindObjectsOfType<VRTK_Pointer>().Length > 0);
        VRTK.VRTK_Pointer[] pointers = FindObjectsOfType<VRTK.VRTK_Pointer>();

        switch (OVRPlugin.GetSystemHeadsetType())
        {
            case OVRPlugin.SystemHeadset.Oculus_Go:
                m_MenuObject = Instantiate(m_MenuPrefab, m_MenuPosition);
                m_MenuObject.transform.localScale.Set(m_MenuSize, m_MenuSize, m_MenuSize);
                foreach(var pointer in pointers)
                {
                    pointer.interactWithObjects = true;
                    pointer.grabToPointerTip = true;
                }
                break;
            case OVRPlugin.SystemHeadset.Rift_S:
            case OVRPlugin.SystemHeadset.Oculus_Quest:
                m_MenuObject = Instantiate(m_WatchPrefab, m_RightWristPosition);
                break;
            default:
                break;
        }
    }
}
