using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer reticleRenderer;
    public Sprite defaultReticle;
    public Sprite plantingReticle;
    public Sprite plantingBlockedReticle;
    public Sprite wateringReticle;
    public Sprite wateringBlockedReticle;

    public bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if(Input.GetKey(KeyCode.E)) {
            if(InRange()) {
                SetReticle("PlantingReticle");
            } else {
                SetReticle("NoPlantingReticle");
            }
        }

        if(Input.GetKey(KeyCode.Q)) {
            if(InRange()) {
                SetReticle("WateringReticle");
            } else {
                SetReticle("NoWateringReticle");
            }
        }

        if(!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E)) {
            SetReticle("DefaultReticle");
        }
    }

    bool InRange() {
        if(Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x) < 1
        && Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y) < 1){
            isInRange = true;
            return true;
        } else {
            isInRange = false;
            return false;
        }
    }

    public void SetReticle(string reticleString) {
        switch (reticleString){
            case "PlantingReticle":
                reticleRenderer.sprite = plantingReticle;
                break;

            case "NoPlantingReticle":
                reticleRenderer.sprite = plantingBlockedReticle;
                break;

            case "WateringReticle":
                reticleRenderer.sprite = wateringReticle;
                break;

            case "NoWateringReticle":
                reticleRenderer.sprite = wateringBlockedReticle;
                break;

            case "DefaultReticle":
                reticleRenderer.sprite = defaultReticle;
                break;
        }
    }
}
