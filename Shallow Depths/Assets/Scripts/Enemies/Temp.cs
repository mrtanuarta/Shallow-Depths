using UnityEngine;

public class Temp : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,15f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}
