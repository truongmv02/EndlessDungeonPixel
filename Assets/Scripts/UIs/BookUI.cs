using System;
using System.Collections;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    Animator anim;
    bool open;
    public Action OnClosed;
    public Action OnOpened;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        open = true;
        anim.SetBool("Open", open);
    }

    public void Close()
    {
        open = false;
        anim.SetBool("Open", open);
    }

    private void HandleOpened()
    {
        if (OnOpened != null)
            OnOpened();
    }
    private void HandleClosed()
    {
        if (OnClosed != null)
            OnClosed();
    }
}
