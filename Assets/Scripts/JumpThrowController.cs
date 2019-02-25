using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThrowController : MonoBehaviour {

    // Set by PlayerController
    [HideInInspector]
    public bool JumpThrow;

    public float JumpForce;
    [HideInInspector]
    public Vector3 KnockBack;

    private LandingLogic lander;
    private Rigidbody playerRigidbody;
    // Use this for initialization
    void Start () {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        lander = GetComponent<LandingLogic>();
    }
	
	// Update is called once per frame
	void Update () {
		if (JumpThrow)
        {
            lander.Detach();
            playerRigidbody.AddForce(Vector3.up * JumpForce + KnockBack);
            JumpThrow = false;
            KnockBack = Vector3.zero;
        }
	}
}
