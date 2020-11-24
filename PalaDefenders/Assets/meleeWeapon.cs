using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeapon : MonoBehaviour
{
    public MPplayerCont playerController;

    // Start is called before the first frame update
    void Awake()
    {
        //keeps weapon from creating physics collisions with player character
        Physics.IgnoreCollision(playerController.GetComponent<Collider>(), GetComponent<Collider>());
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
            other.GetComponent<Minion>().TakeDamage(playerController.attackDamage);
        }
    }
}
