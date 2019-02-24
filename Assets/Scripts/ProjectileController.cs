using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [HideInInspector]
    public GameObject Origin;
    [HideInInspector]
    public Vector3 Direction;

    public float Speed;
    public float LifeTime;

    private Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, LifeTime);
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.MovePosition(transform.position + Direction * Speed);
        transform.forward = Direction;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == Origin.name) return;
        Destroy(gameObject);
    }
}
