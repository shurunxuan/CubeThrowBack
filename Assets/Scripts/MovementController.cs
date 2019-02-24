﻿using UnityEngine;

public class MovementController : MonoBehaviour
{

    // Set by PlayerController
    [HideInInspector]
    public float Horizontal;
    [HideInInspector]
    public float Vertical;


    public float MovementSpeed;
    public bool RotateWhenMove;

    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private Rigidbody rigid;
    
    // Use this for initialization
    void Start()
    {
        cameraForward = Vector3.Normalize(Vector3.Cross(Camera.main.transform.right, Vector3.up));
        cameraRight = Vector3.Normalize(Vector3.Cross(Vector3.up, cameraForward));
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float yVel = rigid.velocity.y;
        Vector3 vel = (cameraForward * Vertical + cameraRight * Horizontal) * MovementSpeed;
        vel.y = yVel;
        rigid.velocity = vel;
        if (RotateWhenMove)
        {
            transform.LookAt(Horizontal * cameraRight + Vertical * cameraForward + transform.position);
        }
    }
}
