using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    int health = 1;

    public int stepNum = 100;
    float stepSize;
    float startLoc;

    public GameObject deathParticles;
    //float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        newTarget(0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stepNum > 100)
        {
            newTarget();
        }

        transform.position = new Vector3(transform.position.x, startLoc+stepSize*stepNum ,transform.position.z);

        stepNum++;


    }

    void newTarget(float targetLoc = -5f)
    {
        startLoc = transform.position.y;

        if (targetLoc == -5)
        {
            targetLoc = UnityEngine.Random.Range(-4f, 4f);
        }
        float distance = targetLoc - transform.position.y;
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
