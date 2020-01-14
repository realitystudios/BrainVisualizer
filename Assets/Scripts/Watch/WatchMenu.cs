using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchMenu : MonoBehaviour
{
    [SerializeField]
    private List<WatchMenuItem> m_MenuItems;

    private int m_CurrentItemIndex = 0;

    private void Start()
    {
        foreach(WatchMenuItem menuItem in m_MenuItems)
        {
            menuItem.OnItemSelected += UpdateMenu;
        }
    }

    private void UpdateMenu(WatchMenuItem menuItem)
    {
        m_CurrentItemIndex = m_MenuItems.IndexOf(menuItem);

        foreach(WatchMenuItem item in m_MenuItems)
        {
            if (item.Equals(menuItem))
            {
                menuItem.Select();
            } else
            {
                menuItem.Deselect();
            }
        }
    }

    private void Update()
    {

    }
}
