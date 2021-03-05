using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnergyBallGenerator))]
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

    public Text gameOverTxt;
    public EnergyBallGenerator ballGenerator;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        ballGenerator = GetComponent<EnergyBallGenerator>();
    }
    private void Start()
    {
        OnGameOver(false);
    }

    public void OnGameOver(bool value)
    {
        gameOverTxt.gameObject.SetActive(value);
        IsGameOver = value;
        gameObject.SetActive(!value);
    }


    public void OnPlatformRespawn(Transform plain)
    {
        ballGenerator.GenerateBall(plain);
    }
}
