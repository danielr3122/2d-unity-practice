using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer reticleRenderer;
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
        if(reticleRenderer.sprite == plantingReticle || reticleRenderer.sprite == wateringReticle){
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x < 1
            && Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y < 1){
                isInRange = true;
            } else {
                if(reticleRenderer.sprite == plantingReticle){
                    reticleRenderer.sprite = plantingBlockedReticle;
                } else if(reticleRenderer.sprite == wateringReticle){
                    reticleRenderer.sprite = wateringBlockedReticle;
                }
                isInRange = false;
            }
        }
    }

    public void ChangeReticle(Sprite newReticle) {
        reticleRenderer.sprite = newReticle;
    }
}
