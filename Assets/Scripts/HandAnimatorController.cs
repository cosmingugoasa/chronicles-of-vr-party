using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    public InputActionProperty pinchAction;
    public InputActionProperty gripAction;

    public Animator animator;
    private void Update()
    {
        var pinchValue = pinchAction.action.ReadValue<float>();
        var gripValue = gripAction.action.ReadValue<float>();

        animator.SetFloat("Trigger", pinchValue);
        animator.SetFloat("Grip", gripValue);
    }
}
