using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Animator animator;
    public int maxHealth;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth = 100;
    }

    public void TakeDamage(int damage)
    {
        // Change health based on damage
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();

    }

    void Die()
    {
        Debug.Log("Minion died!");

        // Die animations
        animator.SetBool("isDead", true);

        // Disable the enemy
        GetComponent<BoxCollider>().enabled = false;
        this.enabled = false;

    }
}
