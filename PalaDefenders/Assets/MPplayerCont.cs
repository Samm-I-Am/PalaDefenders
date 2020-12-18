using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPplayerCont : MonoBehaviour
{
    public Rigidbody rbody;
    
    //movement variables
    public LayerMask ground;
    public Transform feet;
    public float jumpHeight;
    private Vector3 direction;
    private float health;
    private float moveSpeed;
    private float doubleJump;
    private float maxJumps;
    public float gravity;
    public bool isJumping;
    public bool isDefending;
    private bool isDead;
    private AudioSource sliceSound;

    // Animator Controller
    private Animator anim;

    // Attacking variables
    public LayerMask enemyLayers;
    public int attackDamage;
    public float attackRate;
    float cooldown1;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        health = 100f;
        jumpHeight = 10f;
        maxJumps = 1f;
        moveSpeed = 5f;
        doubleJump = 0f;
        gravity = -2f;
        sliceSound = GetComponent<AudioSource>();


        //Animator
        anim = GetComponent<Animator>();

        // Attacking
        attackDamage = 50;
        attackRate = 2f;
        cooldown1 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = anim.GetBool("Dead");
        if(isDead==true)
        {
            return;
        }
        else
        {
            isJumping = anim.GetBool("isJumping");
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
            if (Physics.CheckSphere(feet.position, 0.01f, ground))
            {
                doubleJump = 0;
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isJumping", true);
            }

            if (Input.GetButtonDown("Jump") && (isJumping == false || doubleJump < maxJumps))
            {
                // Jump animation
                anim.SetTrigger("jump");

                doubleJump += 1;
                rbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            }
            rbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

            // Checks to see if next attack is possible
            //
            if (Time.time >= cooldown1)
            {
                // Left mouse click, swing sword
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Attack1");
                    sliceSound.Play();
                    cooldown1 = Time.time + (1f / attackRate);
                }
            }

            // Animator for running
            float moveInput = Input.GetAxisRaw("Horizontal");

            // Checks to see if character is moving, changes state from idle to/from running
            if (moveInput == 0)
                anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);
        }
    }
    public void takeDamage(float takenDamage)
    {
        health -= takenDamage;
        if (health > 0)
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
            anim.SetBool("Dead", true);
        }
        

    }

    //called by animation event to enable weapon at specific frames
    public void enableWeapon()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }

    //called by animation event to disable weapon at specific frames
    public void disableWeapon()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }
}
