using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WanderingSoul : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    [SerializeField]private float speed = 3.5f;
    [SerializeField]
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null){
            Debug.LogWarning("Player Not Found");
        }
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(SelfDestroy());
    }
    void Update()
    {
        if (GlobalVariable.Instance.onWater == true){
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)player.transform.position + new Vector2(0,-1), speed*Time.deltaTime);
        } else {
            transform.position = transform.position;
        }
        OpacitySanity();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerReflection"))
        {
            // Implement damage logic here
            Debug.Log("Player Hit!");
            GlobalVariable.Instance.addKarma(-10);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemies")&& !other.CompareTag("LandBorder")) // Prevent self-hit
        {
            Destroy(gameObject);
        }
    }
    void OpacitySanity()
    {
        float OpacitySanity = (PlayerStats.Instance.getSanity()*-0.005f) + 0.75f;
        Color newColor = sr.color;
        newColor.a = OpacitySanity;
        sr.color = newColor;
    }
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(15f); // Wait for 15 seconds
        Destroy(gameObject);
    }
}
