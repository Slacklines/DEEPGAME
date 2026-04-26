using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float speed;
    public float offset;
    public float SnappedYOffset;

    public bool breakable;
    public int health = 1;
    public GameObject breakParticles;

    void Start(){
        speed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().fallSpeed;
        offset = Time.time * speed * 10 - transform.position.y;
    }

    void Update()
    {
        float rawYOffset = Time.time * speed;

        SnappedYOffset = (Mathf.Floor(rawYOffset * 100) / 10) - offset;

        if (SnappedYOffset >= 14)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, SnappedYOffset, transform.position.z);
    }

    public void damage(){
        health--;
        if (health <= 0)
        {
            breakSelf();
        }
    }

    private void breakSelf(){
        Instantiate(breakParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
