using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class SceneSessionManager : NetworkBehaviour
{
    public static NetworkVariable<int> maxPlayersInSession = new NetworkVariable<int>(6);
    public static NetworkVariable<int> currentPlayersInSession = new NetworkVariable<int>(1);
    
    public static Dictionary<ulong, int> spawnLocations = new Dictionary<ulong, int>();

    /// <summary>
    /// Return the index of the added item in the spawn locations arrays
    /// </summary>
    /// <param name="clientID"></param>
    /// <returns></returns>
    public static int AddClient(ulong clientID) { 
        spawnLocations.Add(clientID, spawnLocations.Count);
        return spawnLocations.Count;
    }
}