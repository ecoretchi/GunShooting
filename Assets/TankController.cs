using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public GameObject player;
    public BulletController bullet;

    public float rateOfFire = 1;
    public float rotSpeed = 5;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        bullet.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        var lookAtPlayerDir = player.transform.position - transform.position;
        var dorProd = Vector3.Dot(lookAtPlayerDir.normalized, transform.forward);

        if (timer > 1 / rateOfFire)
        {
            timer = 0;
            if(dorProd > 0.99)
                DoFire();
        }
        
        var lookAt = Quaternion.LookRotation(lookAtPlayerDir);

        transform.rotation = Quaternion.RotateTowards(transform.rotation ,  lookAt, Time.deltaTime * 10 * rotSpeed);// LookAt(player.transform);
    }

    void DoFire()
    {
        var newBullet = Instantiate(bullet);
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = bullet.transform.position;
        GameObject.Destroy(newBullet.gameObject, 2);
    }
}
