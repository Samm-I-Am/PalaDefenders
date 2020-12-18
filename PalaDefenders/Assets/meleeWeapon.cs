using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeapon : MonoBehaviour
{
    public MPplayerCont playerController;
    public float knockback;

    // Start is called before the first frame update
    void Awake()
    {
        //keeps weapon from creating physics collisions with player character
        Physics.IgnoreCollision(playerController.GetComponent<Collider>(), GetComponent<Collider>());
        knockback = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sword must be rigidbody, while everything else is a trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) //layer 9 is "Minions"
        {
            switch(other.gameObject.tag)
            {
                case "Mimic":
                    other.GetComponent<ChestEnemyAI>().TakeDamage(playerController.attackDamage);
                    //player is to the left of enemy
                    if (other.GetComponent<ChestEnemyAI>().transform.position.x < playerController.transform.position.x)
                    { other.GetComponent<ChestEnemyAI>().rbody.AddForce(Vector3.right * -knockback, ForceMode.VelocityChange); }
                    else //player is to the right of enemy
                    { other.GetComponent<ChestEnemyAI>().rbody.AddForce(Vector3.right * knockback, ForceMode.VelocityChange); }
                    break;
            }
        }
    }
}
