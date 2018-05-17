using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private HealthScript playerHealthScript;

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = true;
        playerHealthScript = GameObject.Find("Ebisumaru").GetComponent<HealthScript>();
    }

    public void DealDamage(int damage)
    {
        playerHealthScript.playerHealth = playerHealthScript.playerHealth - damage;

        playerHealthScript.CheckHealth();
    }
	

}
