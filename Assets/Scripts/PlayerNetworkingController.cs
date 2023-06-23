using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetworkingController : NetworkBehaviour
{
    [SerializeField]
    List<Component> toDisableIfNotOwner;

    void Start()
    {
        if (!IsOwner) { 
            foreach(var obj in toDisableIfNotOwner)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
