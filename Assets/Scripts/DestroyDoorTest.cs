using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoorTest : MonoBehaviour {

    public GameObject breakdownPrefab;
    public GameObject doorObj;

    // Update is called once per frame
    void Start()
    {
        
    }
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 effectPos = new Vector3(doorObj.transform.position.x, doorObj.transform.position.y, doorObj.transform.position.z);
            Vector3 effectRot = new Vector3(doorObj.transform.rotation.x, doorObj.transform.rotation.y, doorObj.transform.rotation.z);
            Destroy(doorObj.gameObject);
            GameObject breakDownEffect = Instantiate(breakdownPrefab, effectPos, Quaternion.identity) as GameObject; 
        }
	}
}
