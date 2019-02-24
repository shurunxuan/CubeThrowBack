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

    public float AttackCoolDownTime;

    [Header("Robot Attack Properties")]
    public Animator RobotAnimator;

    [Header("Human Attack Properties")]
    public GameObject Projectile;

    private Vector3 cameraForward;
    private Vector3 cameraRight;

    private float lastHorizontal;
    private float lastVertical;

    private float coolDownTimer;

    private List<GameObject> otherPlayers;

    // Use this for initialization
    void Start()
    {
        cameraForward = Vector3.Cross(Camera.main.transform.right, Vector3.up);
        cameraRight = Vector3.Cross(Vector3.up, cameraForward);

        lastHorizontal = 0;
        lastVertical = 0;

        otherPlayers = new List<GameObject>(3);
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in allPlayers)
        {
            if (player == gameObject) continue;
            otherPlayers.Add(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (IsRobot)
        {
            if (RobotAttack && coolDownTimer > AttackCoolDownTime)
            {
                // Robot attack
                RobotAnimator.SetTrigger("Attack");

                RobotAttack = false;
                coolDownTimer = 0;
            }
        }
        else
        {
            // Human rotation
            float horDiff = HumanAttackHorizontal - lastHorizontal;
            float verDiff = HumanAttackVertical - lastVertical;
            Vector2 lastAbs = new Vector2(lastHorizontal, lastVertical);
            Vector2 absolute = new Vector2(HumanAttackHorizontal, HumanAttackVertical);
            Vector2 diff = new Vector2(horDiff, verDiff);
            transform.LookAt(HumanAttackHorizontal * cameraRight + HumanAttackVertical * cameraForward +
                             transform.position);

            if (diff.magnitude > 0.3f && absolute.magnitude > 0.5f && lastAbs.magnitude < 0.5f && coolDownTimer > AttackCoolDownTime)
            {
                // Human Attack
                GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                projectile.name = name + "Projectile";
                projectile.GetComponent<ProjectileController>().Origin = gameObject;

                // A little auto aim
                projectile.GetComponent<ProjectileController>().Direction = transform.forward;
                float minAngle = 180.0f;
                GameObject target = null;
                foreach (var player in otherPlayers)
                {
                    Vector3 dirToPlayer = player.transform.position - transform.position;
                    float angle = Vector3.Angle(transform.forward, dirToPlayer);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        target = player;
                    }
                }

                if (target != null)
                {
                    if (minAngle < 20.0f)
                        projectile.GetComponent<ProjectileController>().Direction = (target.transform.position - transform.position).normalized;
                }

                projectile.SetActive(true);

                coolDownTimer = 0;
            }
        }

        lastHorizontal = HumanAttackHorizontal;
        lastVertical = HumanAttackVertical;

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.name.EndsWith("Projectile")) return;
        if (other.name.StartsWith(name)) return;
        // Get Damage
        Debug.Log(name + " damaged by " + other.name);
    }
}
