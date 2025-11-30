using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 90f; // градусів за секунду
    public KeyCode interactKey = KeyCode.F;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, openAngle, 0f));
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
        }

        // рівномірне обертання без easing
        if (isOpen)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                openRotation,
                openSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                closedRotation,
                openSpeed * Time.deltaTime
            );
        }
    }
}