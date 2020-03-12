using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderGenerator : MonoBehaviour
{
    [SerializeField]
    private OvrAvatar m_Avatar;

    [Header("Fallback Materials")]
    [SerializeField]
    private Material m_HandMaterial;
    [SerializeField]
    private Material m_ControllerMaterial;
    [SerializeField]
    private Material m_BodyMaterial;
    [SerializeField]
    private Material m_BaseMaterial;

    private OvrAvatarHand[] m_Hands;
    private OvrAvatarTouchController[] m_Controllers;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Avatar != null)
        {
            StartCoroutine(GenerateHandColliders());
            StartCoroutine(GenerateControllerColliders());
            StartCoroutine(SwapAvatarMaterials());
        }
        else
        {
            Debug.LogError("Avatar not set");
        }
    }

    private IEnumerator SwapAvatarMaterials()
    {
        Debug.Log("Swapping Avatar Materials");
        if (m_Avatar.EnableHands)
        {
            Debug.Log("Hands Enabled");
            yield return new WaitUntil(() => m_Avatar.GetComponentsInChildren<OvrAvatarHand>(true).Length > 0);
            if (m_HandMaterial != null)
            {
                Debug.Log("Swapping Hand Materials");
                foreach (OvrAvatarHand hand in m_Avatar.GetComponentsInChildren<OvrAvatarHand>(true))
                {
                    yield return new WaitUntil(() => hand.GetComponentInChildren<SkinnedMeshRenderer>(true) != null);
                    hand.GetComponentInChildren<SkinnedMeshRenderer>(true).material = m_HandMaterial;
                }
            }
        }

        if (m_Avatar.EnableBody)
        {
            Debug.Log("Body Enabled");
            yield return new WaitUntil(() => m_Avatar.GetComponentsInChildren<OvrAvatarBody>(true).Length > 0);

            if (m_BodyMaterial != null)
            {
                Debug.Log("Swapping Body Materials");
                yield return new WaitUntil(() => m_Avatar.GetComponentInChildren<OvrAvatarBody>(true).GetComponentInChildren<SkinnedMeshRenderer>(true) != null);
                m_Avatar.GetComponentInChildren<OvrAvatarBody>(true).GetComponentInChildren<SkinnedMeshRenderer>(true).material = m_BodyMaterial;
            }
        }

        yield return new WaitUntil(() => m_Avatar.GetComponentsInChildren<OvrAvatarTouchController>(true).Length > 0);

        if (m_ControllerMaterial != null)
        {
            Debug.Log("Swapping Controller Materials");
            foreach(OvrAvatarTouchController controller in m_Avatar.GetComponentsInChildren<OvrAvatarTouchController>(true))
            {
                yield return new WaitUntil(() => controller.GetComponentsInChildren<SkinnedMeshRenderer>(true).Length > 0);
                foreach(SkinnedMeshRenderer skinnedMeshRenderer in controller.GetComponentsInChildren<SkinnedMeshRenderer>(true))
                {
                    skinnedMeshRenderer.material = m_ControllerMaterial;
                }

                controller.GetComponentsInChildren<SkinnedMeshRenderer>(true)[1].material = m_HandMaterial;
            }
        }

        if (m_Avatar.EnableBase)
        {
            Debug.Log("Base Enabled");
            yield return new WaitUntil(() => m_Avatar.GetComponentsInChildren<OvrAvatarBase>(true).Length > 0);

            if (m_BaseMaterial != null)
            {
                yield return new WaitUntil(() => m_Avatar.GetComponentInChildren<OvrAvatarBase>(true).GetComponentInChildren<SkinnedMeshRenderer>(true) != null);
                Debug.Log("Swapping Base Material");
                m_Avatar.GetComponentInChildren<OvrAvatarBase>().GetComponentInChildren<SkinnedMeshRenderer>().material = m_BaseMaterial;
            }
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
