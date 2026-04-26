using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleHandler : MonoBehaviour
{
    public float period;
    private float curPeriod = 6f;
    private float stage;
    private float depth;
    private float speed;
    public GameObject[] wallObstacles;
    public GameObject[] freeObstacles;
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
        else
        {
            float ran = UnityEngine.Random.Range(0f, 1f);

            depth = Mathf.Floor(Time.timeSinceLevelLoad * speed * 5);
            if (depth < 80)
            {
                if (ran < .8f)
                {
                    summonWallObstacle();
                }
            }
            else if(depth < 200)
            {
                if (ran < .7f)
                {
                    summonWallObstacle();
                }
                else if (ran < .85f)
                {
                    summonEnemy(0);
                }
            }
            else
            {
                if (ran < .65f)
                {
                    summonWallObstacle();
                }
                else if (ran < .90f)
                {
                    summonEnemy();
                }
                else if (ran < .97f)
                {
                    summonFreeObstacle();
                }
                if (period == 3f && depth > 300)
                {
                    period = 1.5f;
                }
            }

            //stage++;
            curPeriod = period;
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

        GameObject curObstacle = Instantiate(obstacle, new Vector3(x+offset,transform.position.y,-3), quaternion.identity);
        curObstacle.transform.rotation = Quaternion.Euler( new Vector3(curObstacle.transform.rotation.x,180*ranWall,curObstacle.transform.rotation.z) );
    }    
    
    void summonFreeObstacle(int obstacleIndex = -1)
    {
        var positions = new Dictionary<int, float>
        {
            [0] = -.1f,
            [1] = -.1f
        };

        if (obstacleIndex == -1)
        {
            obstacleIndex = UnityEngine.Random.Range(0, freeObstacles.Length);
        }
        GameObject obstacle = freeObstacles[obstacleIndex];


        float x = 3;

        if (positions.ContainsKey(obstacleIndex))
        {
            x = positions[obstacleIndex];
        }
        else
        {
            x = 3;
        }

        Instantiate(obstacle, new Vector3(x,transform.position.y,-3), quaternion.identity);
    }

    void summonEnemy(int enemyIndex = -1)
    {
        if (enemyIndex == -1)
        {
            enemyIndex = UnityEngine.Random.Range(0, enemies.Length);
        }

        GameObject enemy = enemies[enemyIndex];

        Vector3 spawnPos;

        if (enemyIndex == 0)
        {
            float spawnerY = transform.position.y;
            float y = UnityEngine.Random.Range(0,2)*(2*spawnerY)-spawnerY;
            spawnPos = new Vector3 (0, y, -2);
        }
        else
        {
            int ranWall = UnityEngine.Random.Range(0,2);
            float playerY = GameObject.FindWithTag("Player").transform.position.y;
            spawnPos = new Vector3 (ranWall*16-8, playerY, -2);
        }

        Instantiate(enemy, spawnPos, quaternion.identity);
    }
}
