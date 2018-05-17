using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    private ItemManager itemManager;

    private Collider2D doorCollider;

    public bool doorIsBlue;
    public bool doorIsGreen;
    public bool doorIsRed;

    // Use this for initialization
    void Start ()
    {
        itemManager = GameObject.Find("GameManager").GetComponent<ItemManager>();
        doorCollider = gameObject.GetComponent<Collider2D>();
    }

    public void OnDoorCollision(Collision2D collision)
    {
        if (collision.gameObject.name == "Ebisumaru")
        {
            if (itemManager.keyList["playerHasBlueKey"] && doorIsBlue)
            {
                Destroy(gameObject);
            }
            if (itemManager.keyList["playerHasRedKey"] && doorIsRed)
            {
                Destroy(gameObject);
            }
            if (itemManager.keyList["playerHasGreenKey"] && doorIsGreen)
            {
                Destroy(gameObject);
            }
        }
      
    }
}
