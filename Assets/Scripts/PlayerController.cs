using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float xMovementRange;
    private float yMovementRange;
    private float waterTime;
    private float waterCollectTime;
    private float waterLimit;
    private float horizontalSpeed;
    private float verticalSpeed;
    private float playerSpeed;

    private bool nearWater;

    private int seedCount;

    private Vector3 movementVector;

    private Animator animator;

    private Rigidbody2D playerRb;

    private Camera mainCamera;

    public GameObject seedPrefab;
    public GameObject plantPrefab;
    public GameObject reticle;
    public GameObject waterSource;

    public bool canPlantHere;
    public float waterCount;

    public Image waterLevelOne;
    public Image waterLevelTwo;
    public Image waterLevelThree;

    public SpriteRenderer reticleRenderer;
    public SpriteRenderer seedCannonRenderer;

    public Sprite emptyWaterSprite;
    public Sprite fullWaterSprite;
    public Sprite defaultReticle;
    public Sprite plantingReticle;
    public Sprite plantingBlockedReticle;
    public Sprite wateringReticle;
    public Sprite wateringBlockedReticle;
    public Sprite unloadedSeedCannon;
    public Sprite loadedSeedCannonSprite;

    public GameManagerController gameManager;
    private ReticleController reticleController;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        reticleController = reticle.GetComponent<ReticleController>();
        playerRb = GetComponent<Rigidbody2D>();
        xMovementRange = 8f;
        yMovementRange = 4.25f;
        waterCount = 0;
        waterTime = 0;
        waterLimit = 3;
        waterCollectTime = 1.5f;
        seedCount = 0;
        playerSpeed = 100f;
        reticle.transform.position += Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        waterTime += Time.deltaTime;
        movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        ConstrainPlayer();
        PlayerInteraction();
        UpdateWaterStorage();
        UpdateSeedCannon();
    }

    private void UpdateSeedCannon(){
        if(seedCount > 0){
            seedCannonRenderer.sprite = loadedSeedCannonSprite;
        } else {
            seedCannonRenderer.sprite = unloadedSeedCannon;
        }
    }

    private void UpdateWaterStorage(){
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
        if(Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q)){
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

        if(Input.GetKey(KeyCode.E)){
            if(reticleController.isInRange){
                reticleRenderer.sprite = plantingReticle;
                if(Input.GetMouseButtonDown(0)){
                    Debug.Log("Plant");
                    if(seedCount > 0){
                        Instantiate(plantPrefab, reticle.transform.position, plantPrefab.transform.rotation);
                        seedCount--;
                    }
                }
            }
        }

        if(Input.GetKey(KeyCode.Q)){
            if(reticleController.isInRange){
                reticleRenderer.sprite = wateringReticle;
                if(Input.GetMouseButtonDown(0)){
                    if(waterCount > 0){
                        waterCount--;
                    }
                }
            }
        }

        if(!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q)){
            reticleRenderer.sprite = defaultReticle;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Seed")){
            Destroy(other.gameObject);
            Debug.Log("Player collided with seed");
            seedCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            gameManager.GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Water Source")){
            if(waterTime > waterCollectTime && waterCount < waterLimit){
                waterCount++;
                waterTime = 0;
            }
        }
    }
}
