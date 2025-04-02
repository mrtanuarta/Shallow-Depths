using UnityEngine;

public class SpinningSoulShooter : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    public float lifetime = 10f; // Destroy after 3 seconds
    public GameObject bulletPrefab2; // Assign in Inspector
    public Transform firePoint; // Set a fire point where bullets spawn
    public float finalBulletSpeed;
    public float finalFireRate;
    private float detectionRange = 40f;
    private float nextFireTime;
    [SerializeField]private bool isAggressive = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firePoint = gameObject.transform;
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
        transform.Rotate(0, 0, 20f * Time.deltaTime); // Adjust speed as needed
        CheckAggression();
        OpacitySanity();
        updateBasedOnSanity();
        if (isAggressive && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / finalFireRate;
        }
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
        finalFireRate = Mathf.Lerp(16f, 8f, PlayerStats.Instance.getSanity() / 100f);
        finalBulletSpeed = Mathf.Lerp(16f, 8f, PlayerStats.Instance.getSanity() / 100f);
    }
    void Shoot()
    {
        Debug.Log("Shoot is touched");
        if (player == null || bulletPrefab2 == null || firePoint == null) return;
        Debug.Log("Shoots");
        float shooterRotation = transform.eulerAngles.z; // Get the current rotation of the shooter
        
        for (int i = 0; i < 8; i++) // Loop through 8 directions
        {
            float angle = shooterRotation + (i * 45f); // Add shooter rotation to bullet angle
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab2, transform.position, rotation);

            // Apply force or velocity to push bullets outward
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                rb.linearVelocity = direction * finalBulletSpeed;
            }
        }
    }
}
