using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum BallType
{
    Energy,
    Helth
}
public class EnergyBall : MonoBehaviour
{
    public BallType ballType;

    Renderer meshRenderer;
    Material energyBallMat;
    Material helthBallMat;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
        Assert.IsNotNull(meshRenderer);
        energyBallMat = Resources.Load<Material>("Materials/EnergyBall");
        helthBallMat = Resources.Load<Material>("Materials/HelthBall");
        Assert.IsNotNull(energyBallMat);
        Assert.IsNotNull(helthBallMat);
    }

    public void SetBallType(BallType newBallType)
    {
        ballType = newBallType;
    }

    private void Start()
    {
        switch (ballType)
        {
            case BallType.Energy:
                meshRenderer.material = energyBallMat;
                break;
            case BallType.Helth:
                meshRenderer.material = helthBallMat;
                break;
        }
    }

    public void OnPlayerCollision(PlayerController player)
    {
        switch (ballType)
        {
            case BallType.Energy:
                player.IncreaseEnergy();
                break;
            case BallType.Helth:
                player.IncreaseHelth();
                break;
        }
    }
}
