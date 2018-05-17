using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collided");

        if (collider.gameObject.name == "Ebisumaru")
        {
            gameManager.DealDamage(100);

            Debug.Log("collided");
        }
    }
}
