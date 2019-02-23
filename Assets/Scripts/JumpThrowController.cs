using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThrowController : MonoBehaviour {

    // Set by PlayerController
    [HideInInspector]
    public bool JumpThrow;

    public float JumpForce;


    private Rigidbody playerRigidbody;
    // Use this for initialization
    void Start () {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		if (JumpThrow)
        {
            playerRigidbody.AddForce(Vector3.up * JumpForce);
            JumpThrow = false;
        }
	}
}
