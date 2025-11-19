using System;
using System.Drawing;
using System.Reflection;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject explosionPrefab;
    [SerializeField]
    public float speed = 5;

    [SerializeField]
    int maxRandomHp = 5;
    [SerializeField]
    int minRandomHp = 1;
    int hp;
    [SerializeField]
    float sizeMultiplier = 1.1f;
    float size = 1;

    [SerializeField]
    GameObject boltPrefab;
    [SerializeField]
    float minFireRateValue = 1;
    [SerializeField]
    float maxFireRateValue = 10;
    [SerializeField]
    UnityEngine.Color boltColor;

    float fireCooldown;
    float timeSinceLastFire = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float fireRate = UnityEngine.Random.Range(minFireRateValue, maxFireRateValue);
        fireRate = Mathf.Sqrt(fireRate)/2;
        fireCooldown = 1 / fireRate;

        hp = UnityEngine.Random.Range(minRandomHp, maxRandomHp+1);

        size = 1+(hp * sizeMultiplier);
        this.gameObject.transform.localScale *= size;

        Vector2 position = new();
        float aspectsize = Camera.main.aspect;
        float orthographicWidth = (Camera.main.orthographicSize * aspectsize) - 1;
        position.x = UnityEngine.Random.Range(-orthographicWidth, orthographicWidth);
        position.y = Camera.main.orthographicSize + (this.gameObject.transform.localScale.y/2);
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = Vector2.down*speed;
        transform.Translate(moveVector * Time.deltaTime);

        if (transform.position.y < -(Camera.main.orthographicSize + (this.gameObject.transform.localScale.y / 2)))
        {
            Destroy(this.gameObject);
        }
        if (timeSinceLastFire >= fireCooldown)
        {
            GameObject bolt = Instantiate(boltPrefab, transform.position, Quaternion.identity);
            BoltController boltController = bolt.GetComponent<BoltController>();
            boltController.xSpeed = -(moveVector.x+boltController.xSpeed);
            boltController.ySpeed = -(moveVector.y+boltController.ySpeed);
            boltController.targetTag = "Player";
            bolt.tag = "EnemyProjectile";
            bolt.GetComponent<SpriteRenderer>().color = boltColor;
            bolt.transform.localScale *= size;

            timeSinceLastFire = 0;
        }
        timeSinceLastFire += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            int damage = DataHandler.instance.upgrades["Damage"];
            hp -= damage;
        }
        else if (collision.gameObject.tag == "Player")
        {
            hp = 0;
        }
        if (hp <= 0)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<explosionController>().speed = speed;
            explosion.gameObject.transform.localScale *= size;
            // this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            DataHandler.instance.points += 1;
            Destroy(this.gameObject);
        }
    }
}