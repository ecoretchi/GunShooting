using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBall : MonoBehaviour, IBall
{
    public void OnPlayerCollision(PlayerController player)
    {
        player.IncreaseLife();
    }
}
