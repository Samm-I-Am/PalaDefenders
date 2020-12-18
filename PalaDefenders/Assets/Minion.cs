using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Animator animator;

    //health variables
    public int maxHealth;
    public int currentHealth;

    //delegate and event functions
    public delegate void minionDeath();
    public static event minionDeath minionDied;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth = 100;
    }

    public void TakeDamage(int damage)
    {
        // Change health based on damage
        currentHealth -= damage;


        if (currentHealth <= 0)
            Die();
        else
            animator.SetTrigger("Hurt");
    }

    void Die()
    {
        //declares this event to eventManager
        minionDied();

        // Die animations
        animator.SetBool("isDead", true);

        // Disable the enemy
        GetComponent<BoxCollider>().enabled = false;
        this.enabled = false;

    }
}
