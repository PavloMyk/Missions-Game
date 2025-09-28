using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}