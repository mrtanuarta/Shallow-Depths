using UnityEngine;

public class Reflection : MonoBehaviour
{
    public SpriteRenderer playerRenderer;  // Assign this manually
    private SpriteRenderer reflectionRenderer;

    void Start()
    {
        reflectionRenderer = GetComponent<SpriteRenderer>();

        if (playerRenderer == null)
        {
            // Try finding the player automatically
            GameObject player = GameObject.Find("Player");
            if (player != null)
                playerRenderer = player.GetComponent<SpriteRenderer>();
        }

        if (reflectionRenderer == null)
            Debug.LogError("Reflection Renderer not found");

        if (playerRenderer == null)
            Debug.LogError("Player Renderer not found");
    }

    void LateUpdate()
    {
        if (playerRenderer != null && reflectionRenderer != null)
        {
            reflectionRenderer.sprite = playerRenderer.sprite;
            reflectionRenderer.flipX = playerRenderer.flipX;
        }
    }
}
