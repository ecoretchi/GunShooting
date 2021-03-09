using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformConvController : MonoBehaviour
{
    [Range(0.1f, 13.0f)]
    public float speed = 2;

    readonly List<Transform> platforms = new List<Transform>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            platforms.Add(child);
            GameController.Instance.OnPlatfromInit(child);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var platform in platforms)
        {
            platform.Translate(Vector3.forward * Time.deltaTime * (-speed));
            if (platform.position.z < -12)
            {
                GameController.Instance.OnPlatformSpawn(platform);
            }
        }
    }
}
