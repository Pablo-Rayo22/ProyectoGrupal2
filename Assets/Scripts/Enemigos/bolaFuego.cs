using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaFuego : MonoBehaviour
{
    public float power = 20f;
    public float lifeTime = 5f;
    private float deltatime = 0f;
    

    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = this.transform.forward * power;

    }

    void Update()
    {
        
        deltatime += Time.deltaTime;
        if (deltatime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    

    private void OnTriggerEnter(Collider collision)
    {
        

        if (collision.tag == "Player")
        {
            
            Transform tr = collision.gameObject.transform;
            Destroy(this.gameObject);


        }
    }
}
    
