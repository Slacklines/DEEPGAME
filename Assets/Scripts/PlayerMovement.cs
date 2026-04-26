using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public float recoilForce = 4f;
    private Rigidbody2D rb;
    public float cooldown = 10f;
    private float curCooldown = 0f;
    private bool shooting;
    public float bulletPower;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fallSpeed = .7f;

    public bool dead;
    public float deathForce = 10f;
    public float fallForce = 5f;
    public TextMeshProUGUI depthCounter;
    private float depth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead){
            /*if (deathWait <= 0)
            {
                GameObject.FindWithTag("Animator").GetComponent<animationHandler>().restartLevel();
            }
            deathWait -= Time.fixedDeltaTime;*/
            return;
        }

        depth = Mathf.Floor(Time.timeSinceLevelLoad * fallSpeed * 5);

        depthCounter.text = depth.ToString() + " M";

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

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up*bulletPower, ForceMode2D.Force);

            shooting = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy") && !dead)
        {
            playerDeath();
        }
    }

    float getAngle(Vector2 a, Vector2 b){
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void playerDeath()
    {
        Debug.Log("Player dead");
        dead = true;

        GameObject.FindWithTag("Floor").SetActive(false);
        GameObject.FindWithTag("Roof").SetActive(false);

        rb.gravityScale=fallForce;
        rb.drag = 0.5f;
        rb.angularDrag = 0f;

        Quaternion randomAngle = Quaternion.Euler(0,0,Random.Range(-45f, 45f));

        Vector3 forceVector = randomAngle * Vector3.up;

        rb.AddForce(forceVector*deathForce, ForceMode2D.Impulse);

        GameObject.FindWithTag("Animator").GetComponent<animationHandler>().restartLevel();
    }
}
 