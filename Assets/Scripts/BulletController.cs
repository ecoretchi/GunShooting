using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public TankController tank;
    public float speed = 10;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(tank.player.transform);
        //transform.rotation = tank.transform.rotation;
        dir = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
}
