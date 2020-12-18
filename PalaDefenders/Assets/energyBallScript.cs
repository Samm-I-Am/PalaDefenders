using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyBallScript : MonoBehaviour
{
    Vector3 direction;
    public Transform target;
    public Rigidbody rbody;
    public GameObject EnergyParticle;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        transform.LookAt(target);
        rbody.velocity = transform.forward * 24;
        Instantiate(EnergyParticle, this.transform);
        Destroy(this, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
