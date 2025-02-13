using UnityEngine;
[AddComponentMenu("TienCuong/PlayerMovement")]
public class PlayerMovement : MonoBehaviour
{
    [Header("Character")]
    public float speed = 12f;
    public float gravity = -0.981f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    private CharacterController controller;
    private Vector3 veclocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.height = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCheckGround() && veclocity.y < 0)
        {
            veclocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward*z);
        controller.Move(move*speed*Time.deltaTime);
        if (Input.GetButtonDown("Jump") && IsCheckGround())
        {
            veclocity.y = Mathf.Sqrt(jumpHeight - gravity * 2f);
        }
        veclocity.y += gravity*Time.deltaTime;
        controller.Move(veclocity*Time.deltaTime);
    }
    bool IsCheckGround()
    {
        bool ground = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        return ground;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
