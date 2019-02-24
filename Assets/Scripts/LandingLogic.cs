﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingLogic : MonoBehaviour {
    public float snapToLand;
    public float snapToFace;
    public Collider head;

    public Robot robot;
    private LandingLogic above;
    private Collider below;
    private LandingLogic belowPlayer;
    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        robot = null;
        above = null;
        below = null;
        rigid = GetComponent<Rigidbody>();    
	}
	
	// Update is called once per frame
	void Update () {
        if(below != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, below.transform.position, snapToLand * Time.deltaTime);
        }
        if (robot)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, snapToFace * Time.deltaTime);
        }
    }

    // Landing on stuff
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ObjectHeadSlot" && !below)
        {
            other.enabled = false;
            below = other;
            rigid.isKinematic = true;
            belowPlayer = other.transform.root.GetComponentInChildren<LandingLogic>();
            if (belowPlayer)
            {
                while (belowPlayer.above) belowPlayer = belowPlayer.above;
                belowPlayer.above = this;
            }
            robot = other.transform.root.GetComponent<Robot>();
            if(robot)
            {
                head.enabled = false;
            }
            transform.parent = other.transform;
        }
    }

    public void Detach()
    {
        if(!below)
        {
            return;
        }
        if (belowPlayer)
        {
            belowPlayer.above = null;
            belowPlayer = null;
        }
        if(robot)
        {
            robot = null;
            head.enabled = true;
        }
        StartCoroutine(ReactivateCollider(below));
        below = null;
        rigid.isKinematic = false;
        rigid.velocity = Vector3.zero;
        transform.parent = null;
    }


    IEnumerator ReactivateCollider(Collider col)
    {
        yield return new WaitForSeconds(.5f);
        col.enabled = true;
    }

}
