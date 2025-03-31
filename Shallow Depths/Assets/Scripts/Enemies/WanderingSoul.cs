using Unity.VisualScripting;
using UnityEngine;

public class WanderingSoul : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private float speed = 3.5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null){
            Debug.LogWarning("Player Not Found");
        }
    }
    void Update()
    {
        if (GlobalVariable.Instance.onWater == true){
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)player.transform.position + new Vector2(0,-1), speed*Time.deltaTime);
        } else {
            transform.position = transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerReflection")){
            GlobalVariable.Instance.karma -= 10;
            Destroy(this.gameObject);
        }
    }
}
