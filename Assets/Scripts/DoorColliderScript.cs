using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColliderScript : MonoBehaviour {

	
    private void OnCollisionEnter2D (Collision2D collision)
    {
        SendMessageUpwards("OnDoorCollision", collision);
    }
}
