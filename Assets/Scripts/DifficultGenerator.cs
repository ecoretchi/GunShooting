using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelController))]
public class DifficultGenerator : MonoBehaviour
{
    public Transform cubeOrgn;
    LevelController levelController;

    void Awake()
    {
        levelController = GetComponent<LevelController>();
        cubeOrgn.gameObject.SetActive(false);

    }
    public void OnPlatformSpawn(Transform platform)
    {
        int level = levelController.CurrentLevel;
    }

    void GenerateCube()
    {
        var newCube = Instantiate(cubeOrgn, GameController.Instance.LastPlatform);
        newCube.localPosition = new Vector3(

             Random.Range(-4, 5),
             10,
             Random.Range(-4, 5)
             );
        newCube.gameObject.SetActive(true);
    }
}
