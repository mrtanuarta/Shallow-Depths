using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer sr;
    public float lifetime = 3f; // Destroy after 3 seconds

    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after a while
        sr = GetComponent<SpriteRenderer>();
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
            Destroy(gameObject);
        }
    }
}


