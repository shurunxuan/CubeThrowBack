using UnityEngine;

public class MovementController : MonoBehaviour
{

    // Set by PlayerController
    [HideInInspector]
    public float Horizontal;
    [HideInInspector]
    public float Vertical;
    [HideInInspector]
    public float RightHorizontal;
    [HideInInspector]
    public float RightVertical;


    public float MovementSpeed;
    public bool RotateWhenMove;

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
        transform.position += (cameraForward * Vertical + cameraRight * Horizontal) * MovementSpeed;
        transform.LookAt(RightHorizontal * cameraRight + RightVertical * cameraForward + transform.position);

        if (RotateWhenMove)
        {
            transform.LookAt(Horizontal * cameraRight + Vertical * cameraForward + transform.position);
        }
    }
}
