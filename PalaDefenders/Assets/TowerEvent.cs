using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TowerEvent : MonoBehaviour
{
    public GameObject rangeCheck;
    public GameObject player;
    public GameObject hitParticleEffect;
    public GameObject deathParticleEffect;
    public GameObject meshObject;
    public Rigidbody rbody;
    public float alertRange;
    public float health;
    public delegate void PlayerApproach();
    public static event PlayerApproach approachingPlayer;
    public delegate void TowerDestroyed();
    public static event TowerDestroyed destroyedTower;


    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        alertRange = 10f;
        rbody = GetComponent<Rigidbody>();
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
            
        }

    }

    public void alerted()
    {
        rangeCheck.GetComponent<Collider>().enabled = false;
        
    }

    public void inAttackRange()
    {
        
    }

    public void TakeDamage(float takenDamage)
    {
        health -= takenDamage;
        if (health < 0)
        {
            
            Instantiate(hitParticleEffect, this.transform);
            StartCoroutine(Explode());
        }
        else
        {
            
            Instantiate(hitParticleEffect, this.transform);
        }

    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(1);
        meshObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        Instantiate(deathParticleEffect, this.transform);
        Destroy(this, 2);
        Destroy(gameObject, 2);
    }

}
