using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlacementManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_LeftWristPosition;
    [SerializeField]
    private Transform m_RightWristPosition;
    [SerializeField]
    private Transform m_MenuPosition;

    [SerializeField]
    private GameObject m_WatchPrefab;
    [SerializeField]
    private GameObject m_MenuPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject watch = Instantiate(m_WatchPrefab, m_RightWristPosition);

        StartCoroutine(GetControllerType());
    }

    private IEnumerator GetControllerType()
    {
        yield return new WaitUntil(() => OVRInput.GetConnectedControllers() != OVRInput.Controller.None);
        Debug.Log(OVRInput.GetConnectedControllers());
    }
}
