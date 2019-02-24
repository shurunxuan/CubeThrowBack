using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    
    //public Collider hitbox;

    //public CharacterController control;

    [HideInInspector]
    public MovementController MovementController;
    [HideInInspector]
    public AttackController AttackController;

    // Use this for initialization
    void Start () {
        //control = GetComponent<CharacterController>();
	    MovementController = GetComponent<MovementController>();
	    AttackController = GetComponentInChildren<AttackController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
