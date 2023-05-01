using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UIManager : MonoBehaviour
{
    public Transform playersHead;
    public GameObject UIPointer;
    public GameObject UICanvas;
    public SteamVR_Action_Boolean xButtonInput;
    public Transform playerUIPosition;

    private void Update()
    {
        if (xButtonInput.stateUp) {
            UICanvas.SetActive(!UICanvas.activeSelf);
            UIPointer.SetActive(!UIPointer.activeSelf);
            if (UICanvas.activeSelf) {
                UICanvas.transform.position = playerUIPosition.position;
                UICanvas.transform.LookAt(playersHead.position);
            }
        }
    }

    public void testJoin() {
        Debug.Log("JOIN");
    }
    public void testPlus()
    {
        Debug.Log("Plus");
    }
    public void testMinus()
    {
        Debug.Log("Minus");
    }
    public void testCreate()
    {
        Debug.Log("CREATE");
    }
}
