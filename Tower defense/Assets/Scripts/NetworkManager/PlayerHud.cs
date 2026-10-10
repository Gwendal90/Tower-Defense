using TMPro;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine;

public class PlayerHud : NetworkBehaviour
{
    private NetworkVariable<FixedString128Bytes> playersName = new NetworkVariable<FixedString128Bytes>();

    private bool overlaySet = false;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playersName.Value = $"Player {OwnerClientId}";
        }
    }

    public void SetOverlay()
    {
        var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        localPlayerOverlay.text = playersName.Value.ToString();
    }

    private void Update()
    {
        if (!overlaySet && !playersName.Value.IsEmpty)
        {
            SetOverlay();
            overlaySet = true;
        }
    }
}