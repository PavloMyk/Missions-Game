using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    [Header("HeadReferences")]
    public GameObject player;
    public GameObject im;

    [Header("Guard Settings")]
    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public float alarm = 0f;
    public float alarmSpeed = 0.001f;
    public float antiAlarmSpeed = 0.005f;
    public float health = 100f;

    [Header("Alert Settings")]
    public float alertDelaySeconds = 3f; // ск≥льки секунд треба, щоб оголосити тривогу
    private bool isCounting = false;
    private bool isAlerted = false;

    void FixedUpdate()
    {
        float PlayerVisibility = player.GetComponent<PlayerMovement>().playerVisibility;
        Vector3 dirToPlayer = player.transform.position - transform.position;
        Vector3 directionToPlayer = dirToPlayer.normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // якщо alarm повний ≥ ще не почавс€ в≥дл≥к ≥ ще не в стан≥ тривоги Ч стартуЇмо в≥дл≥к
        if (alarm >= 1f && !isCounting && !isAlerted)
        {
            StartCoroutine(AlertCountdown());
        }

        Image img = im.GetComponent<Image>();
        if (img != null)
            img.color = Color.Lerp(Color.green, Color.red, alarm);

        // якщо вже у стан≥ тривоги Ч можна виконувати ≥нш≥ д≥њ тут
        if (isAlerted)
        {
            // ѕриклад: пересл≥дуванн€ гравц€ (можеш зам≥нити на власну лог≥ку)
            // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 3f * Time.deltaTime);
        }

        if (angleToPlayer < viewAngle * 0.5f)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, viewDistance))
            {
                if (hit.collider.gameObject == player)
                {
                    // ЌарощуЇмо alarm
                    alarm += alarmSpeed * PlayerVisibility; // прив'€зка до кадр≥в €к в попередн≥х прикладах
                }
            }
        }
        else
        {
            if (alarm != 1f)
            {
                // якщо гравець поза зоною, зменшуЇмо alarm
                alarm = Mathf.Clamp01(alarm - antiAlarmSpeed);
            }
        }
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AlertCountdown()
    {
        isCounting = true;
        float timer = 0f;

        // ¬≥дл≥к поки alarm >= 1f
        while (timer < alertDelaySeconds)
        {
            OnAlert();
            timer += Time.deltaTime;
            yield return null;
        }

        // ѕ≥сл€ в≥дл≥ку Ч стан тривоги
        isAlerted = true;
        isCounting = false;
        Debug.Log($"{name} оголосив тривогу!");
    }

    private void OnAlert()
    {
        // ѕриклад: в≥дразу встановимо ≥ндикатор у червоне та ф≥ксуЇмо alarm
        alarm = 1000f;
        Image img = im.GetComponent<Image>();
        if (img != null) img.color = Color.red;

        // TODO: додай тут власну лог≥ку (наприклад, виклик п≥дкр≥пленн€ або зм≥ну повед≥нки)
    }
}