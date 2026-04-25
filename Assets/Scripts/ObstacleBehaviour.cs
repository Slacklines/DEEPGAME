using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float speed;
    public float offset;
    public float SnappedYOffset;

    void Start(){
        speed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().fallSpeed;
        offset = Time.time * speed * 10 + 7;
    }

    void Update()
    {
        float rawYOffset = Time.time * speed;

        SnappedYOffset = (Mathf.Floor(rawYOffset * 100) / 10) - offset;

        if (SnappedYOffset >= 7)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, SnappedYOffset, transform.position.z);
    }
}
