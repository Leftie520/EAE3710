using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class RightTriBumper : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnCollisionEnter2D(Collision2D col)
    {
        // getting the other collider (better be a goddamn ball :)
        GameObject obj = col.gameObject;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // finding the angle of the collision and normalizing the new ball velocity
        // Vector3 newVel = new Vector3(rb.position.x, rb.position.y) - transform.position;
        Vector3 newVel = new Vector3(-1, 1, 0);
        newVel.Normalize();
        newVel *= 20;

        // actually setting the new ball velocity
        rb.linearVelocity = newVel;

        // this value will likely change as balancing ensues.
        Score(50);

    }

    protected virtual void Score(int score)
    {
        GameManager.Instance.addScore(score);
    }

}