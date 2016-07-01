using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private AttackModule m_AttackModule;
    private bool m_Jump;
    public int PlayerNumber;
    private string PlayerString;

    private void Start()
    {
        PlayerString = "P" + PlayerNumber.ToString() + "_";
    }

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
        m_AttackModule = GetComponent<AttackModule>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown(PlayerString + "Jump");
        }
        if (Input.GetButtonDown(PlayerString + "Push"))
        {
            m_AttackModule.TryAttack();
        }
        if(Input.GetButtonDown(PlayerString + "Grab"))
        {
            m_AttackModule.TryGrab();
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        float h = Input.GetAxis(PlayerString + "Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);
        m_Jump = false;
    }
}
