using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed;
    private Material mat;

    public float SnappedYOffset;

    void Start(){
        speed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().fallSpeed;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float rawYOffset = Time.time * speed;

        SnappedYOffset = (Mathf.Floor(rawYOffset * 100) / 100 ) % 1;

        mat.mainTextureOffset = new Vector2(0, -SnappedYOffset);
    }
}
