using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPplayerCont : MonoBehaviour
{
    Rigidbody rbody;

    //level up effects
    public GameObject levelUpParticle;
    public Transform particleSpawn;
    
    //movement variables
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

    // Attacking variables
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public int attackDamage;
    public float attackRate;
    float nextAttackTime;
    public GameObject weapon;

    //when object is enabled...
    //assigns the playerLevelUp function to the LevelUp event from eventManager
    private void OnEnable()
    {
        eventManager.levelUp += playerLevelUp;
    }

    //when object is disabled...
    //removes the playerLevelUp function from the LevelUp event from eventManager 
    private void OnDisable()
    {
        eventManager.levelUp -= playerLevelUp;
    }

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
        attackDamage = 50;
        attackRate = 2f;
        nextAttackTime = 0f;
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

        // Checks to see if next attack is possible
        if(Time.time >= nextAttackTime)
        {
            // Left mouse click, swing sword
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
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

    // Function for attacking enemies
    void Attack()
    {
        anim.SetTrigger("Attack");



        /*// Detect enemies in sight
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach(Collider enemy in hitEnemies)
        {
            //Debug.Log("we hit " + enemy.name);

            // Passes damage to minion
            enemy.GetComponent<Minion>().TakeDamage(attackDamage);

        }*/

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

    // Visually see sphere of attack point in scene view
    /*void OnDrawGizmosSelected()
    {
        // Attack point already selected
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/

    //is called when eventManager.levelUp event is called
    void playerLevelUp()
    {
        Instantiate(levelUpParticle, particleSpawn.position, Quaternion.identity);
    }
}
