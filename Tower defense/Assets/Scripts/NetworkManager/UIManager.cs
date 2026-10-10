using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button StartServerButton;

    [SerializeField]
    private Button StartHostButton;

    [SerializeField]
    private Button StartClientButton;

    [SerializeField]
    private TextMeshProUGUI playersInGameText;
    [SerializeField]
    private TextMeshProUGUI joinCode;
    

    [SerializeField]
    private TMP_InputField playerCode;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        playersInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInGame}";
    }

    private void Start()
    {
        StartHostButton.onClick.AddListener(async () =>
        {
            if(RelayManager.Instance.IsRelayEnabled)
            {
                RelayHostData relayHostData = await RelayManager.Instance.SetupRelay();
                joinCode.text = relayHostData.JoinCode;
            }
            if(NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host started...");
            }
            else
            {
                Debug.Log("Host could not be started...");
            }
        });
        StartServerButton.onClick.AddListener(() =>
        {
            if(NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server started...");
            }
            else
            {
                Debug.Log("Server could not be started...");
            }
        });
        StartClientButton.onClick.AddListener(async() =>
        {
            if(RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(playerCode.text))
                await RelayManager.Instance.JoinRelay(playerCode.text);

            if(NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client started...");
            }
            else
            {
                Debug.Log("Client could not be started...");
            }
        });
    }
}
