using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject explosionPrefab;   
    [SerializeField]
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 position = new();
        float aspectsize = Camera.main.aspect;
        float orthographicWidth = (Camera.main.orthographicSize * aspectsize) - 1;
        position.x = Random.Range(-orthographicWidth, orthographicWidth);
        position.y = Camera.main.orthographicSize + 1;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = Vector2.down;
        transform.Translate(moveVector * Time.deltaTime * speed);

        if (transform.position.y < -(Camera.main.orthographicSize + 1))
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<explosionController>().speed = speed;
        // this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        DataHandler.instance.points += 1;
        Destroy(this.gameObject);
    }
}
