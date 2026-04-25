using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Bullet hit");
            other.gameObject.GetComponent<EnemyBehavior>().damage();
        }
        Destroy(gameObject);
    }
}
