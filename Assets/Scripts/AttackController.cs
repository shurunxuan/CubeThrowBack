using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    // Set by PlayerController
    [HideInInspector]
    public bool RobotAttack;
    [HideInInspector]
    public float HumanAttackHorizontal;
    [HideInInspector]
    public float HumanAttackVertical;

    public bool IsRobot;

    public Animator RobotAnimator;

    private Vector3 cameraForward;
    private Vector3 cameraRight;

    // Use this for initialization
    void Start()
    {
        cameraForward = Vector3.Cross(Camera.main.transform.right, Vector3.up);
        cameraRight = Vector3.Cross(Vector3.up, cameraForward);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRobot)
        {
            if (RobotAttack)
            {
                // Robot attack
                RobotAnimator.SetTrigger("Attack");

                RobotAttack = false;
            }
        }
        else
        {
            // Human attack
            Debug.Log("Human attact towards " + HumanAttackHorizontal + ", " + HumanAttackVertical);
        }
    }
}
