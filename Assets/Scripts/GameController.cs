//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public DifficultGenerator difficultGenerator;

    public EnergyBall energyBallOrgn;

    public HealthBall healthBallOrgn;

    public Text gameOverTxt;
    public Button restartBtn;
    public bool IsGameOver { get; private set; }

    public Transform LastPlatform { get; private set; }

    private void Awake()
    {
        energyBallOrgn.gameObject.SetActive(false);
        healthBallOrgn.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void StartGame()
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

    public void OnPlatfromInit(Transform platform)
    {
        LastPlatform = platform;
    }

    public void OnPlatformSpawn(Transform platform)
    {
        LastPlatform = platform;

        RemoveAllChildren(platform);

        difficultGenerator.OnPlatformSpawn(platform);

        GenerateNewBall(platform, GetRandomOrignBall());

    }

    void RemoveAllChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; ++i)
        {
            var child = parent.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public Transform GenerateNewBall(Transform orgnBall)
    {
        return GenerateNewBall(LastPlatform, orgnBall);
    }

    public Transform GenerateNewBall(Transform parent, Transform orgnBall)
    {
        var newBall= Instantiate(orgnBall, parent);
        newBall.gameObject.SetActive(true);
        newBall.transform.localPosition =
            new Vector3(
                Random.Range(-3, +3), //x
                Random.Range(+1, +2), //y
                Random.Range(-3, +3)  //z
                );
        return newBall.transform;
    }

    Transform GetRandomOrignBall()
    {
        if(Random.Range(0,3)==0)
        {
            return healthBallOrgn.transform;
        }
        return energyBallOrgn.transform;
    }
}
