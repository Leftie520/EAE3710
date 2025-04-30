using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Venus : Bumper
{
    public int score;
    public float angleOffsetRange = 90f;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        Vector3 newVel = new Vector3(rb.position.x, rb.position.y) - transform.position;
        newVel.Normalize();
        newVel *= 16;

        // Randomly rotate direction within range
        float angleOffset = Random.Range(-angleOffsetRange, angleOffsetRange);
        newVel = Quaternion.Euler(0, 0, angleOffset) * newVel;

        rb.linearVelocity = newVel;

        Score(score);
    }

    protected virtual void Score(int score)
    {
        Debug.Log("Venus Bumper Add Score");
        GameManager.Instance.addScore(score);
    }
}
