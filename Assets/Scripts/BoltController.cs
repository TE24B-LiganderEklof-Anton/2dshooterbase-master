using System.Security.Cryptography;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    [SerializeField]
    public float ySpeed = 10;
    [SerializeField]
    public float xSpeed = 0;
    [SerializeField]
    // public float damage = 1;

    void Update()
    {
        Vector2 moveVector = (Vector2.up * ySpeed * Time.deltaTime) + (Vector2.right * xSpeed * Time.deltaTime);
        transform.Translate(moveVector);

        

        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
