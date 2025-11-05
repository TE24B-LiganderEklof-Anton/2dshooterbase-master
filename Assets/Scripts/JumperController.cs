using UnityEngine;

public class JumperController : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    GameObject groundChecker;
    [SerializeField]
    LayerMask groundLayer;
    void Update()
    {
        Vector2 moveVector = Vector2.right;
        moveVector *= Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        transform.Translate(moveVector);
    }
    void FixedUpdate()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundChecker.transform.position, 0.01f, groundLayer);
        if (Input.GetAxisRaw("Jump") > 0 && isGrounded)
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.linearVelocityY = 0;
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // rigidBody.AddTorque(10, ForceMode2D.Impulse);
        }
    }
}
