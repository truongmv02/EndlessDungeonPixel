using Cinemachine;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        cinemachineVirtualCamera.Follow = GameManager.Instance.Player.transform;
    }
}