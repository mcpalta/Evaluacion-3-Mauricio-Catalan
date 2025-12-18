using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Image fillImage;
    public Vector3 offset = new Vector3(0, 2.5f, 0);

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (health == null || cam == null) return;

        fillImage.fillAmount =
            (float)health.currentHealth / health.maxHealth;

        transform.position = health.transform.position + offset;
        transform.forward = cam.transform.forward;
    }
}
