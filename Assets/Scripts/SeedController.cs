using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 targetDirection;
    private bool isTargetCalculated;
    private Rigidbody2D seedRb;
    private Vector3 movementVector;
    private bool isShot = false;
    private Vector3 cameraBounds;
    
    [SerializeField] private float seedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        seedRb = GetComponent<Rigidbody2D>();
        if(gameObject.CompareTag("Seed")){
            seedRb.drag = 3;
        }
        seedSpeed = 50f;
        isTargetCalculated = false;
        seedRb.AddForce(new Vector2(Random.Range(5, 10), Random.Range(5, 10)), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        cameraBounds = Camera.main.WorldToViewportPoint(transform.position);
        if(gameObject.CompareTag("Bullet Seed")){
            seedRb.drag = 0;
            if(!isTargetCalculated){
                targetDirection = targetPosition - transform.position;
                isTargetCalculated = true;
            }
        }
        // CheckCameraBounds();
    }

    void FixedUpdate() {
        if(gameObject.CompareTag("Bullet Seed") && isTargetCalculated && !isShot){
            Debug.Log(targetPosition);
            seedRb.velocity = targetDirection.normalized * seedSpeed;
            isShot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Boundary") && gameObject.CompareTag("Bullet Seed")){
            Debug.Log("Bullet Left Map");
            Destroy(gameObject);
        } else {
            Debug.Log("Seed Bounced Off Wall");
        }
    }
}
