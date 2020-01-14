using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class WatchMenuItem : MonoBehaviour
{
    [SerializeField]
    private Material m_TransparentMat;

    [SerializeField]
    private UnityEvent OnClick = new UnityEvent();

    private Dictionary<string, Material> m_Materials;

    private bool m_Selected;

    public delegate void ItemSelected(WatchMenuItem item);
    public event ItemSelected OnItemSelected;

    private void Start()
    {
        m_Materials = new Dictionary<string, Material>();

        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            m_Materials.Add(renderer.name, renderer.material);
        }
    }

    public void Select()
    {
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = m_Materials[renderer.name];
        }
    }

    public void Deselect()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = m_TransparentMat;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(VRTK_DeviceFinder.HeadsetTransform(), Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("hands:b_l_index_ignore") || other.name.Contains("hands:b_r_index_ignore"))
        {
            OnClick.Invoke();
            OnItemSelected?.Invoke(this);
        }
    }
}
