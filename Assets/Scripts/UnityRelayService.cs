using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class UnityRelayService : MonoBehaviour
{
    string joinCode;
    async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Player signed in with ID : {AuthenticationService.Instance.PlayerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        NetworkManager.Singleton.OnClientConnectedCallback += UnityRelay_OnClientConnectedCallback;
    }

    private void UnityRelay_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log($"{nameof(UnityRelay_OnClientConnectedCallback)} - Player connected : {obj}");
        LobbyManager.Instance.AddReadyPlayer();
    }

    public async void CreateRelay() {
        if (string.IsNullOrEmpty(UIController.Instance.playerName.text)) {
            return;
        }

        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(5);
            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            UIController.Instance.SetJoinCodeTextValue(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException ex) {
            Debug.Log($"Relay Error : {ex.Message}");
        }
    }

    public async void JoinRelay()
    {
        try {
            Debug.Log($"Joining Relay with code : {joinCode}");
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();
        }
        catch(RelayServiceException ex) {
            Debug.Log($"Relay Error : {ex.Message}");
        }
    }

}
