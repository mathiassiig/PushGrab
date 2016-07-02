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
    public int P1_Kills;
    public int P2_Kills;
    public int P3_Kills;
    public int P4_Kills;
    public List<PlatformerCharacter2D> players;

    public List<RespawnPoint> respawns;

    public int RespawnDelay = 3;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        foreach (var player in players)
        {
            DoRespawn(player);
        }
    }

    public void AddKill(int playerNumber)
    {
        switch(playerNumber)
        {
            case 1:
                P1_Kills++;
                break;
            case 2:
                P2_Kills++;
                break;
            case 3:
                P3_Kills++;
                break;
            case 4:
                P4_Kills++;
                break;
        }
    }

    public void PlayerDied(PlatformerCharacter2D player)
    {
        player.gameObject.SetActive(false);
        StartCoroutine(Respawn(player));
    }

    private IEnumerator Respawn(PlatformerCharacter2D player)
    {
        yield return new WaitForSeconds(RespawnDelay);
        DoRespawn(player);
    }

    private void DoRespawn(PlatformerCharacter2D player)
    {
        int rand = Random.Range(0, respawns.Count);
        RespawnPoint point = respawns[rand];
        if(point.Pristine)
        {
            point.Respawn(player);
        }
        else
        {
            //Find a point that is pristine
            //Must always have 4 points
            DoRespawn(player);
        }
    }
}
