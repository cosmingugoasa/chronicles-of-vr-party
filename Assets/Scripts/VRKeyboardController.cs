using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class VRKeyboardController : MonoBehaviour
{

    TMP_InputField inputField;

    void Start()
    {
        inputField= GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => PresentKeyboard());
    }

    void PresentKeyboard() {
        NonNativeKeyboard.Instance.PresentKeyboard();
        NonNativeKeyboard.Instance.InputField = inputField;
    }

}
