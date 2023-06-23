using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public TMP_Text joinCodeText;
    public List<TMP_Text> lobbyPlayersItems;
    public TMP_InputField playerName;

    private void Awake()
    {
        Instance = this;
    }

    public void SetJoinCodeTextValue(string joinCode) { 
        joinCodeText.text = joinCode;
    }

    [ClientRpc]
    public void AddJoinedPlayerNameInLobbyClientRpc(ulong playerID, string name) {
        lobbyPlayersItems.ElementAt((int)playerID).text = name;
    }
}
