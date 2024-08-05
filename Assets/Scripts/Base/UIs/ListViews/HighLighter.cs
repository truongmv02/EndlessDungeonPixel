using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HighLighter : MonoBehaviour
{
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        Hide();
    }

    public void Show()
    {
        if (!image.enabled)
            image.enabled = true;
    }

    public void Hide()
    {
        image.enabled = false;
    }

}
