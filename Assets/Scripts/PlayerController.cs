﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public int maxLife = 3;

    public int maxHelthPoints = 5;
    public int maxEnergyPoints = 5;
    public int initLife = 5;
    public int initEnergyPoints = 5;

    public ItemSNV lifeSNV;
    public ItemSNV energySNV;
    public Slider helthSlider;

    int currentLife;
    int currentEnergy;

    float jumpPower = 3;

    Rigidbody rigitBody;

    Vector3 orignPos;

    public int CurrentHelth => (int)helthSlider.value;

    public void Init()
    {

        SetLife(initLife);
        SetEnergy(initEnergyPoints);
        Spawn();
    }

    void Awake()
    {
        energySNV.SetMaxValue(maxEnergyPoints);
        lifeSNV.SetMaxValue(maxLife);
        helthSlider.maxValue = maxHelthPoints;
        rigitBody = GetComponent<Rigidbody>();
        orignPos = transform.position;
    }

    void SetLife(int newLife)
    {
        if (newLife > maxLife || newLife < 0)
            return;
        currentLife = newLife;
        lifeSNV.SetValue( currentLife);
    }

    public void IncreaseLife(int count = 1)
    {
        if (currentLife == maxLife)
        {
            if (CurrentHelth == maxHelthPoints)
                SetEnergy(maxEnergyPoints);
            else
                SetHelth(maxHelthPoints);
        }
        else
            SetLife(currentLife + count);
    }

    public void IncreaseEnergy(int count = 1)
    {
        if (currentEnergy == maxEnergyPoints)
            SetHelth(CurrentHelth + count);
        else
            SetEnergy(currentEnergy + count);
    }

    public void IncreaseHelth(int count = 1)
    {
        if (CurrentHelth == maxHelthPoints)
            SetEnergy(currentEnergy + count);
        else
            SetHelth(CurrentHelth + count);
    }
    
    void SetEnergy(int newEnergy)
    {
        if (newEnergy > maxEnergyPoints || newEnergy < 0 )
            return;
        currentEnergy = newEnergy;
        energySNV.SetValue(currentEnergy);
    }

    void SetHelth(int newHelth)
    {
        if (newHelth > maxHelthPoints || newHelth < 0)
            return;
        helthSlider.value = newHelth;
    }

    public void IncreaseMaxEnergy()
    {
        ++maxEnergyPoints;
        energySNV.SetMaxValue(maxEnergyPoints);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime * speed;
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * dt);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * dt);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * dt);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * dt);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Mathf.Abs(rigitBody.velocity.y) < float.Epsilon)
                AddJumpForce();
            else if(currentEnergy > 0)
            {
                SetEnergy(currentEnergy - 1);
                AddJumpForce();
            }
        }
        if(rigitBody.velocity.y < 0 && transform.position.y < -1)
        {
            LooseLife();
        }

        CheckIsPushedOut();
    }

    void CheckIsPushedOut()
    {
        if(transform.position.z < -6 || transform.position.z > 25)
        {
            LooseLife();
        }
    }

    void Spawn()
    {
        transform.position = orignPos + Vector3.up * 2;
    }

    void AddJumpForce()
    {
        rigitBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {

        var ball = other.GetComponent<MonoBehaviour>();
        if (ball is IBall)
        {
            (ball as IBall).OnPlayerCollision(this);   
            Destroy(other.gameObject);
            return;
        }
        var bullet = other.GetComponent<BulletController>();
        if (bullet)
        {
            bullet.OnPlayerCollision(this);
        }
    }

    public void LooseHelths(float count)
    {
        var currentHelth = helthSlider.value;
        if (currentHelth > count)
            helthSlider.value = Mathf.Clamp(currentHelth - count,  0, maxHelthPoints);
        else
        {
            LooseLife();
        }
    }

    void LooseLife()
    {
        if (currentLife > 0)
        {
            SetLife(currentLife - 1);
            helthSlider.value = maxHelthPoints;
            Spawn();
        }
        else
        {
            GameController.Instance.OnGameOver(true);
        }
    }

}
