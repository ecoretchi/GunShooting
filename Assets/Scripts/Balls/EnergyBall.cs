using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class EnergyBall : MonoBehaviour, IBall
{
    public void OnPlayerCollision(PlayerController player)
    {
        player.IncreaseEnergy();
    }
}
