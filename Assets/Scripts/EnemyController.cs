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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = UnityEngine.Random.Range(minRandomHp, maxRandomHp+1);

        size = 1+(hp * sizeMultiplier);
        this.gameObject.transform.localScale *= size;
        // print(size);

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
        Vector2 moveVector = Vector2.down;
        transform.Translate(moveVector * Time.deltaTime * speed);

        if (transform.position.y < -(Camera.main.orthographicSize + (this.gameObject.transform.localScale.y/2)))
        {
            Destroy(this.gameObject);
        }
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
        print(hp);
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
