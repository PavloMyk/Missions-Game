using UnityEngine;
using UnityEngine.UI;
public class GuardScript : MonoBehaviour
{
    public GameObject player;
    public float viewDistance = 10f;
    public float viewAngle = 60f; // ��� ������ � ��������
    public GameObject im; // UI Image GameObject
    public float alarm = 0f;
    public float alarmSpeed = 0.01f;
    public float antiAlarmSpeed = 0.005f;
    public float health = 100f;

    void FixedUpdate()
    {
        float PlayerVisibility = player.GetComponent<PlayerMovement>().playerVisibility;
        Vector3 dirToPlayer = player.transform.position - transform.position;
        Vector3 directionToPlayer = dirToPlayer.normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        static void Alarmed()
        {
            Debug.Log("��������� � ���� �������!");
        }

        if (alarm >= 1f)
        {
            Alarmed();
        }
        if (angleToPlayer < viewAngle * 0.5f)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, viewDistance))
            {
                if (hit.collider.gameObject == player)
                {
                    // �������� alarm � �������� �� 0 � 1
                    alarm += alarmSpeed * PlayerVisibility;

                    // ��������� ���� �� ����� alarm. �������� Color � Unity �� 0 �� 1
                    // ��� alarm=0 -> �������; ��� alarm=1 -> ��������
                    Color alertColor = Color.Lerp(Color.green, Color.red, alarm);

                    Image img = im.GetComponent<Image>();
                    if (img != null)
                        img.color = alertColor;

                    Debug.Log("Alarm: " + alarm);
                }
            }
        }   else
            {
                // ���� ������� ���� �����, ��������� �������� alarm
                alarm = Mathf.Clamp01(alarm - antiAlarmSpeed);
                Image img = im.GetComponent<Image>();
                if (img != null)
                {
                    img.color = Color.Lerp(Color.green, Color.red, alarm);
                }
            }
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
