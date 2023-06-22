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
    async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Player signed in with ID : {AuthenticationService.Instance.PlayerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    async void CreateRelay() {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(5);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            UIController.Instance.SetJoinCodeTextValue(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException ex) {
            Debug.Log($"Relay Error : {ex.Message}");
        }
    }

    async void JoinRelay(string joinCode)
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
