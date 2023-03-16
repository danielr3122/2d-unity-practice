using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private float chaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        chaseSpeed = .1f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * chaseSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Bullet Seed")){
            Debug.Log("Enemy Hit");
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
