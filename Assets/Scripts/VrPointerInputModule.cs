using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VrPointerInputModule : BaseInputModule
{
    public Camera pointerCamera;
    public SteamVR_Input_Sources pointerInputSource;
    public SteamVR_Action_Boolean rightTriggerClick;

    public PointerEventData pointerData = null;

    private GameObject currentObject = null;

    protected override void Awake()
    {
        base.Awake();

        pointerData = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        pointerData.Reset();
        pointerData.position = new Vector2(pointerCamera.pixelWidth / 2, pointerCamera.pixelHeight / 2);

        //camera raycasting from player eyes instead of the camera on player controller

        eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
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
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, pointerData, ExecuteEvents.pointerDownHandler);

        if (newPointerPress == null) {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        data.pressPosition = pointerData.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData data) {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if (data.pointerPress == pointerUpHandler) {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
