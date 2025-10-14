using System.Security.Cryptography;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    void Start()
    {
        //Destroy(this.gameObject, 3);
    }
    [SerializeField]
    public float ySpeed = 10;
    [SerializeField]
    public float xSpeed = 0;
    void Update()
    {
        transform.Translate((Vector2.up * ySpeed * Time.deltaTime) + (Vector2.right * xSpeed * Time.deltaTime));

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
