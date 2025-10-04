using UnityEngine;

public class Gunscript : MonoBehaviour
{
    public GameObject gun1;

    bool activated;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun1.SetActive(activated);
            activated = !activated; 
        }
    }
}
