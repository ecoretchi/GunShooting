using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public int maxLife = 3;
    public int maxHelthPoints = 5;
    public int maxEnergyPoints = 5;

    public Text lifeFild;
    public Text energyFild;
    public Slider helthSlider;

    int currentLife;
    int currentEnergy;

    float jumpPower = 3;

    Rigidbody rigitBody;

    Vector3 orignPos;

    public int CurrentHelth => (int)helthSlider.value;

    public void Init()
    {
        SetLife(maxLife);
        SetEnergy(maxEnergyPoints);
        Spawn();
    }

    void Awake()
    {
        helthSlider.maxValue = maxHelthPoints;
        rigitBody = GetComponent<Rigidbody>();
        orignPos = transform.position;
    }

    void SetLife(int newLife)
    {
        currentLife = newLife;
        lifeFild.text = string.Format("{0}", currentLife);
    }

    public void IncreaseEnergy(int count = 1)
    {
        SetEnergy(currentEnergy + count);
    }

    public void IncreaseHelth(int count = 1)
    {
        SetHelth(CurrentHelth + count);
    }
    
    void SetEnergy(int newEnergy)
    {
        if (newEnergy > maxEnergyPoints || newEnergy < 0 )
            return;
        currentEnergy = newEnergy;
        energyFild.text = string.Format("{0}", currentEnergy);
    }

    void SetHelth(int newHelth)
    {
        if (newHelth > maxHelthPoints || newHelth < 0)
            return;
        helthSlider.value = newHelth;
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

        var eneryBall = other.GetComponent<EnergyBall>();
        if (eneryBall)
        {
            eneryBall.OnPlayerCollision(this);
            
            Destroy(other.gameObject);
            return;
        }

        if (other.GetComponent<BulletController>())
        {
            var v = helthSlider.value;
            if (v > 0)
                helthSlider.value = v - 1;
            else
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
