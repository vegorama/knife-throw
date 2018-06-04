using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class BiggBossAIInput : MonoBehaviour
    {

        private GameObject EbisumaruRef;
        private Collider2D attackColl;

        public float moveRight;
        public bool attack;

        // Use this for initialization
        void Start()
        {
            EbisumaruRef = GameObject.Find("Ebisumaru");
            attackColl = GetComponentInChildren<BoxCollider2D>();
        }

        void CheckSide()
        {
            if (transform.position.x < EbisumaruRef.transform.position.x)
            {
                moveRight = 1;
                Debug.Log("Player is on target's right side");
            }
            else
            {
                moveRight = -1;
                Debug.Log("Player is on target's left side");
            }
        }

        //Set from child collider
        public void SetAttack()
        {
            //Set attack to true, Set false again by animator
            attack = true;
        }

        // Update is called once per frame
        void Update()
        {
            CheckSide();
        }
    }
}