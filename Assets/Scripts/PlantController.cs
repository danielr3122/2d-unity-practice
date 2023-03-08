using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    private float time;
    private float stageOneTime = 5f;
    private float stageTwoTime = 5f;
    private float despawnTime = 5f;
    public string plantStage;
    public GameObject seedPrefab;
    public SpriteRenderer currentSprite;
    public Sprite stageTwoSprite;
    public Sprite stageThreeSprite;
    private GameObject currSeed;
    public Sprite plantingReticleSprite;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        plantStage = "Seedling";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > stageOneTime && plantStage == "Seedling"){
            currentSprite.sprite = stageTwoSprite;
            plantStage = "Sprout";
            time = 0;
        }
        if(time > stageTwoTime && plantStage == "Sprout"){
            currentSprite.sprite = stageThreeSprite;
            SpawnSeed();
            plantStage = "Flower";
            time = 0;
        }
        if(time > despawnTime && plantStage == "Flower"){
            SpawnSeed();
            Destroy(gameObject);
        }
    }

    private void SpawnSeed(){
        currSeed = Instantiate(seedPrefab, transform.position, seedPrefab.transform.rotation);
        currSeed.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)).normalized * 5, ForceMode2D.Impulse);
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("Plant") && GameObject.FindGameObjectWithTag("Reticle").GetComponent<SpriteRenderer>().sprite == plantingReticleSprite){
    //         Debug.Log("Shouldn't be able to plant here");
    //         SpawnSeed();
    //         Destroy(gameObject);
    //     }
    // }

    // private void OnTriggerStay2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("Plant") && GameObject.FindGameObjectWithTag("Reticle").GetComponent<SpriteRenderer>().sprite == plantingReticleSprite){
    //         Debug.Log("Shouldn't be able to plant here");
    //         SpawnSeed();
    //         Destroy(gameObject);
    //     }
    // }
}
