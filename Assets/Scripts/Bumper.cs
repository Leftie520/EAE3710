using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bumper : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public int veloctiy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("base bumper collision");
        // getting the other collider (better be a goddamn ball :)
        GameObject obj = col.gameObject;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // finding the angle of the collision and normalizing the new ball velocity
        Vector3 newVel = new Vector3(rb.position.x, rb.position.y) - transform.position;
        newVel.Normalize();
        newVel *= veloctiy;

        // actually setting the new ball velocity
        rb.linearVelocity = newVel;

        // this value will likely change as balancing ensues.
        Score(score);

    }

    protected virtual void Score(int score)
    {
        Debug.Log("Bumper Add Score");
        GameManager.Instance.addScore(score);
    }

}
