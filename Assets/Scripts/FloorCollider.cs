using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour {

    public Transform mainBody;
    bool onGround;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool OnGround()
    {
        return onGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        onGround = true;
    }

    private void OnTriggerStay(Collider other)
    {
        onGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onGround = false;
    }

}
