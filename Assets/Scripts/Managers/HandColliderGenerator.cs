using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderGenerator : MonoBehaviour
{
    [SerializeField]
    private OvrAvatar m_Avatar;

    private OvrAvatarHand[] m_Hands;
    private OvrAvatarTouchController[] m_Controllers;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Avatar != null)
        {
            StartCoroutine(GenerateHandColliders());
            StartCoroutine(GenerateControllerColliders());
        }
        else
        {
            Debug.LogError("Avatar not set");
        }
    }

    private IEnumerator GenerateHandColliders()
    {
        Debug.Log("Generating Hand Colliders");

        yield return new WaitUntil(() => m_Avatar.GetComponentsInChildren<OvrAvatarHand>(true).Length > 0);

        m_Hands = m_Avatar.GetComponentsInChildren<OvrAvatarHand>(true);

        foreach (OvrAvatarHand hand in m_Hands)
        {
            GameObject fingerTip = hand.GetComponentInChildren<OvrAvatarSkinnedMeshPBSV2RenderComponent>(true).bones.FirstOrDefault(bone => bone.name.Contains("_index_ignore")).gameObject;
            fingerTip.AddComponent<SphereCollider>().radius = 0.005f;
        }
    }

    private IEnumerator GenerateControllerColliders()
    {
        Debug.Log("Generating Controller Colliders");

        yield return new WaitUntil(() => m_Avatar.GetComponentsInChildren<OvrAvatarTouchController>(true).Length > 0);

        m_Controllers = m_Avatar.GetComponentsInChildren<OvrAvatarTouchController>(true);

        foreach (OvrAvatarTouchController controller in m_Controllers)
        {
            //GameObject fingerTip = controller.GetComponentInChildren<OvrAvatarSkinnedMeshPBSV2RenderComponent>(true).bones.FirstOrDefault(bone => bone.name.Contains("_index_ignore")).gameObject;
            //fingerTip.AddComponent<SphereCollider>().radius = 0.5f;
        }
    }
}
