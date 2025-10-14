using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.transform.position.x;
        transform.position = pos;
    }
    
}
