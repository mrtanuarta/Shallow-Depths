using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image imageComponent; // Assign your UI Image here
    public Sprite[] sprites; // Assign your sprites in the Inspector

    public void UpdateImage(int a)
    {
        int index = Mathf.Clamp((100 - a) / 20, 0, sprites.Length - 1); // Convert 'a' to an index
        imageComponent.sprite = sprites[index]; // Update the image
    }
    public void Update()
    {
        UpdateImage(PlayerStats.Instance.sanity);
    }
}

