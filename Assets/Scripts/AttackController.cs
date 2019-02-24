using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    // Set by PlayerController
    [HideInInspector]
    public bool Attack;

    public bool IsRobot;

    public float AttackCoolDownTime;

    [Header("Robot Attack Properties")]
    public Animator RobotAnimator;

    [Header("Human Attack Properties")]
    public GameObject Projectile;

    private float coolDownTimer;

    private List<GameObject> otherPlayers;

    private LandingLogic landingLogic;
    // Use this for initialization
    void Start()
    {
        otherPlayers = new List<GameObject>(3);
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in allPlayers)
        {
            if (player == gameObject) continue;
            otherPlayers.Add(player);
        }

        landingLogic = GetComponent<LandingLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (IsRobot)
        {
            if (Attack && coolDownTimer > AttackCoolDownTime)
            {
                // Robot attack
                //RobotAnimator.SetTrigger("Attack");
                Debug.Log("Robot attack");

                Attack = false;
                coolDownTimer = 0;
            }
        }
        else
        {
            if (Attack && coolDownTimer > AttackCoolDownTime)
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
                Attack = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.name.EndsWith("Projectile")) return;
        if (other.name.StartsWith(name)) return;
        // Get Damage
        if (landingLogic.robot != null)
        {
            // Detach
            landingLogic.Detach();
        }
        Debug.Log(name + " damaged by " + other.name);
    }
}
