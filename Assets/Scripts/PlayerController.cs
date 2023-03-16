using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float xMovementRange;
    private float yMovementRange;
    private float waterCount;
    private float waterTime;
    private float waterCollectTime;
    private float waterLimit;
    private bool nearWater;
    public bool canPlantHere;
    private int seedCount;
    private Vector3 movementVector;

    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float playerSpeed;

    private GameObject waterSource;
    public GameObject seedPrefab;
    public GameObject plantPrefab;
    public GameObject testReticle;

    public Image waterLevelOne;
    public Image waterLevelTwo;
    public Image waterLevelThree;

    public Sprite emptyWaterSprite;
    public Sprite fullWaterSprite;

    public SpriteRenderer reticleRenderer;
    public Sprite defaultReticleSprite;
    public Sprite plantingReticleSprite;
    public Sprite wateringReticleSprite;

    private Rigidbody2D playerRb;

    private Camera mainCamera;

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
        testReticle.transform.position += Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        waterTime += Time.deltaTime;
        movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        ConstrainPlayer();
        NearWater();
        PlayerInteraction();

        if(waterCount == 1){
            waterLevelOne.sprite = fullWaterSprite;
            waterLevelTwo.sprite = emptyWaterSprite;
            waterLevelThree.sprite = emptyWaterSprite;
        } else if(waterCount == 2){
            waterLevelOne.sprite = fullWaterSprite;
            waterLevelTwo.sprite = fullWaterSprite;
            waterLevelThree.sprite = emptyWaterSprite;
        } else if(waterCount == 3){
            waterLevelOne.sprite = fullWaterSprite;
            waterLevelTwo.sprite = fullWaterSprite;
            waterLevelThree.sprite = fullWaterSprite;
        } else {
            waterLevelOne.sprite = emptyWaterSprite;
            waterLevelTwo.sprite = emptyWaterSprite;
            waterLevelThree.sprite = emptyWaterSprite;
        }
    }

    private void PlayerInteraction(){
        // Shoot seed
        if(Input.GetMouseButtonDown(0) && !Input.GetButton("E")){
            Debug.Log("Shoot");
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

        if(Input.GetButton("E")){
            reticleRenderer.sprite = plantingReticleSprite;
            if(Input.GetMouseButtonDown(0)){
                Debug.Log("Plant");
                if(seedCount > 0){
                    Instantiate(plantPrefab, testReticle.transform.position, plantPrefab.transform.rotation);
                    seedCount--;
                }
            }
        } else {
            reticleRenderer.sprite = defaultReticleSprite;
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
        if((Mathf.Abs(waterSource.transform.position.x - transform.position.x) < 3) &&
        (Mathf.Abs(waterSource.transform.position.y - transform.position.y) < 2)){
            if(waterTime > waterCollectTime && waterCount < waterLimit){
                waterCount++;
                waterTime = 0;
                Debug.Log("Water Count: " + waterCount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Seed")){
            Destroy(other.gameObject);
            Debug.Log("Player collided with seed");
            seedCount++;
        }
    }
}
