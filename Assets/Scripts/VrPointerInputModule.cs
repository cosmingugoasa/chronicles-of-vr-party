using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VrPointerInputModule : BaseInputModule
{
    public Camera pointerCamera;
    public EventSystem customEventSystem;
    public SteamVR_Input_Sources pointerInputSource;
    public SteamVR_Action_Boolean rightTriggerClick;

    public PointerEventData pointerData = null;

    private GameObject currentObject = null;

    protected override void Awake()
    {
        base.Awake();

        pointerData = new PointerEventData(customEventSystem);
    }

    public override void Process()
    {
        pointerData.Reset();
        pointerData.position = new Vector2(pointerCamera.pixelWidth / 2, pointerCamera.pixelHeight / 2);

        //camera raycasting from player eyes instead of the camera on player controller

        customEventSystem.RaycastAll(pointerData, m_RaycastResultCache);
        pointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        
        currentObject = pointerData.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(pointerData, currentObject);

        if (rightTriggerClick.GetStateDown(pointerInputSource)) {
            ProcessPress(pointerData);
        }

        if (rightTriggerClick.GetStateUp(pointerInputSource)) {
            ProcessRelease(pointerData);
        }
    }

    public PointerEventData GetData() {
        return pointerData;
    }

    private void ProcessPress(PointerEventData data) {
        Debug.Log("PRESS !");
    }

    private void ProcessRelease(PointerEventData data) {
        Debug.Log("RELEASE !");
    }
}
