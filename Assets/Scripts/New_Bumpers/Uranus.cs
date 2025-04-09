using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Uranus : MonoBehaviour
{
    public int score;
    public float bonusTime = 5f;

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
        rb.linearVelocity = newVel;

        // Add bonus time to the timer
        GameManager.Instance.timer += bonusTime;
        GameManager.Instance.timerText.text = "Time: " + GameManager.Instance.timer.ToString("0.0");

        Score(score);
    }

    protected virtual void Score(int score)
    {
        Debug.Log("Bumper Add Score");
        GameManager.Instance.addScore(score);
    }
}
