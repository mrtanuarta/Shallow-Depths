using UnityEngine;

public class BulletNoEdit : MonoBehaviour
{
    public float lifetime = 2f;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float finalBulletSpeed;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after a while
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        finalBulletSpeed = Mathf.Lerp(9f, 5f, PlayerStats.Instance.getSanity() / 100f);
        
        rb.linearVelocity = new Vector2(finalBulletSpeed, 0f); // Move to the right
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
        if (other.CompareTag("Player"))
        {
            // Implement damage logic here
            Debug.Log("Player Hit!");
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemies")) // Prevent self-hit
        {
            Debug.Log("Not Enemies Hit");
            Destroy(gameObject);
        }
    }
}

