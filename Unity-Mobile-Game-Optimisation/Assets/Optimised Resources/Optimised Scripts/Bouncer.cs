using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{    
    public void OnEnable()
    {
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = RandomVector(0f, 10f);

        Invoke("destroy", 3f);
    }

    public Vector3 RandomVector(float min, float max)
    {
        float x = Random.Range(min, max);
        float y = Random.Range(min, max + 10f);
        return new Vector3(x, y, 0f);
    }

    public void destroy()
    {
        gameObject.SetActive(false);
    }
}
