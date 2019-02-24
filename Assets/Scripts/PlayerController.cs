using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(1, 4)]
    public int PlayerNumber;

    private MovementController movementController;
    private JumpThrowController jumpThrowController;
    private AttackController attackController;
    // Use this for initialization
    void Start()
    {
        if (PlayerNumber < 1 || PlayerNumber > 4)
            Debug.LogError("Don't do that again. -- Victor");

        movementController = gameObject.GetComponent<MovementController>();
        jumpThrowController = gameObject.GetComponent<JumpThrowController>();
        attackController = gameObject.GetComponent<AttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        movementController.Horizontal = Input.GetAxis("LeftHorizontal" + PlayerNumber);
        movementController.Vertical = Input.GetAxis("LeftVertical" + PlayerNumber);
        attackController.HumanAttackHorizontal = Input.GetAxis("RightHorizontal" + PlayerNumber);
        attackController.HumanAttackVertical = Input.GetAxis("RightVertical" + PlayerNumber);
        jumpThrowController.JumpThrow = Input.GetButtonDown("Jump" + PlayerNumber);
        attackController.RobotAttack = Input.GetButtonDown("Attack" + PlayerNumber);            
    }
}
