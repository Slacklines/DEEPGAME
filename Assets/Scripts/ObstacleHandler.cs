using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleHandler : MonoBehaviour
{
    public float period;
    private float curPeriod;
    private float stage;
    private float speed;
    public GameObject[] wallObstacles;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {

        speed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (curPeriod > 0){
            curPeriod -= Time.fixedDeltaTime;
        }
        else if (UnityEngine.Random.Range(0f, 1f) < .3)
        {

            if (UnityEngine.Random.Range(0f, 1f) < .15f)
            {
                summonEnemy();
            }
            else
            {
                summonWallObstacle();
            }

            stage++;
            curPeriod = period;
            period = Mathf.Max(period-.1f, .15f);
        }
    }

    void summonWallObstacle(int obstacleIndex = -1)
    {
        if (obstacleIndex == -1)
        {
            obstacleIndex = UnityEngine.Random.Range(0, wallObstacles.Length);
        }
        int ranWall = UnityEngine.Random.Range(0,2);

        GameObject obstacle = wallObstacles[obstacleIndex];
        float x = ranWall*12.1f-6.1f;

        float obstacleWidth = obstacle.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float offset = (1-2*ranWall)*obstacleWidth/2;

        GameObject curObstacle = Instantiate(obstacle, new Vector3(x+offset,-7,-1), quaternion.identity);
        curObstacle.transform.rotation = Quaternion.Euler( new Vector3(curObstacle.transform.rotation.x,180*ranWall,curObstacle.transform.rotation.z) );
    }

    void summonEnemy(int enemyIndex = -1)
    {
        if (enemyIndex == -1)
        {
            enemyIndex = UnityEngine.Random.Range(0, enemies.Length);
        }
        int ranWall = UnityEngine.Random.Range(0,2);

        GameObject enemy = enemies[enemyIndex];

        float y = UnityEngine.Random.Range(0,2)*14-7;

        Instantiate(enemy, new Vector3(-.05f,y,-1), quaternion.identity);
    }
}
