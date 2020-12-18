using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestHeadAttackScript : MonoBehaviour
{
    public ChestEnemyAI enemyController;
    public float knockback;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(enemyController.GetComponent<Collider>(), GetComponent<Collider>());
        knockback = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //layer 10 is "player"
        {
            other.GetComponent<MPplayerCont>().takeDamage(enemyController.attackDamage);
            //player is to the left of enemy
            if (enemyController.playerXpos < transform.position.x)
            { other.GetComponent<MPplayerCont>().rbody.AddForce(Vector3.right * -knockback, ForceMode.VelocityChange); }
            else //player is to the right of enemy
            { other.GetComponent<MPplayerCont>().rbody.AddForce(Vector3.right * knockback, ForceMode.VelocityChange); }
            
        }
    }
}
