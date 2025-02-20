using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpeedBoost : MonoBehaviour
{
    public int xDir;

    public int yDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // finding the angle of the collision and normalizing the new ball velocity
        // Vector3 newVel = new Vector3(rb.position.x, rb.position.y) - transform.position;
        Vector3 newVel = new Vector3(-1, 1, 0);
        newVel.Normalize();
        newVel *= 50;

        // actually setting the new ball velocity
        rb.linearVelocity = newVel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
