using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListViewItem : Button
{
    private ListView listView;
    protected override void Awake()
    {
        base.Awake();
        if (Application.isPlaying)
        {
            listView = GetComponentInParent<ListView>();
            listView.AddItem(this);
            onClick.AddListener(SelectItem);
        }
    }

    public void SelectItem()
    {
        listView.SelectItem(this);
    }
    public virtual void Show() { }
    public virtual void Hide() { }


}
