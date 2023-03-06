using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool startMoving;
    private float xMovementRange;
    private float yMovementRange;
    private Vector3 targetDirection;
    private bool isTargetCalculated;
    
    [SerializeField] private float seedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        xMovementRange = 10f;
        yMovementRange = 7f;
        seedSpeed = 5f;
        isTargetCalculated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving){
            gameObject.tag = "Bullet Seed";
            if(!isTargetCalculated){
                targetDirection = targetPosition - transform.position;
                isTargetCalculated = true;
            }
            // transform.Translate((targetPosition - transform.position) * seedSpeed * Time.deltaTime);
            transform.position += Time.deltaTime * targetDirection * seedSpeed;
            // transform.position = Vector2.MoveTowards(transform.position, targetPosition, seedSpeed);
        }
        ConstrainSeed();
    }

    private void ConstrainSeed(){
        if(transform.position.x > xMovementRange ||
           transform.position.x < -xMovementRange ||
           transform.position.y > yMovementRange ||
           transform.position.y < -yMovementRange){
            Destroy(gameObject);
        }
    }
}
