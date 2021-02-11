﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public int maxLife = 3;
    public int maxHelthPoints = 5;

    public Text lifeFild;
    public Slider helthSlider;
    int currentLife;
    float jumpPower = 3;

    Rigidbody rigitBody;

    Vector3 orignPos;

    // Start is called before the first frame update
    void Start()
    {
        helthSlider.maxValue = maxHelthPoints;
        SetLife(maxLife);
        rigitBody = GetComponent<Rigidbody>();

        orignPos = transform.position;
    }

    void SetLife(int newLife)
    {
        currentLife = newLife;
        lifeFild.text = string.Format("{0}", currentLife);
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
            {
                rigitBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
        if(rigitBody.velocity.y < 0 && transform.position.y < -1)
        {
            LooseLife();
            transform.position = orignPos + Vector3.up * 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var v = helthSlider.value;
        if (v > 0)
            helthSlider.value = v - 1;
        else
            LooseLife();
    }

    void LooseLife()
    {
        if (currentLife > 0)
        {
            SetLife(currentLife - 1);
            helthSlider.value = maxHelthPoints;
        }
        else
        {
            throw new System.Exception("Game Over");
        }
    }

}
