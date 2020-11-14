using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPplayerCont : MonoBehaviour
{
    Rigidbody rbody;
    public LayerMask ground;
    public Transform feet;

    public float jumpHeight;
    private Vector3 direction;
    private float moveSpeed;
    private float doubleJump;
    private float maxJumps;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        jumpHeight = 20f;
        maxJumps = 1f;
        moveSpeed = 20f;
        doubleJump = 0f;
        gravity = -2f;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction = direction.normalized;
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
            //changed to fixedDeltaTime to fix jittering while moving
            //has to do with the physics timing and update timing not being synched up
            rbody.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        bool isGrounded()
        {
            if (Physics.CheckSphere(feet.position, 0.1f, ground))
            {
                doubleJump = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        if (Input.GetButtonDown("Jump") && (isGrounded() || doubleJump < maxJumps))
        {
            doubleJump += 1;
            rbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }
        rbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

    }

}
