using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGoZoneScript : MonoBehaviour {

    private GameObject playerRef;

	// Use this for initialization
	void Start ()
    {
        playerRef = GameObject.Find("Ebisumaru");
    }
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shuriken")
        {
            playerRef.GetComponent<ShurikenThrowControl>().canThrow = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerRef.GetComponent<ShurikenThrowControl>().canThrow = true;
    }
}
