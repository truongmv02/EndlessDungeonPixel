using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListView : MonoBehaviour
{
    List<ListViewItem> listViewItem = new List<ListViewItem>();
    int selectedIndex = -1;
    ListViewItem selectedItem;
    HighLighter highLighter;

    public event Action<ListViewItem, ListViewItem> OnChangeSelected;

    private void Awake()
    {
        highLighter = GetComponentInChildren<HighLighter>();
    }

    public void AddItem(ListViewItem item)
    {
        listViewItem.Add(item);
    }
    public void RemoveItem(ListViewItem item)
    {
        if (selectedItem == item)
        {
            selectedItem = null;
            selectedIndex = -1;
        }
        listViewItem.Remove(item);
        Destroy(item.gameObject);
    }
    public void RemoveAll()
    {
        for (int i = listViewItem.Count - 1; i >= 0; i--)
        {
            RemoveItem(listViewItem[i]);
        }
        selectedIndex = -1;
        selectedItem = null;
    }
    public void SelectItem(ListViewItem item)
    {
        if (item == selectedItem) return;
        OnChangeSelected?.Invoke(selectedItem, item);
        selectedItem = item;
        selectedIndex = listViewItem.IndexOf(item);
        if (highLighter != null)
        {
            highLighter.Show();
            highLighter.transform.SetParent(item.transform);
            highLighter.gameObject.SetActive(true);
            highLighter.transform.position = item.transform.position;
        }
    }
    public int GetIndex(ListViewItem item)
    {
        return listViewItem.IndexOf(item);
    }
    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
    public ListViewItem GetSelectedItem()
    {
        return selectedItem;
    }

}
