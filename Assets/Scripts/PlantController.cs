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

        // TODO: Change sprite between stages, spawn x number of seeds
        if(time > stageOneTime && plantStage == "Seedling"){
            currentSprite.sprite = stageTwoSprite;
            plantStage = "Sprout";
            time = 0;
        }
        if(time > stageTwoTime && plantStage == "Sprout"){
            currentSprite.sprite = stageThreeSprite;
            Instantiate(seedPrefab, transform.position, seedPrefab.transform.rotation);
            plantStage = "Flower";
            time = 0;
        }
        if(time > despawnTime && plantStage == "Flower"){
            Destroy(gameObject);
        }
    }
}
