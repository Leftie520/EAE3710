using UnityEngine;

public class CloneBumper : Bumper
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        score += 100;
        Debug.Log(score);
        //if()

    }
}
