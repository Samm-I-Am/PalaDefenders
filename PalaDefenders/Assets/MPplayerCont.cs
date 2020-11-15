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

    // Animator Controller
    private Animator anim;

    // Attacking components
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        jumpHeight = 10f;
        maxJumps = 1f;
        moveSpeed = 5f;
        doubleJump = 0f;
        gravity = -2f;

        //Animator
        anim = GetComponent<Animator>();

        // Attacking
        attackRange = 0.5f;
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
                anim.SetBool("isJumping", false);
                return true;
            }
            else
            {
                anim.SetBool("isJumping", true);
                return false;
            }
        }

        if (Input.GetButtonDown("Jump") && (isGrounded() || doubleJump < maxJumps))
        {
            // Jump animation
            anim.SetTrigger("jump");


            doubleJump += 1;
            rbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }
        rbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        // Left mouse click, swing sword
        if (Input.GetButtonDown("Fire1"))
            Attack();

        // Animator for running
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Checks to see if character is moving, changes state from idle to/from running
        if (moveInput == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

    }

    // Function for attacking enemies
    void Attack()
    {
        anim.SetTrigger("Attack");

        // Detect enemies in sight
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);

        }

    }

    // Visually see sphere of attack point
    void OnDrawGizmosSelected()
    {
        // Attack point already selected
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
