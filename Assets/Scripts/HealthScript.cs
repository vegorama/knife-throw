using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class HealthScript : MonoBehaviour {

    public int playerHealth = 100;
    private Animator m_Anim;

    public bool isDead = false;


    // Use this for initialization
    void Start ()
    {
        m_Anim = GetComponent<Animator>();
    }
	
    public void CheckHealth()
    {
        if (playerHealth <= 0)
        {
            isDead = true;

            GetComponent<ShurikenThrowControl>().enabled = false;
            GetComponent<Platformer2DUserControl>().enabled = false;
        }

        m_Anim.SetBool("IsDead", isDead);
    }
}
