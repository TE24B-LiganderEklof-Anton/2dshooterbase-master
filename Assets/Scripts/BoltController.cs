using System;
using System.Security.Cryptography;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    [SerializeField]
    public float ySpeed = 10;
    [SerializeField]
    public float xSpeed = 0;
    [SerializeField]
    public string targetTag = "Enemy";

    void Update()
    {
        Vector2 moveVector = (Vector2.up * ySpeed * Time.deltaTime) + (Vector2.right * xSpeed * Time.deltaTime);
        transform.Translate(moveVector, Space.World);

        // this.gameObject.transform.rotation = Quaternion.LookRotation(moveVector.normalized, Vector3.forward);

        float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
        // print(angle);
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle-90);

        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            Destroy(this.gameObject);
        }
    }
}
