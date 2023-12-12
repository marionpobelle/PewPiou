using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundProp : MonoBehaviour
{
    float speed;
    public void Init(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        if(transform.position.x < -200)
        {
            Destroy(gameObject);
        }

        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
