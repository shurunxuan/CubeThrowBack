using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(1, 4)]
    public int PlayerNumber;
    public float StunTime = .1f;
    public float KnockBack = 100;
    public float ThrowBack = 1000;

    private MovementController movementController;
    private JumpThrowController jumpThrowController;
    private AttackController attackController;
    private LandingLogic landingLogic;
    private Rigidbody rigid;
    private FloorCollider floor;

    private GameObject indicator;

    private bool stunned;
    public bool Stunned
    {
        get { return stunned; }
        set
        {
            stunned = value;
            movementController.enabled = !stunned;
            attackController.enabled = !stunned;
        }
    }
    private float stunTimer;
    private bool onGround = false;

    // Use this for initialization
    void Start()
    {
        if (PlayerNumber < 1 || PlayerNumber > 4)
            Debug.LogError("Don't do that again. -- Victor");

        movementController = GetComponent<MovementController>();
        jumpThrowController = GetComponent<JumpThrowController>();
        attackController = GetComponent<AttackController>();
        landingLogic = GetComponent<LandingLogic>();
        rigid = GetComponent<Rigidbody>();
        floor = GetComponentInChildren<FloorCollider>();

        Transform indicatorTransform = transform.Find("Indicator");
        if (indicatorTransform == null)
            Debug.LogError("Please attach an Indicator to the player (both human and robot)!");
        else
            indicator = indicatorTransform.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        onGround = floor.OnGround();
        if (stunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer < 0)
            {
                Stunned = false;
            }
        }

        if (landingLogic.robot != null)
        {
            // Robot move
            landingLogic.robot.MovementController.Horizontal = Input.GetAxis("LeftHorizontal" + PlayerNumber);
            landingLogic.robot.MovementController.Vertical = Input.GetAxis("LeftVertical" + PlayerNumber);
            landingLogic.robot.MovementController.RightHorizontal = Input.GetAxis("RightHorizontal" + PlayerNumber);
            landingLogic.robot.MovementController.RightVertical = Input.GetAxis("RightVertical" + PlayerNumber);
            // Robot attack
            landingLogic.robot.AttackController.Attack = Input.GetButtonDown("Attack" + PlayerNumber);

            movementController.Horizontal = 0;
            movementController.Vertical = 0;
            movementController.RightHorizontal = 0;
            movementController.RightVertical = 0;
            jumpThrowController.JumpThrow = false;
            attackController.Attack = false;
        }
        else
        {
            movementController.Horizontal = Input.GetAxis("LeftHorizontal" + PlayerNumber);
            movementController.Vertical = Input.GetAxis("LeftVertical" + PlayerNumber);
            movementController.RightHorizontal = Input.GetAxis("RightHorizontal" + PlayerNumber);
            movementController.RightVertical = Input.GetAxis("RightVertical" + PlayerNumber);
            if (Input.GetButtonDown("Jump" + PlayerNumber) && onGround)
            {
                if (landingLogic.above)
                {
                    PlayerController other = landingLogic.above.GetComponent<PlayerController>();
                    other.jumpThrowController.JumpThrow = true;
                    other.jumpThrowController.KnockBack = -transform.forward * ThrowBack;
                    other.Stunned = true;
                    other.stunTimer = other.StunTime;
                }
                else
                {
                    jumpThrowController.JumpThrow = true;
                }
            }
            attackController.Attack = Input.GetButtonDown("Attack" + PlayerNumber) && onGround;
        }

        indicator.SetActive(Vector2.Distance(Vector2.zero, new Vector2(movementController.RightHorizontal, movementController.RightVertical)) > 0.2f ||
                            Vector2.Distance(Vector2.zero, new Vector2(movementController.Horizontal, movementController.Vertical)) > 0.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.name.EndsWith("Projectile")) return;
        if (other.name.StartsWith(name)) return;
        if (other.transform.root == this.transform.root) return;
        // Get Damage
        landingLogic.Detach();
        Stunned = true;
        stunTimer = StunTime;
        rigid.velocity = Vector3.zero;
        jumpThrowController.KnockBack = other.transform.forward;
        jumpThrowController.KnockBack.y = 0;
        jumpThrowController.KnockBack = Vector3.Normalize(jumpThrowController.KnockBack) * KnockBack;
        jumpThrowController.JumpThrow = true;
        Debug.Log(name + " damaged by " + other.name);
    }
}
