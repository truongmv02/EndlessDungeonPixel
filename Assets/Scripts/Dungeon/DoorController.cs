using MVT.Base.Dungeon;
using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool IsOpen { get; set; } = true;
    Animator animator;
    BoxCollider2D doorCollider;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        doorCollider = transform.Find("DoorCollider").GetComponentInChildren<BoxCollider2D>();
    }
    private void Start()
    {
        OpenDoor();
    }

    public void CloseDoor()
    {
        IsOpen = false;
        doorCollider.enabled = true;
        SetAnimation(IsOpen);
    }

    public void OpenDoor()
    {
        IsOpen = true;
        doorCollider.enabled = false;
        SetAnimation(IsOpen);
    }

    private void SetAnimation(bool value)
    {
        animator.SetBool("Open", value);

    }
}
