using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyVFXController : MonoBehaviour
{
    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(
                                    transform.localScale.x,
                                    (float)(characterController.height * 0.75) / 2,
                                    transform.localScale.z);

        transform.position = new Vector3(
                                    transform.position.x,
                                    transform.localScale.y,
                                    transform.position.z
                                    );
    }
}
