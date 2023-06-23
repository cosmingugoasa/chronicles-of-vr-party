using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager Instance { get; private set; }

    private Dictionary<ulong, (bool, string)> playersReadyInLobby;

    private void Awake()
    {
        Instance = this;
        playersReadyInLobby= new Dictionary<ulong, (bool, string)>();
    }

    public void AddReadyPlayer() {
        AddReadyPlayerServerRpc();
    }

    [ServerRpc]
    private void AddReadyPlayerServerRpc(ServerRpcParams rpcParams = default) {

        UIController.Instance.AddJoinedPlayerNameInLobbyClientRpc(
                rpcParams.Receive.SenderClientId, UIController.Instance.playerName.text);

        playersReadyInLobby[rpcParams.Receive.SenderClientId] = (true, UIController.Instance.playerName.text);

        bool allClientsReady = true;
        foreach (var playerID in NetworkManager.Singleton.ConnectedClientsIds) {
            if(!playersReadyInLobby.ContainsKey(playerID) || !playersReadyInLobby[playerID].Item1) {
                allClientsReady = false;
                break;
            }
        }

        if (allClientsReady) {
            NetworkManager.Singleton.SceneManager.LoadScene(
                Utils.Scenes.DemoMap.ToString(),
                UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
