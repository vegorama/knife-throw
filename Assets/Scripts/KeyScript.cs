using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    private ItemManager itemManager;


    public bool isBlueKey;
    public bool isRedKey;
    public bool isGreenKey;

    // Use this for initialization
    void Start ()
    {
        itemManager = GameObject.Find("GameManager").GetComponent<ItemManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ebisumaru")
        {
            if (isBlueKey)
            {
                itemManager.keyList["playerHasBlueKey"] = true;
                Destroy(gameObject);
            }
            if (isRedKey)
            {
                itemManager.keyList["playerHasRedKey"] = true;
                Destroy(gameObject);
            }
            if (isGreenKey)
            {
                itemManager.keyList["playerHasGreenKey"] = true;
                Destroy(gameObject);
            }
        }
    }
}
