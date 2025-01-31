using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bumper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // getting the other collider (better be a goddamn ball :)
        GameObject obj = col.gameObject;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // finding the angle of the collision and normalizing the new ball velocity
        Vector3 newVel = new Vector3(rb.position.x, rb.position.y) - transform.position;
        newVel.Normalize();
        newVel *= 20;

        // actually setting the new ball velocity
        rb.velocity = newVel;


    }

}
