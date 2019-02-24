using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(1, 4)]
    public int PlayerNumber;

    private MovementController movementController;
    private JumpThrowController jumpThrowController;
    private AttackController attackController;

    private GameObject indicator;
    // Use this for initialization
    void Start()
    {
        if (PlayerNumber < 1 || PlayerNumber > 4)
            Debug.LogError("Don't do that again. -- Victor");

        movementController = gameObject.GetComponent<MovementController>();
        jumpThrowController = gameObject.GetComponent<JumpThrowController>();
        attackController = gameObject.GetComponent<AttackController>();

        Transform indicatorTransform = transform.Find("Indicator");
        if (indicatorTransform == null)
            Debug.LogError("Please attach an Indicator to the player (both human and robot)!");
        else
            indicator = indicatorTransform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        movementController.Horizontal = Input.GetAxis("LeftHorizontal" + PlayerNumber);
        movementController.Vertical = Input.GetAxis("LeftVertical" + PlayerNumber);
        movementController.RightHorizontal = Input.GetAxis("RightHorizontal" + PlayerNumber);
        movementController.RightVertical = Input.GetAxis("RightVertical" + PlayerNumber);
        jumpThrowController.JumpThrow = Input.GetButtonDown("Jump" + PlayerNumber);
        attackController.Attack = Input.GetButtonDown("Attack" + PlayerNumber);


        indicator.SetActive(Vector2.Distance(Vector2.zero,
                                new Vector2(movementController.RightHorizontal, movementController.RightVertical)) > 0.2f);

    }
}
