using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedNote : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
