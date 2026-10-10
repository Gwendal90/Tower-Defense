using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioListener audioListener;

    public override void OnNetworkSpawn()
    {
        bool isLocalPlayer = IsOwner;

        playerCamera.gameObject.SetActive(isLocalPlayer);
        audioListener.enabled = isLocalPlayer;
    }
}