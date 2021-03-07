using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    public GameObject player;
    public BulletController bullet;

    public int ammoCountMax;
    public int reloadTimeSec;

    public float rateOfFire = 1;
    public float rotSpeed = 5;


    public Transform reloadUI;

    float timer;
    float timerReload;

    int ammoCount;

    //Slider reloadSlider;

    // Start is called before the first frame update
    void Start()
    {
        bullet.gameObject.SetActive(false);
        //reloadSloder = reloadUI.GetComponentInChildren<Slider>();
        //reloadSlider.maxValue = reloadTimeSec;
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
        
        if (ammoCount == 0)
            DoReload();

        var lookAt = Quaternion.LookRotation(lookAtPlayerDir);

        transform.rotation = Quaternion.RotateTowards(transform.rotation ,  lookAt, Time.deltaTime * 10 * rotSpeed);// LookAt(player.transform);
    }

    void DoFire()
    {
        if (ammoCount == 0)
            return;

        var newBullet = Instantiate(bullet);
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = bullet.transform.position;
        GameObject.Destroy(newBullet.gameObject, 2);
        --ammoCount;

        //if(ammoCount ==0)
        //    reloadUI.gameObject.SetActive(true);
    }

    void DoReload()
    {
        
        timerReload += Time.deltaTime;
        //reloadSlider.value = timerReload;
        if (timerReload > reloadTimeSec)
        {
            ammoCount = ammoCountMax;
            timerReload = 0;
            //reloadUI.gameObject.SetActive(false);
        }
    }
}
