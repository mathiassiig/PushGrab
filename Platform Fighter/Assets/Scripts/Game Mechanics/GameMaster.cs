using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class GlobalDefinitions
{
    public const int RESPAWN_PRISTINE_DELAY = 3;
    public enum DMGTYPE
    {
        FIRE,
        PIERCING,
        BLUNT,
        EXPLOSION,
        SLASHING
    };

    public enum DIRECTION
    {
        UP,
        LEFT,
        RIGHT,
        DOWN,
        UNIFORM
    }

    public static DIRECTION OppositeDirection(DIRECTION dir)
    {
        switch(dir)
        {
            case DIRECTION.UP:
                return DIRECTION.DOWN;
            case DIRECTION.DOWN:
                return DIRECTION.UP;
            case DIRECTION.RIGHT:
                return DIRECTION.LEFT;
            case DIRECTION.LEFT:
                return DIRECTION.RIGHT;
        }
        return DIRECTION.UNIFORM;
    }

    public static DIRECTION GetDirection(Vector2 vector)
    {
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y)) //sides
        {
            if (vector.x > 0)
            {
                return DIRECTION.RIGHT;
            }
            else
            {
                return DIRECTION.LEFT;
            }
        }
        else
        {
            if(vector.y > 0)
            {
                return DIRECTION.UP;
            }
            else
            {
                return DIRECTION.DOWN;
            }
        }
    }

    #region Animation
    public const float ANIM_CHARACTER_BLINK_MIN_DELAY = 1f;
    public const float ANIM_CHARACTER_BLINK_MAX_DELAY = 4f;
    public const float VIS_CHARACTER_BLUNT_SEVERITY_LIMIT = 200f;
    public const int VIS_CHARACTER_BLOOD_MIN = 2;
    public const int VIS_CHARACTER_BLOOD_MAX = 10;
    public const int VIS_CHARACTER_GORE_MIN = 1;
    public const int VIS_CHARACTER_GORE_MAX = 5;
    #endregion

}

public class GameMaster : MonoBehaviour
{
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
    }

    public void PlayerDied()
    {
        ShowRestartScreen();
    }

    public void RabbitDied()
    {
        ShowRestartScreen();
    }

    public void ShowRestartScreen()
    {

    }

    public void FoxDied()
    {

    }

    public void LevelComplete()
    {

    }
}
