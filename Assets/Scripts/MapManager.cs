using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MapManager : NetworkBehaviour
{
    public GameObject playerPrefab;

    public Transform[] platforms;
    public Transform[] spawnPoints;

    public void Awake()
    {
        
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
    }

    private void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log($"All client loaded the map scene");
        foreach (var playerID in NetworkManager.Singleton.ConnectedClientsIds) {
            var player = Instantiate(playerPrefab, 
                                    spawnPoints[(int)playerID].position,
                                    spawnPoints[(int)playerID].rotation);

            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerID, true);
        }
    }
}
