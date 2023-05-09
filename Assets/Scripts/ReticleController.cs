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
                SetReticle(plantingReticle);
            } else {
                SetReticle(plantingBlockedReticle);
            }
        }

        if(Input.GetKey(KeyCode.Q)) {
            if(InRange()) {
                SetReticle(wateringReticle);
            } else {
                SetReticle(wateringBlockedReticle);
            }
        }

        if(!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E)) {
            SetReticle(defaultReticle);
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

    void SetReticle(Sprite newReticle) {
        reticleRenderer.sprite = newReticle;
    }
}
