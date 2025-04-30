 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Uranus : Bumper
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
        base.OnCollisionEnter2D(col);

        // Add bonus time to the timer
        GameManager.Instance.timer += bonusTime;
        GameManager.Instance.timerText.text = "Time: " + GameManager.Instance.timer.ToString("0.0");

        
    }

    protected virtual void Score(int score)
    {
        Debug.Log("Bumper Add Score");
        GameManager.Instance.addScore(score);
    }
}
