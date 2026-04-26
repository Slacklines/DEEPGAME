using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    int health = 1;

    public int stepNum = 100;
    public float stepSize;
    public float startLoc;

    public string enemyType;

    public GameObject deathParticles;
    //float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyType == "Vertical" || enemyType == "Horizontal")
        {
            newTarget(0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyType == "Vertical" || enemyType == "Horizontal")
        {
            if (stepNum > 100)
            {
                newTarget();
            }

            if (enemyType == "Vertical")
            {
                transform.position = new Vector3(transform.position.x, startLoc+stepSize*stepNum ,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(startLoc+stepSize*stepNum ,transform.position.y, transform.position.z);
            }

            stepNum++;
        }
    }

    void newTarget(float targetLoc = -5f)
    {
        float distance;
        if (enemyType == "Vertical")
        {
            startLoc = transform.position.y;

            if (targetLoc == -5f)
            {
                targetLoc = UnityEngine.Random.Range(-4f, 4f);
            }

            distance = targetLoc - transform.position.y;
        }else
        {
            startLoc = transform.position.x;

            if (targetLoc == -5f)
            {
                targetLoc = UnityEngine.Random.Range(-5f, 5f);
            }

            distance = targetLoc - transform.position.x;           
        }

        stepSize = distance/100;
        stepNum = 1;
    }

    public void damage()
    {
        health -= 1;
        if (health <= 0)
        {
            enemyDeath();
        }
    }

    void enemyDeath()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
