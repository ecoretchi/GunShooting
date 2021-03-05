using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallGenerator : MonoBehaviour
{

    public Transform energyBallPrefab;

    private void Awake()
    {
        energyBallPrefab.gameObject.SetActive(false);
    }
    public void GenerateBall(Transform plain)
    {
        var newBall = Instantiate(energyBallPrefab, plain);
        newBall.localPosition = new Vector3(Random.Range(-4, 4), Random.Range(2, 6), Random.Range(-2, 2));
        newBall.gameObject.SetActive(true);
    }
}
