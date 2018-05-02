using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuy : MonoBehaviour {

    public int healthPoints = 100;

	// Use this for initialization
	void Start () {
		
	}
	

	public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        Debug.Log("HP:" + healthPoints);

        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
