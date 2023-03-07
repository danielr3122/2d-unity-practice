using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xMovementRange;
    private float yMovementRange;
    private float waterCount;
    private float waterTime;
    private float waterCollectTime;
    private float waterLimit;
    private bool nearWater;
    private int seedCount;
    private Vector3 movementVector;

    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float playerSpeed;

    private GameObject waterSource;
    private Camera mainCamera;
    public GameObject seedPrefab;
    public GameObject plantPrefab;
    public GameObject testReticle;

    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        xMovementRange = 8f;
        yMovementRange = 4.25f;
        waterCount = 0;
        waterTime = 0;
        waterLimit = 3;
        waterCollectTime = 1.5f;
        seedCount = 0;
        playerSpeed = 100f;
        playerRb = GetComponent<Rigidbody2D>();
        waterSource = GameObject.Find("Water Source");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        waterTime += Time.deltaTime;
        movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        ConstrainPlayer();
        NearWater();
        PlayerInteraction();
        testReticle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }

    private void PlayerInteraction(){
        // Shoot seed
        if(Input.GetMouseButtonDown(0)){
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if(seedCount > 0){
                Debug.Log(mousePosition);
                GameObject seed = Instantiate(seedPrefab, transform.position, transform.rotation);
                seed.GetComponent<SeedController>().targetPosition = mousePosition;
                seed.gameObject.tag = "Bullet Seed";
                seed.layer = LayerMask.NameToLayer("Bullet Seed");
                seedCount--;
            }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            if(seedCount > 0){
                // TODO: Plant seed
                Instantiate(plantPrefab, transform.position, plantPrefab.transform.rotation);
                seedCount--;
            }
        }
    }

    void FixedUpdate() {
        PlayerMovement(movementVector);
    }

    private void PlayerMovement(Vector3 moveToVec){
        playerRb.velocity = moveToVec * playerSpeed * Time.fixedDeltaTime;
    }

    private void ConstrainPlayer(){
        if(transform.position.x > xMovementRange){
            transform.position = new Vector3(xMovementRange, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -xMovementRange){
            transform.position = new Vector3(-xMovementRange, transform.position.y, transform.position.z);
        }
        if(transform.position.y > yMovementRange){
            transform.position = new Vector3(transform.position.x, yMovementRange, transform.position.z);
        }
        if(transform.position.y < -yMovementRange){
            transform.position = new Vector3(transform.position.x, -yMovementRange, transform.position.z);
        }
    }

    private void NearWater(){
        if((Mathf.Abs(waterSource.transform.position.x - transform.position.x) < 4) &&
        (Mathf.Abs(waterSource.transform.position.y - transform.position.y) < 2)){
            if(waterTime > waterCollectTime && waterCount < waterLimit){
                waterCount++;
                waterTime = 0;
                Debug.Log("Water Count: " + waterCount);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Seed")){
            Debug.Log("Player collided with seed");
            seedCount++;
            Destroy(other.gameObject);
        }
    }
}
