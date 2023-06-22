using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController instance = null;
    public static UIController Instance
    {
        get { 
            if (instance is null)
                instance = new UIController();
            return instance;
        }
    }

    public TMP_Text joinCodeText;
    public List<TMP_Text> lobbyPlayersItems;

    public void SetJoinCodeTextValue(string joinCode) { 
        joinCodeText.text = joinCode;
    }

    public void AddJoinedPlayerNameInLobby(string name) {
        lobbyPlayersItems.Where(i => i.text != "Empty").First().text = name;
    }
}
