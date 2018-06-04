using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (Ebisumaru2D))]
    public class EbisumaruUserControl : MonoBehaviour
    {
        private Ebisumaru2D m_Character;       

        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<Ebisumaru2D>();            
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            
            bool crouch = Input.GetKey(KeyCode.LeftControl);

            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            bool aiming = (Input.GetMouseButton(1) || Input.GetMouseButton(0));

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, aiming);
            m_Jump = false;
        }
    }
}
