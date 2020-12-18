using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackCheckScript : MonoBehaviour
{
    public ChestEnemyAI enemyController;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(enemyController.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //layer 10 is "player"
        {
            GetComponent<Collider>().enabled = false;
            enemyController.GetComponent<ChestEnemyAI>().inAttackRange();
        }
    }
}
