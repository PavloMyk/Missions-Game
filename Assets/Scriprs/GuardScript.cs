using TMPro;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    public GameObject player;
    public float viewDistance = 10f;
    public float viewAngle = 60f; // кут огляду в градусах
    public ParticleSystem ps;
    public float alarm = 0f;

    void FixedUpdate()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < viewAngle / 2f)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, viewDistance))
            {
                if (hit.collider.gameObject == player)
                {

                    float green = Mathf.Clamp01(1f - alarm);
                    Color alertColor = new Color(225, green * 250, green * 250);

                    var main = ps.main;
                    main.startColor = alertColor;
                    if (alarm < 1f)
                        alarm += 0.000000001f;
                }
            }
        }
    }
}