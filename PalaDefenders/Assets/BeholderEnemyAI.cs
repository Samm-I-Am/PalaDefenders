﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEnemyAI : MonoBehaviour
{
    public GameObject rangeCheck;
    public GameObject attackCheck;
    public GameObject player;
    public GameObject hitParticleEffect;
    public GameObject deathParticleEffect;
    public GameObject meshObject;
    public GameObject projectileSpawn;
    public GameObject projectile;
    public Rigidbody rbody;
    public float attackDamage;
    public float alertRange;
    public float attackingRange;
    public float playerXpos;
    public float moveSpeed;
    public float health;
    private Vector3 direction;
    private Animator anim;
    public delegate void minionDeath();
    public static event minionDeath minionDied;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        health = 100f;
        attackDamage = 10f;
        alertRange = 10f;
        attackingRange = 2.4f;
        moveSpeed = 3f;
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            //do nothing
        }
        else
        {
            playerXpos = player.transform.position.x;
            direction = Vector3.zero;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Alert"))
            {
                anim.SetBool("inAttackRange", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            {
                attackCheck.GetComponent<Collider>().enabled = true;
                //player is to the left of enemy
                if (playerXpos < transform.position.x)
                { direction = new Vector3(-1, 0, 0); }
                else //player is to the right of enemy
                { direction = new Vector3(1, 0, 0); }
                transform.forward = direction;
                rbody.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Instantiate(projectile, projectileSpawn.transform);
            }
        }
    }

    public void alerted()
    {
        rangeCheck.GetComponent<Collider>().enabled = false;
        anim.SetTrigger("alerted");
    }

    public void inAttackRange()
    {
        anim.SetBool("inAttackRange", true);
    }

    public void TakeDamage(float takenDamage)
    {
        health -= takenDamage;
        if (health < 0)
        {
            anim.SetTrigger("Dead");
            Instantiate(hitParticleEffect, this.transform);
            minionDied();
            StartCoroutine(Explode());
        }
        else
        {
            anim.SetTrigger("Hurt");
            Instantiate(hitParticleEffect, this.transform);
        }

    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.5f);
        meshObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        Instantiate(deathParticleEffect, this.transform);
        Destroy(this, 1);
        Destroy(gameObject, 1);
    }
}
