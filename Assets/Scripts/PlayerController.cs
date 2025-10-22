using UnityEngine;
using UnityEngine.Animations;
// using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    float speed = 0.01f;
    [SerializeField]
    GameObject boltprefab;
    float timeSinceLastShot = 0;
    // [SerializeField]
    float timeBetweenShots = 1;
    // [SerializeField]
    float maxHp = 1;
    float currentHp = 0;
    [SerializeField]
    Slider hpSlider;


    // [SerializeField]
    // float pitchvariation = 0;
    // float pitch = shootSound.pitch;
    void Start()
    {
        speed = DataHandler.instance.upgrades["MoveSpeed"];
        maxHp = DataHandler.instance.upgrades["Health"];
        float attackspeed = DataHandler.instance.upgrades["AttackSpeed"];
        timeBetweenShots = 1/attackspeed;

        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;

    }
    void Update()
    {
        //==============================================================================================
        //Calculate Movement
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        float deltatime = Time.deltaTime;
        Vector2 movementvector = ((Vector2.right * inputX) + (Vector2.up * inputY)).normalized;
        Vector2 finalMovement = movementvector * speed * deltatime;

        //==============================================================================================
        //Limit Movement
        float maxY = Camera.main.orthographicSize;
        float maxX = Camera.main.orthographicSize * Camera.main.aspect;
        Vector2 futurePosition = (Vector2)transform.position + finalMovement;

        if (futurePosition.x > maxX)
        {
            finalMovement.x = maxX - transform.position.x;
        }
        else if (futurePosition.x < -maxX)
        {
            finalMovement.x = -maxX - transform.position.x;
        }
        if (futurePosition.y > maxY)
        {
            finalMovement.y = maxY - transform.position.y;
        }
        else if (futurePosition.y < -maxY)
        {
            finalMovement.y = -maxY - transform.position.y;
        }
        //==============================================================================================
        //Move
        transform.Translate(finalMovement);
        //==============================================================================================
        //Shooting:
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetAxisRaw("Fire1") > 0 && timeSinceLastShot >= timeBetweenShots)
        {
            float ySpeed = finalMovement.y / Time.deltaTime;
            float xSpeed = finalMovement.x / Time.deltaTime;
            GameObject bolt = Instantiate(boltprefab, transform.position, Quaternion.identity);
            bolt.GetComponent<BoltController>().ySpeed += ySpeed;
            bolt.GetComponent<BoltController>().xSpeed += xSpeed;

            // float maxPitch = pitch + pitchvariation;
            // float randompitch = Random.Range(-maxPitch,maxPitch);
            // shootSound.pitch = randompitch;
            AudioSource shootSound = GetComponent<AudioSource>();
            shootSound.Play();
            timeSinceLastShot = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHp--;
            hpSlider.value = currentHp;
        }

        if (currentHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

}
