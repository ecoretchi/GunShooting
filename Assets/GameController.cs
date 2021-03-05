//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static GameController _thisGameController = null;

    public static GameController Instance
    {
        get
        {
            if (_thisGameController == null)
                _thisGameController = GameObject.FindObjectOfType<GameController>();

            return _thisGameController;
        }
    }
    public PlayerController playerController;
    public EnergyBall energyBallOrgn;

    public Text gameOverTxt;
    public Button restartBtn;
    public bool IsGameOver { get; private set; }


    private void Start()
    {
        energyBallOrgn.gameObject.SetActive(false);
        StartGame();
    }

    public void StartGame()
    {
        playerController.Init();
        OnGameOver(false);
    }
    public void OnGameOver(bool value)
    {
        restartBtn.gameObject.SetActive(value);
        gameOverTxt.gameObject.SetActive(value);
        IsGameOver = value;
        gameObject.SetActive(!value);
    }

    public void OnPlatformSpawn(Transform platform)
    {
        var newBall = Instantiate(energyBallOrgn, platform);
        newBall.gameObject.SetActive(true);
        newBall.transform.localPosition = new Vector3(Random.Range(-3, +3), Random.Range(-1, +2), Random.Range(-3, +3) );
    }
}
