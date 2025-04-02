using UnityEngine;

public class SpinningSoulShooter : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    public float lifetime = 3f; // Destroy after 3 seconds
    public GameObject bulletPrefab; // Assign in Inspector
    public Transform firePoint; // Set a fire point where bullets spawn
    public float bulletSpeed = 5f;
    public float finalBulletSpeed;
    public float finalFireRate;
    public float detectionRange = 10f;
    private float nextFireTime;
    private bool isAggressive = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player Not Found");
        }
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void CheckAggression()
    {
        if (player == null) return;
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        isAggressive = distance <= detectionRange;
    }
    void Update()
    {
        CheckAggression();
        OpacitySanity();
        updateBasedOnSanity();
    }
    void OpacitySanity()
    {
        if (sr == null || PlayerStats.Instance == null) return;
        
        float opacitySanity = (PlayerStats.Instance.getSanity() * -0.005f) + 0.75f;
        opacitySanity = Mathf.Clamp(opacitySanity, 0f, 1f);
        
        Color newColor = sr.color;
        newColor.a = opacitySanity;
        sr.color = newColor;
    }
    void updateBasedOnSanity()
    {
        finalBulletSpeed = Mathf.Lerp(9f, 5f, PlayerStats.Instance.getSanity() / 100f);
        finalFireRate = Mathf.Lerp(1f, 2f, PlayerStats.Instance.getSanity() / 100f);
    }
    void Shoot()
    {
        if (player == null || bulletPrefab == null || firePoint == null) return;
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * finalBulletSpeed;
        }
    }
}
