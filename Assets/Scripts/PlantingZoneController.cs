using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingZoneController : MonoBehaviour
{
    public GameObject player;
    public GameObject plantPrefab;
    public GameObject reticle;

    private ReticleController reticleController;

    // Start is called before the first frame update
    void Start()
    {
        reticleController = reticle.GetComponent<ReticleController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, .2f, 0);
    }

    void OnMouseDown() {
        Debug.Log("Planting position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void OnMouseOver() {
        // TODO: planting reticle
        reticleController.SetReticle("PlantingReticle");
    }

    void OnMouseExit() {
        // TODO: no planting reticle
        reticleController.SetReticle("NoPlantingReticle");
    }
}
