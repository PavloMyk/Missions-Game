using TMPro;
using UnityEngine;

public class Gunscript : MonoBehaviour
{
    [Header("References")]
    public GameObject gun1;
    public GameObject canvas;
    public TextMeshProUGUI ammoText;

    [Header("Weapon Values")]
    public float damage = 30f;
    public int magazineSize = 10;        
    public int reserveAmmo = 30;         

    private int currentAmmo;             
    private bool activated = false;

    [Header("Player Visibility Penalty")]
    public float visibilityPenalty = 10f;

    void Start()
    {
        currentAmmo = magazineSize;
        UpdateUI();
        if (canvas != null) canvas.SetActive(false);
        if (gun1 != null) gun1.SetActive(false);
    }

    void Update()
    {
        HandleSwitch();
        HandleShooting();
        UpdateUI();
    }

    void HandleSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activated = !activated;

            if (gun1 != null) gun1.SetActive(activated);
            if (canvas != null) canvas.SetActive(activated);

            // Безпечна робота з компонентом PlayerMovement
            PlayerMovement pm = GetComponent<PlayerMovement>();
            if (pm != null)
            {
                if (activated)
                    pm.playerVisibility += visibilityPenalty;
                else
                    pm.playerVisibility = Mathf.Max(0f, pm.playerVisibility - visibilityPenalty);
            }
        }
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && activated && currentAmmo > 0)
        {
            // Вистріл
            Debug.Log("Вистріл!");

            Vector3 origin = gun1 != null ? gun1.transform.position : transform.position;
            Vector3 dir = gun1 != null ? gun1.transform.forward : transform.forward;

            Ray ray = new Ray(origin, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                GuardScript guard = hit.transform.GetComponent<GuardScript>();
                if (guard != null)
                {
                    guard.health -= damage;
                    guard.alarm = 10f;
                    Debug.Log("Влучив у охоронця! Залишилось HP: " + guard.health);
                }
                else
                {
                    CameraScript camera = hit.transform.GetComponent<CameraScript>();
                    if (camera != null)
                    {
                        camera.health -= damage;
                        camera.alarm = 10f;
                        Debug.Log("Влучив у Камеру! Залишилось HP: " + camera.health);
                    }
                }
            }

            currentAmmo--;
        }

        // Перезарядка
        if (Input.GetKeyDown(KeyCode.R) && activated)
        {
            Reload();
        }
    }

    void Reload()
    {
        if (currentAmmo == magazineSize || reserveAmmo == 0) return;

        int needed = magazineSize - currentAmmo;
        int taken = Mathf.Min(needed, reserveAmmo);

        currentAmmo += taken;
        reserveAmmo -= taken;

        Debug.Log($"Перезаряджено: +{taken} патронів. Магазин: {currentAmmo}, Запас: {reserveAmmo}");
    }

    void UpdateUI()
    {
        if (ammoText != null)
            ammoText.text = $"{currentAmmo} / {reserveAmmo}";
    }
}
