using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRenderTest : MonoBehaviour {

	// Use this for initialization

	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(10.0f * Mathf.Sin(Time.time), 0.0f, 0.0f);
	}
}
