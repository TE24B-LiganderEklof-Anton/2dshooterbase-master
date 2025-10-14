using UnityEngine;

public class explosionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector3 position = this.gameObject.transform.position;
        // position.z = -1f;
        // transform.Translate(position);
        Destroy(this.gameObject, 0.375f);
    }

    // Update is called once per frame
    [SerializeField]
    public float speed = 10;
    void Update()
    {
        transform.Translate(Vector2.down*speed*Time.deltaTime);
    }

}
