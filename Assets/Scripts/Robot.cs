using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    public float speed;
    public Collider hitbox;

    public CharacterController control;

	// Use this for initialization
	void Start () {
        control = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
