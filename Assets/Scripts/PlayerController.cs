using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public GameObject followHead;
    public GameObject dicePrefab;
    public Transform playerObjectsSpawn;

    public SteamVR_Action_Vector2 leftTouchPadInput;
    public SteamVR_Action_Boolean yButtonInput;
    public float speed = 2;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (leftTouchPadInput.axis.magnitude > 0.1f)
        {
            Vector3 headsetDirection = Player.instance.hmdTransform.TransformDirection(new Vector3(leftTouchPadInput.axis.x, 0, leftTouchPadInput.axis.y));
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(headsetDirection, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }

        if (yButtonInput.stateUp) {
            SpawnObjectForPlayer(dicePrefab, playerObjectsSpawn);
        }
    }

    public void SpawnObjectForPlayer(GameObject prefab, Transform position) {
        Instantiate(prefab, position).transform.SetParent(null);
    }

    private void AdjustPlayerObjectsSpawnPoint() { 
        
    }

}
