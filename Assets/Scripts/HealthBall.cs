using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBall : MonoBehaviour, IBall
{
    public void OnPlayerCollision(PlayerController player)
    {
        player.IncreaseHelth();
    }
}
