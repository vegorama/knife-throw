using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision 2D Detected. Magnitude: " + col.relativeVelocity.y);

        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(rb.velocity.x, (col.relativeVelocity.y * -0.8f));
    }
}
