using UnityEngine;

public class Gunscript : MonoBehaviour
{
    public GameObject gun1;
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            gun1.SetActive(true);
        }
    }
}
