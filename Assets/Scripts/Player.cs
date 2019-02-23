using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public float jumpTime;

    public float snapToLand;

    public KeyCode jump;

    private Robot robot;
    private Player above;
    private Collider below;
    private Player belowPlayer;
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
        if (above == null && Input.GetKeyDown(jump)){
            Jump();
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
            belowPlayer = other.transform.root.GetComponentInChildren<Player>();
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
        transform.parent = null;
        rigid.velocity = new Vector3(0 , jumpHeight / jumpTime, 0);
    }


    IEnumerator ReactivateCollider(Collider col)
    {
        yield return new WaitForSeconds(.5f);
        col.enabled = true;
    }

}
