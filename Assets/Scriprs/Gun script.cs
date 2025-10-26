using UnityEngine;

public class Gunscript : MonoBehaviour
{
    public GameObject gun1;
    public float Damage = 10;

    private bool activated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activated = !activated;
            gun1.SetActive(activated);
            if (activated)
            {
                gameObject.GetComponent<PlayerMovement>().playerVisibility = 10f;
            }
            else
            {
                gameObject.GetComponent<PlayerMovement>().playerVisibility = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && activated)
        {
            Debug.Log("Вистріл!");
            Ray ray = new Ray(gun1.transform.position, gun1.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                GuardScript guard = hit.transform.GetComponent<GuardScript>();
                if (guard != null)
                {
                    guard.health -= Damage;
                    guard.alarm = 1f;
                    Debug.Log("Влучив у охоронця! Залишилось HP: " + guard.health);
                }
            }
        }
    }
}