using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(new Vector2(0f, -1) * Time.deltaTime * speed);

        if(transform.position.y < -4.4f)
        {
            Destroy(gameObject);
        }
    }
}
