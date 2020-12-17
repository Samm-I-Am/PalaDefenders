using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnemyAI : MonoBehaviour
{
    //Needs to interact similar to how the player character's sword did.
    //There are collision boxes for this enemy's head and lower body,
    //specifically under root/body , and root/body/head .
    //The head collision box is treated as a trigger, while the other one is not.
    //May have to assign box collider to the very top of the enemy object for
    //movement purposes if it acts janky for the physics collisions.
    //Head part will also need a script similar to meleeWeapon that handles overriding
    //physics collisions, and dealing with onTriggerEnter events.

    //Basic plan for chest enemy AI:
    //Starts off standing in one spot, looking around.
    //When player gets close, it switches to the alerted state, and then starts walking towards the player.
    //When player gets in range for it to attack, it goes to the attackStartUp state, then the attack state, then alerted state, and back to walking.

    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called by animation event to enable weapon at specific frames
    public void enableHurtbox()
    {
        head.GetComponent<Collider>().enabled = true;
    }

    //called by animation event to disable weapon at specific frames
    public void disableHurtbox()
    {
        head.GetComponent<Collider>().enabled = false;
    }
}
