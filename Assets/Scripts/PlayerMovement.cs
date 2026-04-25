using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float recoilForce = 4f;
    private Rigidbody2D rb;
    public float cooldown = 10f;
    private float curCooldown = 0f;
    private bool shooting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        float angle = getAngle(transform.position, mousePosition);

        transform.rotation = Quaternion.Euler( new Vector3(0f,0f,angle) );

        if (Input.GetKeyDown("space") && !shooting && curCooldown <= 0){
            shooting = true;
        }
        
        if(curCooldown > 0){
            curCooldown -= Time.fixedDeltaTime;
        }
    }

    void FixedUpdate(){
        if (shooting){
            rb.AddForce(transform.right * recoilForce, ForceMode2D.Impulse);
            curCooldown = cooldown;
            shooting = false;
        }
    }

    float getAngle(Vector2 a, Vector2 b){
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
