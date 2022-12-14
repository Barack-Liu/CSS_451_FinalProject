using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMenu : MonoBehaviour
{
    public static bool FightEnsued = false;

    public GameObject playerObject;
    public GameObject pauseMenuUI;
    public BattleScene battleSceneRef;

    public PlayerPosVector playerPosVector;
    public TMPro.TMP_Text battleStatus;

    void Start()
    {
        Debug.Assert(battleStatus != null);
    }

    void Update()
    {
        CheckPlayerPosVectorState();
        if (FightEnsued)
        {
            Pause();
        }
        else
        {
            Resume();
        } 
    }

    private void CheckPlayerPosVectorState()
    {
        if (playerPosVector.currentState == PlayerPosVector.MapStates.BATTLE)
        {
            FightEnsued = true;
        }
        else
        {
            FightEnsued = false;
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerObject.GetComponentInChildren<MovementControls>().toggle = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerObject.GetComponentInChildren<MovementControls>().toggle = false;
    }
}
