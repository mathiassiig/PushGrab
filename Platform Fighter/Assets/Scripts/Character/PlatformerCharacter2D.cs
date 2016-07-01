using System;
using System.Collections;
using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField]
    private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField]
    private float m_AirControlSpeedFactor;
    [SerializeField]
    private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField]
    private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;

    public LayerTouching m_GroundCheck;    // A position marking where to check if the player is grounded.
    public LayerTouching m_WallCheckLeft;
    public LayerTouching m_WallCheckRight;
    const float k_WalledRadius = .2f;
    public bool m_WalledLeft;
    public bool m_WalledRight;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.

    public CharacterHealth m_Health;
    public float m_TotalMass = 0f;
    public bool Grabbing = false;

    private void Awake()
    {
        // Setting up references.
        //m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Health = GetComponent<CharacterHealth>();
        m_TotalMass += m_Rigidbody2D.mass;
    }


    private void FixedUpdate()
    {
        CheckIfGrounded();
        CheckIfWalled();
        //m_Anim.SetBool("Ground", m_Grounded);
        // m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    private void CheckIfGrounded()
    {
        m_Grounded = false;
        m_Grounded = m_GroundCheck.Touching;
    }

    private void CheckIfWalled()
    {
        m_WalledLeft = m_WallCheckLeft.Touching;
        m_WalledRight = m_WallCheckRight.Touching;
    }


    public void Move(float move, bool jump)
    {
        if (!m_Health.m_Stunned)
        {
            if (m_Grounded)
            {
                //m_Anim.SetFloat("Speed", Mathf.Abs(move));
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            }
            if (!Grabbing)
            {
                if (move > 0 && !m_FacingRight)
                    Flip();
                else if (move < 0 && m_FacingRight)
                    Flip();
            }
            int invertedWallJump = 1;
            if (!m_FacingRight)
                invertedWallJump = -1;

            if (m_Grounded && jump /*&& m_Anim.GetBool("Ground")*/)
            {
                m_Grounded = false;
                //m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * m_TotalMass));
            }
            else if (m_WalledLeft && jump)
            {
                m_Rigidbody2D.velocity = new Vector2(0, 0);
                m_WalledLeft = false;
                m_Rigidbody2D.AddForce(new Vector2(m_JumpForce * invertedWallJump * m_TotalMass, m_JumpForce * m_TotalMass * 0.66f));
            }
            else if (m_WalledRight && jump)
            {
                m_Rigidbody2D.velocity = new Vector2(0, 0);
                m_WalledRight = false;
                m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce * invertedWallJump * m_TotalMass, m_JumpForce * m_TotalMass * 0.66f));
            }
            if (m_AirControl && !m_Grounded && !m_WalledLeft && !m_WalledRight)
            {
                if ((m_Rigidbody2D.velocity.x < m_MaxSpeed && move > 0) || (m_Rigidbody2D.velocity.x > -m_MaxSpeed && move < 0))
                    m_Rigidbody2D.AddForce(new Vector2(5*move * m_MaxSpeed * m_Rigidbody2D.mass, 0));
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
