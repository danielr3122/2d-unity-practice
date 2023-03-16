using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer reticleRenderer;
    public Sprite plantingReticleSprite;
    public Sprite wateringReticleSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(reticleRenderer.sprite == plantingReticleSprite || reticleRenderer.sprite == wateringReticleSprite){
            transform.position = ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition + player.transform.position)).normalized + (Vector2) player.transform.position;
        } else {
            transform.position = ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
