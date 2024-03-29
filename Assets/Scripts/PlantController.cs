using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    private GameObject currSeed;
    public GameObject seedPrefab;
    private GameObject player;
    private GameObject reticle;

    private float time;
    private float decayTime;
    private float timeToDecay = 15f;
    private float stageOneTime = 5f;
    private float stageTwoTime = 5f;
    private float despawnTime = 5f;

    public string plantStage;

    public SpriteRenderer spriteRenderer;
    public Sprite stageZeroSprite;
    public Sprite stageOneSprite;
    public Sprite stageTwoSprite;
    public Sprite stageThreeSprite;
    public Sprite plantingReticleSprite;

    public bool receivedFirstWater;
    public bool isWatered;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reticle = GameObject.FindGameObjectWithTag("Reticle");
        time = 0;
        plantStage = "Seedling";
        receivedFirstWater = false;
        isWatered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(receivedFirstWater){
            if(isWatered){
                time += Time.deltaTime * 1.1f;
                isWatered = false;
            } else {
                time += Time.deltaTime;
            }
        } else {
            decayTime += Time.deltaTime;
        }

        if(decayTime > timeToDecay){
            Destroy(gameObject);
        }

        if(time > stageOneTime && plantStage == "Seedling"){
            spriteRenderer.sprite = stageTwoSprite;
            plantStage = "Sprout";
            isWatered = false;
            time = 0;
        }
        if(time > stageTwoTime && plantStage == "Sprout"){
            spriteRenderer.sprite = stageThreeSprite;
            SpawnSeed();
            plantStage = "Flower";
            isWatered = false;
            time = 0;
        }
        if(time > despawnTime && plantStage == "Flower"){
            isWatered = false;
            SpawnSeed();
            Destroy(gameObject);
        }

        if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Q)) {
            if(player.GetComponent<PlayerController>().waterCount > 0
            && (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < 1)
            && (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y < 1)) {
                Debug.Log("PLANT WATERED");
                receivedFirstWater = true;
                isWatered = true;
                spriteRenderer.sprite = stageOneSprite;
            }
        }
    }

    private void SpawnSeed(){
        currSeed = Instantiate(seedPrefab, transform.position, seedPrefab.transform.rotation);
        currSeed.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)).normalized * 5, ForceMode2D.Impulse);
    }
}
