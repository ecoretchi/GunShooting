using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformConvController : MonoBehaviour
{
    [Range(0.1f, 13.0f)]
    public float speed = 2;

    float zOffset = 0;

    readonly List<Transform> platforms = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            platforms.Add(child);
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
                float zOffset = Random.Range(-1.5f, 1.5f);
                this.zOffset += zOffset;
                this.zOffset = Mathf.Clamp(this.zOffset, 0, 15);
                platform.position = new Vector3(Random.Range(-5f, 5f), 0, 36 + this.zOffset );
                GameController.Instance.OnPlatformSpawn(platform);
            }
        }
    }
}
