using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelController))]
public class DifficultGenerator : MonoBehaviour
{
    public TankController tankOrgn;
    public PlayerController playerController;
    public LifeBall lifeBallOrgn;

    public Transform cubeOrgn;
    LevelController levelController;

    int hardLevel = 0;
    float zOffset = 0;

    void Awake()
    {
        levelController = GetComponent<LevelController>();
        cubeOrgn.gameObject.SetActive(false);

    }

    public void OnPlatformSpawn(Transform platform)
    {

        float zOffset = Random.Range(-1.5f, 1.5f);
        this.zOffset += zOffset;
        this.zOffset = Mathf.Clamp(this.zOffset, 0, 5 + hardLevel * 10);
        platform.position = new Vector3(Random.Range(-5f, 5f), 0, 36 + this.zOffset);

            
        int level = levelController.CurrentLevel;



        if (level < 10)
        {

            for (int i = 0; i < level; ++i)
                GenerateObject(cubeOrgn);
            
            OnHardLevelA();
        }
        else if (level < 20)
        {
            for (int i = 0; i < level - 5; ++i)
                GenerateObject(cubeOrgn);

            if (level % 2 == 0)
                OnHardLevelB();
        }
        else if (level < 30)
        {
            for (int i = 0; i < level - 15; ++i)
                GenerateObject(cubeOrgn);
            if (level % 3 == 0)
                OnHardLevelC();
        }
        else if (level < 40)
        {
            for (int i = 0; i < level / 2; ++i)
                GenerateObject(cubeOrgn);
            if (level % 3 == 0)
                OnHardLevelD();
        }
        else if (level < 50)
        {
            for (int i = 0; i < level / 3; ++i)
                GenerateObject(cubeOrgn);
            if (level % 3 == 0)
                OnHardLevelE();
        }
        else
        {
            for (int i = 0; i < level / 10; ++i)
                GenerateObject(cubeOrgn);
            SetHardLevel(4 + level/50);
            if (level % 2 == 0)
                GenerateTank();
        }
    }

    void OnHardLevelA()
    {    
        SetHardLevel(1);
        GenerateTank();
    }

    void OnHardLevelB()
    {
        SetHardLevel(2);
        GenerateTank();
    }

    void OnHardLevelC()
    {
        SetHardLevel(3);
        GenerateTank();
    }

    void OnHardLevelD()
    {
        SetHardLevel(4);
        int level = levelController.CurrentLevel;
        for (int i = 0; i < (level - 30) / 4; ++i)
            GenerateTank();
    }
    void GenerateTank()
    {
        var tank = GenerateObject(tankOrgn.transform, 4).GetComponent<TankController>();
        tank.bullet.powerOfFire = hardLevel + 1;
        tank.bullet.speed = 50 + hardLevel * 2;
    }
    void OnHardLevelE()
    {
        SetHardLevel(5);
        int level = levelController.CurrentLevel;
        for (int i = 0; i < (level - 40) / 5; ++i)
            GenerateObject(tankOrgn.transform, 4);
    }
    void OnHardLevelChanged()
    {
        GameController.Instance.GenerateNewBall(lifeBallOrgn.transform);
        playerController.IncreaseMaxEnergy();
    }
    void SetHardLevel(int val)
    {
        if(hardLevel != val)
        {
            hardLevel = val;
            OnHardLevelChanged();
        }
    }
    GameObject GenerateObject(Transform obj, int y = 1)
    {
        var newObj = Instantiate(obj, GameController.Instance.LastPlatform);
        newObj.localPosition = GetRandomPos(y);
        newObj.gameObject.SetActive(true);
        return newObj.gameObject;
    }

    Vector3 GetRandomPos(int y)
    {
       return new Vector3(

             Random.Range(-4, 5),
             y,
             Random.Range(-4, 5)
             );
    }
}
