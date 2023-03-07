using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public GameObject topRightBoundary;
    public GameObject bottomLeftBoundary;

    private Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        SetBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetBoundaries(){
        point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        topRightBoundary.transform.position = point;

        point = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        bottomLeftBoundary.transform.position = point;
    }
}
