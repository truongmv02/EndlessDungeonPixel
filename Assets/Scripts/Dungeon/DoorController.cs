using MVT.Base.Dungeon;
using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool IsOpen { get; set; } = true;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        SetAnimation(IsOpen);

    }

    public void CloseDoor()
    {
        IsOpen = false;
        SetAnimation(IsOpen);
    }

    public void OpenDoor()
    {
        IsOpen = true;
        SetAnimation(IsOpen);
    }

    private void SetAnimation(bool value)
    {
        animator.SetBool("Open", value);

    }
}
