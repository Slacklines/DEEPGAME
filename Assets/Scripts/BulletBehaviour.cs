using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour
{
    
    public GameObject bulletDeath;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 incomingDir = other.relativeVelocity.normalized * -1f;
        Vector2 colliderNormal = other.contacts[0].normal;
        Vector2 reflectDir = Vector2.Reflect(incomingDir, colliderNormal);

        float angle = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;

        killBullet(other.gameObject.tag, angle);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().damage();
            Destroy(gameObject);
        }
    }

    public void killBullet(string tag, float angle){

            GameObject myBulletDeath = Instantiate(bulletDeath, transform.position, Quaternion.Euler(0, 0, angle));

            Destroy(gameObject);
    }
}
