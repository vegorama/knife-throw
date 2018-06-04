using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CheckAttack : MonoBehaviour {

    private BiggBossAIInput biggAI;

	// Use this for initialization
	void Start ()
    {
        biggAI = GetComponentInParent<BiggBossAIInput>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("ATTACK CALLED");

        if (collider.gameObject.name == "Ebisumaru")
        {
            biggAI.SetAttack();
        }
    }
}
