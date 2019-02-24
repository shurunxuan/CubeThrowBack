using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [HideInInspector]
    public GameObject Origin;
    [HideInInspector]
    public Vector3 Direction;

    public float Speed;
    public float LifeTime;


    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == Origin.name) return;
        Destroy(gameObject);
    }
}
