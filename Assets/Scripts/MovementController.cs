using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    // Set by PlayerController
    [HideInInspector]
    public float Horizontal;
    [HideInInspector]
    public float Vertical;


    public float MovementSpeed;

    private Vector3 cameraForward;
    private Vector3 cameraRight;
    // Use this for initialization
    void Start () {
        cameraForward = Vector3.Cross(Camera.main.transform.right, Vector3.up);
        cameraRight = Vector3.Cross(Vector3.up, cameraForward);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += (cameraForward * Vertical + cameraRight * Horizontal) * MovementSpeed;
    }
}
