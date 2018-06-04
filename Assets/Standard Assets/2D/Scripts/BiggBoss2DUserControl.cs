using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class BiggBoss2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private BiggBossAIInput biggAI;
        
        private bool m_Jump;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            biggAI = GetComponent<BiggBossAIInput>();
        }


        //BiggBoss don't jump!
        /*
        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }
        */

        private void FixedUpdate()
        {
            // Read the inputs.

            //Biggboss dont need crouch
            bool crouch = false;

            //This needs logic
            float h = biggAI.moveRight;

            //BiggBoss Don't aim
            bool aiming = biggAI.attack;

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, aiming);
            m_Jump = false;
        }
    }
}
