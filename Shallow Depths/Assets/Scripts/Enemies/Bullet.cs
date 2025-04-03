using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float lifetime = 20f; // Destroy after 3 seconds

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after a while
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb.linearVelocity == null)
        {
            rb.linearVelocity = transform.right * 5f; // Adjust speed as needed
        }
    }
    void Update()
    {
        OpacitySanity();
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerReflection"))
        {
            // Implement damage logic here
            Debug.Log("Player Hit!");
            PlayerStats.Instance.addKarma(-5);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemies") && !other.CompareTag("LandBorder") && !other.CompareTag("Player")) // Prevent self-hit
        {
            Destroy(gameObject);
        }
    }
}


