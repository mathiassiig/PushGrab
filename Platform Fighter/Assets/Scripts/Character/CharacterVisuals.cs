using UnityEngine;
using System.Collections;

public class CharacterVisuals : MonoBehaviour
{
    float BlinkDelay;
    public Animator animator;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBlink();
    }

    void UpdateBlink()
    {
        BlinkDelay -= Time.deltaTime;
        if (BlinkDelay <= 0)
        {
            animator.SetTrigger("BlinkNow");
            BlinkDelay = Random.Range(GlobalDefinitions.ANIM_CHARACTER_BLINK_MIN_DELAY, GlobalDefinitions.ANIM_CHARACTER_BLINK_MAX_DELAY);
        }
    }

    void VisualDeath(GlobalDefinitions.DMGTYPE type, Vector2 direction)
    {
        switch (type)
        {
            case GlobalDefinitions.DMGTYPE.BLUNT:
                break;
            case GlobalDefinitions.DMGTYPE.EXPLOSION:
                break;
            case GlobalDefinitions.DMGTYPE.FIRE:
                break;
            case GlobalDefinitions.DMGTYPE.PIERCING:
                break;
        }
    }
}
