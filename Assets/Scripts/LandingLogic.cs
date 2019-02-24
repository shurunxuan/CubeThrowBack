using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingLogic : MonoBehaviour {
    public float snapToLand;

    private Robot robot;
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
            while (belowPlayer.above) belowPlayer = belowPlayer.above;
            belowPlayer.above = this;

            transform.parent = other.transform;
        }
    }

    public void Jump()
    {
        if (belowPlayer)
        {
            StartCoroutine(ReactivateCollider(below));
            belowPlayer.above = null;
            belowPlayer = null;
            below = null;
        }
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
