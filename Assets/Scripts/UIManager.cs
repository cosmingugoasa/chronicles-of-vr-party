using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;

public class UIManager : MonoBehaviour
{
    public Transform playersHead;
    public GameObject UIPointer;
    public GameObject UICanvas;
    public SteamVR_Action_Boolean xButtonInput;
    public Transform playerUIPosition;

    //ui elements
    public TextMeshProUGUI numberOfPlayers;

    private void Start()
    {
        numberOfPlayers.SetText(SceneSessionManager.currentPlayersInSession.ToString());
    }

    private void Update()
    {
        HandleMenuAndPointerToggle();
    }
    private void HandleMenuAndPointerToggle() {
        //enable or disable menu and pointer
        if (xButtonInput.stateUp)
        {
            UICanvas.SetActive(!UICanvas.activeSelf);
            UIPointer.SetActive(!UIPointer.activeSelf);
            if (UICanvas.activeSelf)
            {
                UICanvas.transform.position = playerUIPosition.position;
                UICanvas.transform.LookAt(playersHead.position);
            }
        }
    }

    public void IncrementPlayerCounter()
    {
        numberOfPlayers.SetText(SceneSessionManager.currentPlayersInSession.Value++.ToString());
    }
    public void ReducePlayerCounter()
    {
        numberOfPlayers.SetText(SceneSessionManager.currentPlayersInSession.Value--.ToString());
    }
    public void testJoin() {
        Debug.Log("JOIN");
    }
    
    public void testCreate()
    {
        Debug.Log("CREATE");
    }

}
