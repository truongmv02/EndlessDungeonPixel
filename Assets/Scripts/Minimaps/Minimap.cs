
using Cinemachine;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    CharacterController player;
    public CinemachineVirtualCamera CinemachineVirtualCamera { set; get; }
    public CinemachineConfiner CinemachineConfiner { set; get; }

    private void Awake()
    {
        CinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        CinemachineConfiner = GetComponentInChildren<CinemachineConfiner>();
        GameManager.Instance.MiniMap = this;
    }
    private void Start()
    {

        player = GameManager.Instance.Player;
        CinemachineVirtualCamera.Follow = player.transform;
        CinemachineConfiner.InvalidatePathCache();
    }
}
