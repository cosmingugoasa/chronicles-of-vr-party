using System.Collections;
using System.Collections.Generic;
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

    public void SetJoinCodeTextValue(string joinCode) { 
        joinCodeText.text = joinCode;
    }

}
