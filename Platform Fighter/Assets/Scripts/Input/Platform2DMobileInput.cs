using UnityEngine;
using System.Collections;

public class Platform2DMobileInput : MonoBehaviour
{
    public MovementAxis movement;
    public JumpButton jumpbutton;
    //public UseButton UseButton;
    public PlatformerCharacter2D m_Character;

    // Use this for initialization
    void Start ()
    {
        movement = FindObjectOfType<MovementAxis>();
        jumpbutton = FindObjectOfType<JumpButton>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = movement.Movement;
        bool jump = jumpbutton.JumpNow;
        m_Character.Move(h, jump);
        if (jump == true)
            jumpbutton.JumpNow = false;
    }
}
